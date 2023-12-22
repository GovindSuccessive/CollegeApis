using FirstClassLibrary.Entity;
using FirstWebAPiProject.Model.Dto;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations;

namespace FirstWebAPiProject.Validation
{
    public class CourseValidator: AbstractValidator<CourseDto>
    {
        public CourseValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Name Do not Empty").WithErrorCode("404")
                .MaximumLength(20).WithMessage("Name Length Should be Smaller than 20 Character")
                .NotNull().WithMessage("Name Should not be empty");

            RuleFor(c => c.Description)
                .NotEmpty().WithMessage("Description Do not Empty")
                .MaximumLength(200).WithMessage("Name Length Should be Smaller than 100 Character")
                .NotNull().WithMessage("Name Should not be empty");

        }
    }
}