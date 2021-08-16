using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Thnyk.Core.Application.CQRS.Query.GetUser
{
    public class GetUserQuery : IRequest<GetUserResponse>
    {
        public int Id { get; set; }
    }
}
