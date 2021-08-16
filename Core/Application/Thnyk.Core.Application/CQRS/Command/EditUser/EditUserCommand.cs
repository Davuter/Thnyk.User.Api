using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Thnyk.Core.Application.CQRS.Command.EditUser
{
    public class EditUserCommand :IRequest<bool>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Job { get; set; }
        public string UserPicture { get; set; }
        public string Hobbies { get; set; }
        public string Motto { get; set; }
        public string Hometown { get; set; }
        public string PersonalBlog { get; set; }
    }
}
