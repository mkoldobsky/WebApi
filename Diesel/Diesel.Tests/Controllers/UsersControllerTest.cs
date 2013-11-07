namespace Diesel.Tests.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using Diesel.Controllers;
    using Diesel.Core;
    using Diesel.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class UsersControllerTest
    {
        [TestMethod]
        public void ItShouldGetAllUsers()
        {
            // Arrange
            var repo = new Mock<IUserRepository>();
            repo.Setup(x => x.All).Returns(() => GetUsers().AsQueryable());
            var controller = new UsersController(repo.Object);

            // Act
            var result = controller.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("Player1", result.ElementAt(0).Username);
            Assert.AreEqual("Player2", result.ElementAt(1).Username);
        }

        [TestMethod]
        public void ItShouldGetById()
        {
            // Arrange
            var repo = new Mock<IUserRepository>();
            repo.Setup(x => x.Find(1)).Returns(GetUser1);
            var controller = new UsersController(repo.Object);

            // Act
            User result = controller.Get(1);

            // Assert
            Assert.AreEqual("Player1", result.Username);
        }

        [TestMethod]
        public void ItShouldCreateUserOnPost()
        {
            // Arrange
            var user = new User { Username = "Other" };
            var repo = new Mock<IUserRepository>();
            repo.Setup(x => x.InsertOrUpdate(It.IsAny<User>())).Verifiable();
            repo.Setup(x => x.Save()).Verifiable();
            var controller = new UsersController(repo.Object);
            HttpConfiguration configuration = new HttpConfiguration();
            var request = new Mock<HttpRequestMessage>();

            controller.Request = request.Object;
            controller.Request.Properties["MS_HttpConfiguration"] = configuration;

            // Act
            controller.Post(user);

            // Assert
            repo.Verify(x => x.InsertOrUpdate(user), Times.Once);
            repo.Verify(x => x.Save(), Times.Once);
        }

        [TestMethod]
        public void ItShouldUpdateUserOnPut()
        {
            // Arrange
            var repo = new Mock<IUserRepository>();
            repo.Setup(x => x.InsertOrUpdate(It.IsAny<User>())).Verifiable();
            repo.Setup(x => x.Save()).Verifiable();
            var controller = new UsersController(repo.Object);
            HttpConfiguration configuration = new HttpConfiguration();
            var request = new Mock<HttpRequestMessage>();

            controller.Request = request.Object;
            controller.Request.Properties["MS_HttpConfiguration"] = configuration;
            // Act
            var result = controller.Put(5, new User { Username = "Change" });

            // Assert
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
           
        }

        [TestMethod]
        public void ItShouldDelete()
        {
            // Arrange
            var user = new User {Id = 1};
            var repo = new Mock<IUserRepository>();
            repo.Setup(x => x.Find(It.IsAny<int>())).Returns(user);
            repo.Setup(x=>x.Delete(It.IsAny<int>())).Verifiable();
            var controller = new UsersController(repo.Object);
            HttpConfiguration configuration = new HttpConfiguration();
            var request = new Mock<HttpRequestMessage>();

            controller.Request = request.Object;
            controller.Request.Properties["MS_HttpConfiguration"] = configuration;


            // Act
            controller.Delete(5);

            // Assert
            repo.Verify(x=>x.Delete(5), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void ItShouldThrowExceptionWhenNotFindToDelete()
        {
            // Arrange
            var user = new User { Id = 1 };
            var repo = new Mock<IUserRepository>();
            repo.Setup(x => x.Find(It.IsAny<int>())).Returns(()=>null);
            repo.Setup(x => x.Delete(It.IsAny<int>())).Verifiable();
            var controller = new UsersController(repo.Object);
            HttpConfiguration configuration = new HttpConfiguration();
            var request = new Mock<HttpRequestMessage>();

            controller.Request = request.Object;
            controller.Request.Properties["MS_HttpConfiguration"] = configuration;


            // Act
            controller.Delete(5);

            // Assert
            repo.Verify(x => x.Delete(5), Times.Never);
        }

        [TestMethod]
        public void ItShouldAuthenticateWhenGettingActualUser()
        {
            var users = new List<User> {new User {Id = 1}};
            var repository = new Mock<IUserRepository>();
            repository.Setup(x => x.AllIncluding(It.IsAny<Expression<Func<User, object>>>())).Returns(
                () => users.AsQueryable());
            var controller = new UsersController(repository.Object);

            HttpConfiguration configuration = new HttpConfiguration();
            var  request = new Mock<HttpRequestMessage>();
            
            controller.Request = request.Object;
            controller.Request.Properties["MS_HttpConfiguration"] = configuration;

            var result = controller.Authenticate(new User { Username = "User", Password = "Password" });
            var user = result.Content.ReadAsAsync<User>();

            Assert.AreEqual(1, user.Id);
        }


        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void ITShouldThrowExceptionWhenNotFound()
        {
            var users = new List<User>();
            var repository = new Mock<IUserRepository>();
            repository.Setup(x => x.AllIncluding(It.IsAny<Expression<Func<User, object>>>())).Returns(
                () => users.AsQueryable());
            var controller = new UsersController(repository.Object);

            HttpConfiguration configuration = new HttpConfiguration();
            var request = new Mock<HttpRequestMessage>();

            controller.Request = request.Object;
            controller.Request.Properties["MS_HttpConfiguration"] = configuration;

            var result = controller.Authenticate(new User { Username = "User", Password = "Password" });
        }


        private List<User> GetUsers()
        {
            return new List<User> { GetUser1(), GetUser2() };
        }

        private User GetUser2()
        {
            return new User { Username = "Player2", Password = "Password2", MaxActiveGames = 2 };
        }

        private User GetUser1()
        {
            return new User { Username = "Player1", MaxActiveGames = 1, Password = "Password1" };
        }

        private UsersController GetRequestMockUsersController()
        {
            var users = new UsersController();

            HttpConfiguration configuration = new HttpConfiguration();
            HttpRequestMessage request = new HttpRequestMessage();
            users.Request = request;
            users.Request.Properties["MS_HttpConfiguration"] = configuration;
            return users;
        }
    }
}
