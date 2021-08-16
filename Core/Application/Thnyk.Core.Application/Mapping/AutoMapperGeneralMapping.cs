using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Thnyk.Core.Application.CQRS.Command.AddUser;
using Thnyk.Core.Application.CQRS.Command.EditUser;
using Thnyk.Core.Application.CQRS.Query.GetUser;
using Thnyk.Core.Domain.Entities;

namespace Thnyk.Core.Application.Mapping
{
    public class AutoMapperGeneralMapping : Profile
    {
        public AutoMapperGeneralMapping()
        {
            CreateMap<AddUserCommand, User>().ReverseMap();

            CreateMap<EditUserCommand, User>().ReverseMap();

            CreateMap<GetUserResponse, User>().ReverseMap();
        }
    }
}
