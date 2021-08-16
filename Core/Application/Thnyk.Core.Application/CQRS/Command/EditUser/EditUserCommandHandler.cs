using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Thnyk.Core.Domain.Entities;
using Thnyk.Core.Domain.Interfaces;

namespace Thnyk.Core.Application.CQRS.Command.EditUser
{
    public class EditUserCommandHandler : IRequestHandler<EditUserCommand, bool>
    {
        private IUserRepository _userRepository;
        private IMapper _mapper;
        public EditUserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<bool> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request);
            user.ModifiedDate = DateTime.Now;
            _userRepository.Update(user);
            return await _userRepository.UnitofWork.SaveEntitiesAsync();
        }
    }
}
