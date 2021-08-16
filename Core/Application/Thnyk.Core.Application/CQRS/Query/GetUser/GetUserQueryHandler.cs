using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Thnyk.Core.Domain.Interfaces;
using Thnyk.Core.Application.Exceptions;

namespace Thnyk.Core.Application.CQRS.Query.GetUser
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery,GetUserResponse>
    {
        private IUserRepository _userRepository;
        private IMapper _mapper;

        public GetUserQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<GetUserResponse> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(request.Id);
            if (user== null)
            {
                throw new NotFoundException("User", request.Id);
            }
            return _mapper.Map<GetUserResponse>(user);
        }
    }
}
