using FluentValidation.TestHelper;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Thnyk.Core.Application.CQRS.Command.AddUser;
using Thnyk.Core.Application.CQRS.Command.DeleteUser;
using Thnyk.Core.Application.CQRS.Command.EditUser;
using Thnyk.Core.Application.Exceptions;
using Thnyk.Core.Domain.Entities;
using Thnyk.User.Api.Controllers;
using Xunit;

namespace Users.Api.Tests
{
    public class ApiControllerTest
    {
        private readonly Mock<IMediator> _mediatorMock;
        private AddUserValidator addvalidator;

        private AddUserCommand FakeAddUserCommand()
        {
            return new AddUserCommand
            {
                Name = "Fake User",
                Hobbies = "Fake Hobbies",
                Hometown = "Fake Hometown",
                Job = "Fake Job",
                Motto = "Fake Motto",
                PersonalBlog = "Fake Personal Blog",
                UserPicture = "Fake User Pictuew"
            };
        }

        private EditUserCommand FakeEditUserCommand()
        {
            return new EditUserCommand
            {
                Id = 1,
                Name = "Fake User",
                Hobbies = "Fake Hobbies",
                Hometown = "Fake Hometown",
                Job = "Fake Job",
                Motto = "Fake Motto",
                PersonalBlog = "Fake Personal Blog",
                UserPicture = "Fake User Pictuew"
            };
        }

        private User FakeUser()
        {
            return new User()
            {
                Id = 1,
                Name = "Fake User",
                Hobbies = "Fake Hobbies",
                Hometown = "Fake Hometown",
                Job = "Fake Job",
                Motto = "Fake Motto",
                PersonalBlog = "Fake Personal Blog",
                UserPicture = "Fake User Pictuew"
            };
        }

        public ApiControllerTest()
        {
            _mediatorMock = new Mock<IMediator>();
            addvalidator = new AddUserValidator();
        }

        [Fact]
        public async Task Add_User_success()
        {
            var fakeUser = FakeUser();

            var fakeResponse = 1;
            //Arrange
            _mediatorMock.Setup(x => x.Send(It.IsAny<AddUserCommand>(), default(CancellationToken)))
                .Returns(Task.FromResult(fakeResponse));

            //Act
            var controller = new UsersController(_mediatorMock.Object);

            var actionResult = await controller.Add(FakeAddUserCommand());

            //Assert
            Assert.Equal(fakeUser.Id, actionResult);
        }

        [Fact]
        public void Should_have_error_when_Name_is_null()
        {
            var model = new AddUserCommand { Name = null };
            var result = addvalidator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(user => user.Name);
        }

        [Fact]
        public void Should_not_have_error_when_request_is_correct()
        {
            var model = FakeAddUserCommand();
            var result = addvalidator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(person => person.Name);
        }

        [Fact]
        public async Task Edit_User_success()
        {
            var fakeUser = FakeUser();

            var fakeResponse = true;
            //Arrange
            _mediatorMock.Setup(x => x.Send(It.IsAny<EditUserCommand>(), default(CancellationToken)))
                .Returns(Task.FromResult(fakeResponse));

            //Act
            var controller = new UsersController(_mediatorMock.Object);

            var actionResult = await controller.Edit(FakeEditUserCommand());

            //Assert
            Assert.True(actionResult);
        }


        [Fact]
        public async Task Delete_User_success()
        {

            var fakeResponse = true;
            //Arrange
            _mediatorMock.Setup(x => x.Send(It.IsAny<DeleteUserCommand>(), default(CancellationToken)))
                .Returns(Task.FromResult(fakeResponse));

            //Act
            var controller = new UsersController(_mediatorMock.Object);

            var actionResult = await controller.Delete(1);

            //Assert
            Assert.True(actionResult);
        }

    }
}
