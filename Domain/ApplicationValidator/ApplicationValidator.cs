using Domain.Constants;
using Domain.Entity;
using FluentValidation;

namespace DAL.ApplicationValidator;

public class ApplicationValidator : AbstractValidator<Application>
{
    public ApplicationValidator()
    {
        RuleFor(a => a.Id).NotEmpty();
        RuleFor(a => a.Author).NotEmpty();
        RuleFor(a => a.ActivityType).NotEmpty();
        RuleFor(a => a.Name).NotEmpty().MaximumLength(ConstantsForApplication.NameLength);
        RuleFor(a => a.Description).MaximumLength(ConstantsForApplication.DescriptionLength);
        RuleFor(a => a.Outline).NotEmpty().MaximumLength(ConstantsForApplication.OutlineLength);
    }
}