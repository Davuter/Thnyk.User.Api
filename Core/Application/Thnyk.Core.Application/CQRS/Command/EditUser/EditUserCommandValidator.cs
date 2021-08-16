using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Thnyk.Core.Application.CQRS.Command.EditUser
{
    public class EditUserCommandValidator : AbstractValidator<EditUserCommand>
    {
        public EditUserCommandValidator()
        {
            RuleFor(r => r.Id).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(r => r.Name).NotEmpty().NotNull();
            RuleFor(r => r.Job).NotNull().NotEmpty();
            RuleFor(r => r.UserPicture).NotEmpty().NotNull();
        }
    }
}
