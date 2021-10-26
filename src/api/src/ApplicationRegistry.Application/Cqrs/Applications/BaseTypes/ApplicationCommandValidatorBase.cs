using FluentValidation;

namespace ApplicationRegistry.Application.Commands
{
    public abstract class ApplicationCommandValidatorBase<T> : AbstractValidator<T>
        where T : ApplicationCommandBase
    {
        protected ApplicationCommandValidatorBase()
        {
            RuleFor(e => e.Name).IsName();
            RuleFor(e => e.Code).NotEmpty().NotNull().MaximumLength(160);
            RuleFor(e => e.Description).IsDescription();
            RuleFor(e => e.Owner).MaximumLength(250);
            RuleFor(e => e.RepositoryUrl).MaximumLength(400);
            RuleFor(e => e.BuildProcessUrls).MaximumLength(400);
            RuleFor(e => e.Framework).MaximumLength(250);
        }
    }

}
