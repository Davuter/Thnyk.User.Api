using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Thnyk.Core.Application.CQRS.Command.AddUser
{
    public class AddUserValidator : AbstractValidator<AddUserCommand>
    {
        public AddUserValidator()
        {
            RuleFor(r => r.Name).NotEmpty().NotNull();
            RuleFor(r => r.Job).NotNull().NotEmpty();
            RuleFor(r => r.UserPicture).NotEmpty().NotNull();
        }
    }
}
