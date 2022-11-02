using FluentValidation;

namespace PersonalFinanceManagement.Domain.Base.Validators
{
    public class EntityValidator<TEntity> : AbstractValidator<TEntity>
        where TEntity : class
    {
    }
}
