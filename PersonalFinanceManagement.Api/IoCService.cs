using PersonalFinanceManagement.Api.Services;
using PersonalFinanceManagement.Application.Contracts;
using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Base.Services;
using PersonalFinanceManagement.Domain.Users.Contracts;
using PersonalFinanceManagement.Domain.Users.Services;
using PersonalFinanceManagement.Domain.Users.Specifications;
using PersonalFinanceManagement.Infra.Data.Helpers;
using PersonalFinanceManagement.Infra.Data.Repositories.Users;

namespace PersonalFinanceManagement.Api
{
    internal static class IoCService
    {
        internal static void Configure(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<INotificationService, NotificationService>();

            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserSpecification, UserSpecification>();
            services.AddScoped<IUserStore, UserStore>();
            services.AddScoped<IUserDeleter, UserDeleter>();
        }
    }
}
