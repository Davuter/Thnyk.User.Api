using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Thnyk.Core.Domain.Interfaces;

namespace Thnyk.Core.Application.CQRS.Query.GetAllUser
{
    public class GetAllQueryHandler : IRequestHandler<GetAllUserQuery, GetAllUserQueryResponse>
    {
        private IUserRepository _userRepository;
        private IMapper _mapper;

        public GetAllQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<GetAllUserQueryResponse> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            var list =await _userRepository.GetAllAsync();
            GetAllUserQueryResponse response = new GetAllUserQueryResponse();
            foreach (var item in list)
            {
                UserListItem userListItem = new UserListItem
                {
                    Id = item.Id,
                    Job = item.Job,
                    Name = item.Name,
                    UserPicture = item.UserPicture
                };
                response.Users.Add(userListItem);
            }

            return response;
        }
    }
}
