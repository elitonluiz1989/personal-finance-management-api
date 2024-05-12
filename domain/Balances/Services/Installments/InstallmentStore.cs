using Microsoft.EntityFrameworkCore;
using PersonalFinanceManagement.Domain.Balances.Contracts.Balances;
using PersonalFinanceManagement.Domain.Balances.Contracts.Installments;
using PersonalFinanceManagement.Domain.Balances.Dtos;
using PersonalFinanceManagement.Domain.Balances.Entities;
using PersonalFinanceManagement.Domain.Balances.Enums;
using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Base.Services;
using PersonalFinanceManagement.Domain.Managements.Entities;

namespace PersonalFinanceManagement.Domain.Balances.Services.Installments
{
    public class InstallmentStore : Store<Installment, int>, IInstallmentStore
    {
        private readonly IBalanceRepository _balanceRepository;

        public InstallmentStore(
            INotificationService notificationService,
            IInstallmentRepository repository,
            IBalanceRepository balanceRepository
        )
            : base(notificationService, repository)
        {
            _balanceRepository = balanceRepository;
        }

        public void Store(Installment? installment)
        {
            if (ValidateEntity(installment) is false)
                return;

            SaveEntity(installment);
        }

        public async Task Store(InstallmentStoreDto dto, Balance balance)
        {
            if (ValidateDto(dto) is false)
                return;

            var installment = await SetInstallment(dto, balance);

            Store(installment);
        }

        public async Task Store(InstallmentStoreDto dto)
        {
            if (ValidateDto(dto) is false)
                return;

            var balance = await _balanceRepository.Find(dto.BalanceId);

            if (balance is null)
            {
                AddNotification($"{balance} is null");

                return;
            }

            await Store(dto, balance);
        }

        public void Store(Installment? installment, Management management)
        {
            if (installment is null)
                return;

            installment.Management = management;

            Store(installment);
        }

        public async Task Store(int[] installmentsIds, Management management)
        {
            if (installmentsIds is null || installmentsIds.Length == 0)
                return;

            var installments = await _repository.Query()
                .Where(p => installmentsIds.Contains(p.Id))
                .ToListAsync();

            foreach (var installment in installments)
            {
                Store(installment, management);

                if (HasNotifications)
                    return;
            }
        }

        public void UpdateStatus(Installment installment, InstallmentStatusEnum status)
        {
            installment.Status = status;

            _repository.Update(installment);
        }

        public void UpdateStatus(IEnumerable<Installment> installments, InstallmentStatusEnum status)
        {
            foreach (var installment in installments)
            {
                _repository.Update(installment);
            }
        }

        private async Task<Installment?> SetInstallment(InstallmentStoreDto dto, Balance balance)
        {
            if (HasNotifications)
                return null;

            if (dto.IsRecorded)
                return await UpdateInstallment(dto);

            return CreateInstallment(dto, balance);
        }

        private static Installment? CreateInstallment(InstallmentStoreDto dto, Balance balance)
        {
            return new Installment()
            {
                BalanceId = balance.Id,
                Balance = balance,
                Reference = dto.Reference,
                Number = dto.Number,
                Amount = dto.Amount
            };
        }

        private async Task<Installment?> UpdateInstallment(InstallmentStoreDto dto)
        {
            var installment = await _repository.Find(dto.Id);

            if (installment is null)
            {
                AddNotification($"{nameof(installment)} is null");

                return null;
            }

            if (installment.Reference != dto.Reference)
                installment.Reference = dto.Reference;

            if (installment.Number != dto.Number)
                installment.Number = dto.Number;

            if (installment.Amount != dto.Amount)
                installment.Amount = dto.Amount;

            return installment;
        }
    }
}
