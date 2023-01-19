using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PersonalFinanceManagement.Application.Contracts;
using PersonalFinanceManagement.Application.Dtos.Authentication;
using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Base.Services;
using PersonalFinanceManagement.Domain.Users.Contracts;
using PersonalFinanceManagement.Domain.Users.Dtos;
using PersonalFinanceManagement.Domain.Users.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PersonalFinanceManagement.Api.Services
{
    public class AuthenticationService : NotifiableService, IAuthenticationService
    {
        public SecurityToken? Token { get; private set; }
        private readonly IUserRepository _repository;
        private readonly JwtSecurityTokenHandler _handler;

        public AuthenticationService(
            INotificationService notificationService,
            IUserRepository repository
        )
            : base(notificationService)
        {
            _repository = repository;
            _handler = new JwtSecurityTokenHandler()
            {
                SetDefaultTimesOnTokenCreation = false
            };
        }

        public async Task<AuthenticatedDto?> Authenticate(AuthenticationDto dto, IConfiguration configuration)
        {
            if (ValidateNullableObject(dto) is false)
                return default;

            var results = await _repository.Query()
                .Where(p => p.UserName == dto.UserName && p.Password == dto.HashedPassword)
                .ToListAsync();
            var user = results.FirstOrDefault();

            if (ValidateNullableObject(user) is false || user is null)
                return default;

            Token = CreateToken(user, configuration);

            if (ValidateNullableObject(Token) is false || Token is null)
                return default;

            user.RefreshToken = GenerateRefreshToken();
            user.RefeshTokenExperitionTime = DateTime.Now.AddDays(configuration.GetValue<uint>("Jwt:ExprirationDelayInMinutes"));

            var userDto = new UserDto()
            {
                UserName = user.UserName,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role
            };

            return new AuthenticatedDto()
            {
                User = userDto,
                Token = _handler.WriteToken(Token),
                RefreshToken = user.RefreshToken,
                Expires = Token.ValidTo
            };
        }

        public async Task<AuthenticationRefreshedDto?> Refresh(AuthenticationRefreshDto dto, IConfiguration configuration)
        {
            if (ValidateNullableObject(dto) is false)
                return default;

            var principal = GetPrincipalFromExpiredToken(dto.Token, configuration.GetValue<string>("Jwt:Secret"));

            if (principal is null)
            {
                AddNotification("Invalid access token/refresh token");

                return default;
            }

            var userIdValue = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value;

            if (string.IsNullOrEmpty(userIdValue))
            {
                AddNotification("User ID not found in token data");

                return default;
            }

            var success = int.TryParse(userIdValue, out var userId);

            if (success is false)
            {
                AddNotification("User ID not found in token data");

                return default;
            }

            var user = await _repository.Find(userId);

            if (user == null ||
                user.RefreshToken != dto.RefreshToken ||
                user.RefeshTokenExperitionTime < DateTime.Now)
            {
                AddNotification("Invalid token/refresh token");

                return default;
            }

            var newToken = CreateToken(user, configuration);
            var newRefreshToken = GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;

            return new AuthenticationRefreshedDto()
            {
                Token = _handler.WriteToken(newToken),
                RefreshToken = newRefreshToken,
                Expires = newToken.ValidTo
            };
        }

        private SecurityToken CreateToken(User user, IConfiguration configuration)
        {
            var descriptor = CreateDescriptor(user, configuration);

            return _handler.CreateToken(descriptor);
        }

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);

            return Convert.ToBase64String(randomNumber);
        }

        private static SecurityTokenDescriptor CreateDescriptor(User user, IConfiguration configuration)
        {
            var key = Encoding.ASCII.GetBytes(configuration.GetValue<string>("Jwt:Secret"));
            var claims = new List<Claim>(2)
            {
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role.GetHashCode().ToString())
            };

            return new SecurityTokenDescriptor()
            {
                Issuer = configuration.GetValue<string>("Jwt:ValidIssuer"),
                Audience = configuration.GetValue<string>("Jwt:ValidAudience"),
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(configuration.GetValue<uint>("Jwt:ExprirationDelayInMinutes")),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };
        }
        
        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string token, string secret)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                ValidateLifetime = false
            };
            var principal = _handler.ValidateToken(
                token,
                tokenValidationParameters,
                out SecurityToken securityToken
            );

            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                jwtSecurityToken.Header.Alg.Equals(
                    SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase
                ) is false)
                AddNotification("Invalid token");

            return principal;
        }
    }
}
