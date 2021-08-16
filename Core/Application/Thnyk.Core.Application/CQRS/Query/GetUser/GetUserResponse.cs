using System;
using System.Collections.Generic;
using System.Text;

namespace Thnyk.Core.Application.CQRS.Query.GetUser
{
    public class GetUserResponse
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
