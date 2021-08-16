using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Thnyk.Core.Application.CQRS.Command.AddUser
{
    public class AddUserCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string UserPicture { get; set; }
        public string Job { get; set; }
        public string Hobbies { get; set; }
        public string Motto { get; set; }
        public string Hometown { get; set; }
        public string PersonalBlog { get; set; }
    }
}
