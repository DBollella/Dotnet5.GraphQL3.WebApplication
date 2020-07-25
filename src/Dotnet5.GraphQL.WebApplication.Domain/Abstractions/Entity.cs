using FluentValidation;
using FluentValidation.Results;

namespace Dotnet5.GraphQL.WebApplication.Domain.Abstractions
{
    public abstract class Entity<TId>
        where TId : struct
    {
        public TId Id { get; protected set; }
        public bool IsValid => ValidationResult?.IsValid ?? default;
        public ValidationResult ValidationResult { get; private set; }

        protected void Validate<TEntity>(TEntity entity, AbstractValidator<TEntity> validator)
            => ValidationResult = validator.Validate(entity);
    }
}