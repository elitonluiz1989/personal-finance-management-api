using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PersonalFinanceManagement.Application.Contracts;
using PersonalFinanceManagement.Application.Dtos.Authentication;
using PersonalFinanceManagement.Domain.Users.Contracts;
using PersonalFinanceManagement.Domain.Users.Dtos;
using PersonalFinanceManagement.Domain.Users.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PersonalFinanceManagement.Api.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public SecurityToken? Token { get; private set; }
        private readonly IUserRepository _repository;
        private readonly JwtSecurityTokenHandler _handler;

        public AuthenticationService(IUserRepository repository)
        {
            _repository = repository;
            _handler = new JwtSecurityTokenHandler();
        }

        public async Task<AuthenticatedDto?> Authenticate(AuthenticationDto dto)
        {
            var results = await _repository.Query()
                .Where(p => p.UserName == dto.UserName && p.Password == dto.HashedPassword)
                .ToListAsync();
            var user = results.FirstOrDefault();

            if (user is null)
                return null;

            Token = CreateToken(dto, user);

            if (Token is null)
                return null;

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
                Expires = Token.ValidTo
            };
        }

        private SecurityToken CreateToken(AuthenticationDto dto, User user)
        {
            var descriptor = CreateDescriptor(dto, user);
            return _handler.CreateToken(descriptor);
        }

        private static SecurityTokenDescriptor CreateDescriptor(AuthenticationDto dto, User user)
        {
            var key = Encoding.ASCII.GetBytes(dto.AppSecret);
            var claims = new List<Claim>(2)
            {
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role.GetHashCode().ToString())
            };

            return new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(dto.ExpirationDelay),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };
        }
    }
}
