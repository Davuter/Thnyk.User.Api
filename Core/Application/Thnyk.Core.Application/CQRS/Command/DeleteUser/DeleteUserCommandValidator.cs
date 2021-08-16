using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Thnyk.Core.Application.CQRS.Command.DeleteUser
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(r => r.Id).NotEmpty().NotNull().GreaterThan(0);
        }
    }
}
