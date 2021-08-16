using System;
using System.Collections.Generic;
using System.Text;

namespace Thnyk.Core.Application.CQRS.Query.GetAllUser
{
    public class GetAllUserQueryResponse
    {
        public List<UserListItem> Users { get; set; }
        public GetAllUserQueryResponse()
        {
            Users = new List<UserListItem>();
        }
    }

    public class UserListItem
    {
        public int Id { get; set; }
        public string UserPicture { get; set; }
        public string Name { get; set; }
        public string Job { get; set; }
    }
}
