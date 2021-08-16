using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Thnyk.Core.Application.Exceptions;
using Thnyk.Core.Domain.Interfaces;

namespace Thnyk.Core.Application.CQRS.Command.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private IUserRepository _userRepository;

        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(request.Id);
            if (user == null)
            {
                throw new NotFoundException("User", request.Id);
            }
            _userRepository.Delete(user);
            return await _userRepository.UnitofWork.SaveEntitiesAsync();
        }
    }
}
