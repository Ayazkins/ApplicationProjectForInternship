using Domain.Constants;
using Domain.Entity;
using Domain.Requests;
using FluentValidation;

namespace DAL.ApplicationValidator;

public class UnsubmittedApplicationValidator : AbstractValidator<Application>
{
    public UnsubmittedApplicationValidator()
    {
        RuleFor(a => a.Author)
            .NotEmpty()
            .WithMessage("You can not create application without author");
        RuleFor(a => a.Name).MaximumLength(ConstantsForApplication.NameLength);
        RuleFor(a => a.Outline).MaximumLength(ConstantsForApplication.OutlineLength);
        RuleFor(a => a.Description).MaximumLength(ConstantsForApplication.DescriptionLength);
        RuleFor(a => a.SendAt).Null().WithMessage("Application already commited");
        RuleFor(a => a)
            .Must(a => a.Description != null
                       || a.Outline != null
                       || a.ActivityType != null
                       || a.Name != null)
            .WithMessage("Fill more fields");
    }
}