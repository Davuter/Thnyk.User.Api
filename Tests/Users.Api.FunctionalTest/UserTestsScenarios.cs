using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Thnyk.Core.Application.CQRS.Command.AddUser;
using Thnyk.Core.Application.CQRS.Query.GetUser;
using Xunit;

namespace Users.Api.FunctionalTest
{
    public class UserTestsScenarios : UserTestsBaseScenario
    {
        [Fact]
        public async Task Get_get_all_user_response_ok_status_code()
        {
            using (var server = CreateServer())
            {
                var response = await server.CreateClient()
                    .GetAsync(Get.Users);

                response.EnsureSuccessStatusCode();
            }
        }

        [Fact]
        public async Task Get_get_user_byid_response_ok_status_code()
        {
            using (var server = CreateServer())
            {
                var response = await server.CreateClient()
                    .GetAsync(Get.GetById(3));

                response.EnsureSuccessStatusCode();
            }
        }

        [Fact]
        public async Task Get_get_user_byid_response_notfound_status_code()
        {
            using (var server = CreateServer())
            {
                var response = await server.CreateClient()
                    .GetAsync(Get.GetById(100));

                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            }
        }

        [Fact]
        public async Task Get_get_user_byid_response_badrequest_status_code()
        {
            using (var server = CreateServer())
            {
                var response = await server.CreateClient()
                    .GetAsync(Get.GetById(-1));

                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }
        }

        [Fact]
        public async Task Post_new_user_response_badrequest_status_code()
        {
            using (var server = CreateServer())
            {
                var content = new StringContent(JsonConvert.SerializeObject(new AddUserCommand { Name = null}), UTF8Encoding.UTF8, "application/json");
                var response = await server.CreateClient()
                    .PostAsync(Post.AddUser, content);

                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }
        }

        [Fact]
        public async Task Post_new_user_response_success_status_code()
        {
            using (var server = CreateServer())
            {
                var content = new StringContent(FakeAddUserCommand(), UTF8Encoding.UTF8, "application/json");
                var response = await server.CreateClient()
                    .PostAsync(Post.AddUser, content);

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task Post_new_user_and_check_new_user_success_status_code()
        {
            using (var server = CreateServer())
            {
                var content = new StringContent(FakeAddUserCommand(), UTF8Encoding.UTF8, "application/json");
                var response = await server.CreateClient()
                    .PostAsync(Post.AddUser, content);
                response.EnsureSuccessStatusCode();
                var result = Convert.ToInt32(await response.Content.ReadAsStringAsync());

                var userresponse = await server.CreateClient()
                 .GetAsync(Get.GetById(result));

                Assert.Equal(HttpStatusCode.OK, userresponse.StatusCode);
            }
        }

        [Fact]
        public async Task Put_user_response_success_status_code()
        {
            using (var server = CreateServer())
            {
                var content = new StringContent(FakeEditUserCommand(), UTF8Encoding.UTF8, "application/json");
                var response = await server.CreateClient()
                    .PutAsync(Put.EditUser, content);

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task Put_user_and_check_user()
        {
            using (var server = CreateServer())
            {
                var content = new StringContent(FakeEditUserCommand(), UTF8Encoding.UTF8, "application/json");
                var response = await server.CreateClient()
                    .PutAsync(Put.EditUser, content);

                response.EnsureSuccessStatusCode();


                var userresponse = await server.CreateClient()
                 .GetStringAsync(Get.GetById(3));

                var user = JsonConvert.DeserializeObject<GetUserResponse>(userresponse);

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.Equal("Fake User", user.Name);
            }
        }

        [Fact]
        public async Task Delete_user_response_badrequest_status_code()
        {
            using (var server = CreateServer())
            {
                var response = await server.CreateClient()
                    .DeleteAsync(Delete.DeletUser(-1));

                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }
        }

        [Fact]
        public async Task Delete_user_response_notfound_status_code()
        {
            using (var server = CreateServer())
            {
                var response = await server.CreateClient()
                    .DeleteAsync(Delete.DeletUser(100));

                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            }
        }

        [Fact]
        public async Task Delete_user_response_OK_status_code()
        {
            using (var server = CreateServer())
            {
                var content = new StringContent(FakeAddUserCommand(), UTF8Encoding.UTF8, "application/json");
                var response = await server.CreateClient()
                    .PostAsync(Post.AddUser, content);
                response.EnsureSuccessStatusCode();
                var result = Convert.ToInt32(await response.Content.ReadAsStringAsync());

                var deleteresponse = await server.CreateClient()
                    .DeleteAsync(Delete.DeletUser(result));

                Assert.Equal(HttpStatusCode.OK, deleteresponse.StatusCode);
            }
        }

        [Fact]
        public async Task Delete_user__and_check_user_nofound_status_code()
        {
            using (var server = CreateServer())
            {
                var content = new StringContent(FakeAddUserCommand(), UTF8Encoding.UTF8, "application/json");
                var response = await server.CreateClient()
                    .PostAsync(Post.AddUser, content);
                response.EnsureSuccessStatusCode();
                var result = Convert.ToInt32(await response.Content.ReadAsStringAsync());

                var deletresponse = await server.CreateClient()
                    .DeleteAsync(Delete.DeletUser(result));

                var userresponse = await server.CreateClient()
                    .GetAsync(Get.GetById(result));

                Assert.Equal(HttpStatusCode.NotFound, userresponse.StatusCode);
            }
        }
    }


}
