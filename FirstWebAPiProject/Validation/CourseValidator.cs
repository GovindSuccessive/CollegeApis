using FirstClassLibrary.Entity;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace FirstWebAPiProject.Validation
{
    public class CourseValidator: AbstractValidator<Course>
    {
        public CourseValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Name Do not Empty")
                .MinimumLength(8).WithMessage("Name Length Should be Greater than 8 Character")
                .MaximumLength(20).WithMessage("Name Length Should be Smaller than 20 Character")
                .NotNull().WithMessage("Name Should not be empty");

            RuleFor(c => c.Description)
                .NotEmpty().WithMessage("Description Do not Empty")
                .MaximumLength(200).WithMessage("Name Length Should be Smaller than 100 Character")
                .NotNull().WithMessage("Name Should not be empty");

        }
    }
}
