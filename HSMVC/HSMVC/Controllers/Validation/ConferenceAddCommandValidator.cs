using FluentValidation;
using FluentValidation.Results;
using HSMVC.Controllers.Commands;

namespace HSMVC.Controllers.Validation
{
    public class ConferenceAddCommandValidator : AbstractValidator<ConferenceAddCommand>
    {
        public ConferenceAddCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(ConferenceValidatorHelper.RequiredMessage("Name"));
            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage(ConferenceValidatorHelper.RequiredMessage("StartDate"))
                .Must(ConferenceValidatorHelper.IsAValidDate).WithMessage(ConferenceValidatorHelper.NotAValidDateMessage("StartDate"));
            RuleFor(x => x.EndDate)
                .NotEmpty().WithMessage(ConferenceValidatorHelper.RequiredMessage("EndDate"))
                .Must(ConferenceValidatorHelper.IsAValidDate).WithMessage(ConferenceValidatorHelper.NotAValidDateMessage("EndDate"));
            RuleFor(x => x.Cost).NotNull().WithMessage(ConferenceValidatorHelper.RequiredMessage("Cost"));

            Custom(conference => ConferenceValidatorHelper.DoesConferenceNameAlreadyExist(conference.Name)
                ? new ValidationFailure("Name", string.Format("The conference {0} name already exists.", conference.Name))
                : null);
        }
    }
}