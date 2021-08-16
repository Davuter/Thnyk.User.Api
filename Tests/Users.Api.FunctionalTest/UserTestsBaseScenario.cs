using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.Json;
using Thnyk.Core.Application.CQRS.Command.AddUser;
using Thnyk.Core.Application.CQRS.Command.EditUser;
using Thnyk.Users.Infrastructure;

namespace Users.Api.FunctionalTest
{
    public class UserTestsBaseScenario
    {
        public TestServer CreateServer()
        {
            var path = Assembly.GetAssembly(typeof(UserTestsBaseScenario))
                .Location;

            var hostBuilder = new WebHostBuilder().UseStartup<TestUserApiStartup>();

            var testServer = new TestServer(hostBuilder);

            return testServer;
        }

        public static class Get
        {
            public static string Users = "api/users";

            public static string GetById(int id)
            {
                return $"api/users/{id}";
            }
        }

        public static class Post
        {
            public static string AddUser = "api/users";
          
        }

        public static class Put
        {
            public static string EditUser = "api/users";
        }

        public static class Delete
        {
            public static string DeletUser(int id)
            {
                return $"api/users/{id}";
            }
        }

        public string  FakeAddUserCommand()
        {
            return JsonSerializer.Serialize(new AddUserCommand
            {
                Name = "Fake User",
                Hobbies = "Fake Hobbies",
                Hometown = "Fake Hometown",
                Job = "Fake Job",
                Motto = "Fake Motto",
                PersonalBlog = "Fake Personal Blog",
                UserPicture = "Fake User Pictuew"
            });
        }


        public string FakeEditUserCommand()
        {
            return JsonSerializer.Serialize(new EditUserCommand
            {
                Id = 3,
                Name = "Fake User",
                Hobbies = "Fake Hobbies",
                Hometown = "Fake Hometown",
                Job = "Fake Job",
                Motto = "Fake Motto",
                PersonalBlog = "Fake Personal Blog",
                UserPicture = "Fake User Pictuew"
            });
        }
    }
}
