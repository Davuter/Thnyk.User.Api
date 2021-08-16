using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Thnyk.Core.Domain.Interfaces;

namespace Thnyk.Core.Application.CQRS.Query.GetUser
{
    public class GetUserQueryValidator : AbstractValidator<GetUserQuery>
    {

        public GetUserQueryValidator()
        {
            RuleFor(r => r.Id).NotEmpty().NotNull().GreaterThan(0);
        }
    }
}
