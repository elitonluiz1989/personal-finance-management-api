using PersonalFinanceManagement.Api.Services;
using PersonalFinanceManagement.Application.Contracts;
using PersonalFinanceManagement.Application.Services;
using PersonalFinanceManagement.Domain.Balances.Contracts.Balances;
using PersonalFinanceManagement.Domain.Balances.Contracts.Installments;
using PersonalFinanceManagement.Domain.Balances.Contracts.RefinanceBalances;
using PersonalFinanceManagement.Domain.Balances.Services.Balances;
using PersonalFinanceManagement.Domain.Balances.Services.Installments;
using PersonalFinanceManagement.Domain.Balances.Services.RefinancedBalances;
using PersonalFinanceManagement.Domain.Balances.Specifications;
using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Base.Services;
using PersonalFinanceManagement.Domain.Management.Contracts;
using PersonalFinanceManagement.Domain.Management.Specifications;
using PersonalFinanceManagement.Domain.Transactions.Contracts;
using PersonalFinanceManagement.Domain.Transactions.Services;
using PersonalFinanceManagement.Domain.Transactions.Specifications;
using PersonalFinanceManagement.Domain.Users.Contracts;
using PersonalFinanceManagement.Domain.Users.Services;
using PersonalFinanceManagement.Domain.Users.Specifications;
using PersonalFinanceManagement.Infra.Data.Helpers;
using PersonalFinanceManagement.Infra.Data.Repositories.Balances;
using PersonalFinanceManagement.Infra.Data.Repositories.Transactions;
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

            services.AddScoped<IBalanceRepository, BalanceRepository>();
            services.AddScoped<IBalanceSpecification, BalanceSpecification>();
            services.AddScoped<IBalanceStore, BalanceStore>();
            services.AddScoped<IBalanceDeleter, BalanceDeleter>();

            services.AddScoped<IInstallmentRepository, InstallmentRepository>();
            services.AddScoped<IInstallmentWithBalanceAndTransactionsSpecification, InstallmentWithBalanceAndTransactionsSpecification>();
            services.AddScoped<IInstallmentStore, InstallmentStore>();
            services.AddScoped<IInstallmentDeleter, InstallmentDeleter>();
            services.AddScoped<IBalanceInstallmentStore, BalanceInstallmentStore>();

            services.AddScoped<IRefinancedBalanceRepository, RefinancedBalanceRepository>();
            services.AddScoped<IRefinancedBalanceStore, RefinancedBalanceStore>();

            services.AddScoped<ITransactionStore, TransactionStore>();
            services.AddScoped<ITransactionItemStore, TransactionItemStore>();
            services.AddScoped<ITransactionResidueStore, TransactionResidueStore>();
            services.AddScoped<ITransactionItemManager, TransactionItemManager>();
            services.AddScoped<IBalancesAsTransactionValueHandler, BalancesAsTransactionValueHandler>();
            services.AddScoped<ITransactionItemStorageHandler, TransactionItemStorageHandler>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<ITransactionItemRepository, TransactionItemRepository>();
            services.AddScoped<ITransactionResidueRepository, TransactionResidueRepository>();
            services.AddScoped<ITransactionWithTransactionItemsSpecification, TransactionWithTransactionItemsSpecification>();
            services.AddScoped<ITransactionDeleter, TransactionDeleter>();
            services.AddScoped<ITransactionItemDeleter, TransactionItemDeleter>();

            services.AddScoped<IManagementService, ManagementService>();
            services.AddScoped<IManagementSpecification, ManagementSpecification>();
        }
    }
}
