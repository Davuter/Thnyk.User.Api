using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Thnyk.Core.Application.CQRS.Query.GetAllUser
{
    public class GetAllUserQuery : IRequest<GetAllUserQueryResponse>
    {
    }
}
