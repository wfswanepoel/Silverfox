using Application.Infrastructure.Validation.Interfaces;
using Common.Helpers;
using Domain.Constants;
using FluentValidation;

namespace Application.Infrastructure.Validation.CultureName
{
    public class RequireCultureValidator : AbstractValidator<IRequireCulture>
    {
        public RequireCultureValidator()
        {
            RuleFor(x => x.CultureName)
                .NotNull().WithMessage(ErrorDetailConstants.HeaderFieldRequired)
                .NotEmpty().WithMessage(ErrorDetailConstants.HeaderFieldEmpty)
                .Must(m => ContextResolver.GetContextByCultureName(m) != null).WithMessage(ErrorDetailConstants.HeaderFieldNotSupported);
        }
    }
}
