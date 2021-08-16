using AutoMapper;
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
using Thnyk.Core.Domain.Interfaces;
using Xunit;

namespace Users.Api.Tests.Application
{
    public class HandlerTest
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        public HandlerTest()
        {
            _mediatorMock = new Mock<IMediator>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _mapperMock = new Mock<IMapper>();
        }

        private AddUserCommand FakeAddUserCommand()
        {
            return new AddUserCommand
            {
                Name ="Fake User",
                Hobbies="Fake Hobbies",
                Hometown ="Fake Hometown",
                Job ="Fake Job",
                Motto = "Fake Motto",
                PersonalBlog ="Fake Personal Blog",
                UserPicture ="Fake User Pictuew"
            };
        }

        private EditUserCommand FakeEditUserCommand()
        {
            return new EditUserCommand
            {
                Id=1,
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

        #region Add User Handler Test Methods

        [Fact]
        public async Task Handle_AddUser_success()
        {
            //Arrange
            var fakeaddUserCommand = FakeAddUserCommand();

            var fakeUser = FakeUser();

            _mapperMock.Setup(x => x.Map<User>(It.IsAny<AddUserCommand>())).Returns(Task.FromResult(fakeUser).Result);

            _userRepositoryMock.Setup(x => x.Add(It.IsAny<User>())).Returns(Task.FromResult(fakeUser.Id));

            _userRepositoryMock.Setup(x => x.UnitofWork.SaveEntitiesAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(true));

            //Act
            var handler = new AddUserCommandHandler(_userRepositoryMock.Object, _mapperMock.Object);
            var cltToken = new System.Threading.CancellationToken();
            var result = await handler.Handle(fakeaddUserCommand, cltToken);

            //Assert
            Assert.Equal(result, fakeUser.Id);
        }
        #endregion

        #region Edit User Handler Test Methods

        [Fact]
        public async Task Handle_EditUser_success()
        {
            //Arrange
            var fakeeditUserCommand = FakeEditUserCommand();

            var fakeUser = FakeUser();

            _mapperMock.Setup(x => x.Map<User>(It.IsAny<EditUserCommand>())).Returns(Task.FromResult(fakeUser).Result);

            _userRepositoryMock.Setup(x => x.Update(It.IsAny<User>()));

            _userRepositoryMock.Setup(x => x.UnitofWork.SaveEntitiesAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(true));

            //Act
            var handler = new EditUserCommandHandler(_userRepositoryMock.Object, _mapperMock.Object);
            var cltToken = new System.Threading.CancellationToken();
            var result = await handler.Handle(fakeeditUserCommand, cltToken);

            //Assert
            Assert.True(result);
        }
        #endregion

        #region Edit User Handler Test Methods

        [Fact]
        public async Task Handle_DeleteUser_success()
        {
            //Arrange
            var fakeCommand = new DeleteUserCommand() { 
                Id = 1
            };

            var fakeUser = FakeUser();

            _userRepositoryMock.Setup(x => x.GetAsync(fakeCommand.Id)).Returns(Task.FromResult(fakeUser));
            _userRepositoryMock.Setup(x => x.Delete(It.IsAny<User>()));

            _userRepositoryMock.Setup(x => x.UnitofWork.SaveEntitiesAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(true));

            //Act
            var handler = new DeleteUserCommandHandler(_userRepositoryMock.Object);
            var cltToken = new System.Threading.CancellationToken();
            var result = await handler.Handle(fakeCommand, cltToken);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task Handle_DeleteUser_Throw_NotfoundException()
        {
            //Arrange
            var fakeCommand = new DeleteUserCommand()
            {
                Id = 1
            };


            _userRepositoryMock.Setup(x => x.GetAsync(fakeCommand.Id)).Returns(Task.FromResult((User)null));

            //Act
            var handler = new DeleteUserCommandHandler(_userRepositoryMock.Object);
            var cltToken = new System.Threading.CancellationToken();
            var result = await Assert.ThrowsAsync<NotFoundException>(() =>handler.Handle(fakeCommand, cltToken));

            //Assert
            Assert.True(result is NotFoundException);
        }
        #endregion
    }
}
