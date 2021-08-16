using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Thnyk.Core.Domain.Entities;
using Thnyk.Core.Domain.Interfaces;

namespace Thnyk.Core.Application.CQRS.Command.AddUser
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, int>
    {
        private IUserRepository _userRepository;
        private IMapper _mapper;
        public AddUserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request);
            await _userRepository.Add(user);
            await _userRepository.UnitofWork.SaveEntitiesAsync();
            return user.Id;
        }
    }
}
