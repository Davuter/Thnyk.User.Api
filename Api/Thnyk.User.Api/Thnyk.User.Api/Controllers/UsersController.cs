using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thnyk.Core.Application.CQRS.Command.AddUser;
using Thnyk.Core.Application.CQRS.Command.DeleteUser;
using Thnyk.Core.Application.CQRS.Command.EditUser;
using Thnyk.Core.Application.CQRS.Query.GetAllUser;
using Thnyk.Core.Application.CQRS.Query.GetUser;

namespace Thnyk.User.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IMediator _mediator;
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<int> Add(AddUserCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut]
        public async Task<bool> Edit(EditUserCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpDelete]
        [Route("{Id}")]
        public async Task<bool> Delete(int Id)
        {
            DeleteUserCommand command = new DeleteUserCommand()
            {
                Id = Id
            };
            return await _mediator.Send(command);
        }

        [HttpGet]
        public async Task<GetAllUserQueryResponse> GetAll()
        {
            GetAllUserQuery command = new GetAllUserQuery();
            return await _mediator.Send(command);
        }

        [HttpGet]
        [Route("{Id}")]
        public async Task<GetUserResponse> Get(int Id)
        {
            GetUserQuery command = new GetUserQuery()
            {
                Id = Id
            };
            return await _mediator.Send(command);
        }

    }
}
