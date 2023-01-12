using FluentValidation.Results;
using PersonalFinanceManagement.Domain.Base.Validators;
using PersonalFinanceManagement.Domain.Base.Contracts;

namespace PersonalFinanceManagement.Domain.Base.Entites
{
    public abstract class Entity<TId> : IEntity
        where TId : struct
    {
        public List<ValidationFailure> Errors { get; protected set; } = new();
        protected EntityValidator<Entity<TId>>? Validator;

        public virtual TId Id { get; set; }
        public bool IsRecorded => Id.Equals(default(TId)) is false;
        public bool WithRegistrationDates => this is IEntityWithRegistrationDates;
        public bool WithSoftDelete => this is IEntityWithSoftDelete;

        protected Entity()
        {
            SetValidator();
            SetValitionRules();
        }

        public bool Validate()
        {
            if (Validator is null)
                return true;

            var validation = Validator.Validate(this);
            Errors = validation.Errors;

            return validation.IsValid;
        }

        protected abstract void SetValitionRules();

        private void SetValidator()
        {
            Validator = new EntityValidator<Entity<TId>>();
        }
    }
}
