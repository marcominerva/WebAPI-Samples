using CalendarApi.Shared.Models;
using FluentValidation;

namespace CalendarApi.BusinessLayer.Validations
{
    public class EventValidator : AbstractValidator<Event>
    {
        public EventValidator()
        {
            RuleFor(e => e.Name).NotEmpty().WithMessage("The name of the event is required").MaximumLength(50).WithMessage("The name of the event must be less than 50 characters");
            RuleFor(e => e.Description).MaximumLength(4000).WithMessage("The description of the event must be less than 4000 characters");
        }
    }
}
