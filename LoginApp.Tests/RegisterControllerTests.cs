using NUnit.Framework;
using Moq;
using Microsoft.AspNetCore.Mvc;
using LoginApp.Controllers;
using LoginApp.Services;
using LoginApp.ViewModels;
using LoginApp.Models;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using LoginApp.Repositories;
namespace LoginApp.Tests.Controllers
{
    [TestFixture]
    public class RegisterControllerTests
    {
        private Mock<IUserRepository> _mockUserRepository;
        private UserService _userService;
        private RegisterController _controller;

        [SetUp]
        public void Setup()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _userService = new UserService(_mockUserRepository.Object);
            _controller = new RegisterController(_userService);
        }

        [Test]
        public void Register_ReturnsViewResult_ForGetRequest()
        {
            // Act
            var result = _controller.Register();

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public async Task Register_ReturnsRedirectToNextPage_WhenModelStateIsValid()
        {
            // Arrange
            var model = new RegisterViewModel
            {
                Username = "testuser",
                Password = "password"
            };
            _mockUserRepository.Setup(repo => repo.AddUserAsync(It.IsAny<User>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Register(model);

            // Assert
            Assert.That(result, Is.TypeOf<RedirectToActionResult>());
            var redirectResult = result as RedirectToActionResult;
            Assert.That(redirectResult.ActionName, Is.EqualTo("NextPage"));
        }

        [Test]
        public async Task Register_ReturnsViewResult_WhenModelStateIsInvalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("Username", "Required");

            // Act
            var result = await _controller.Register(new RegisterViewModel());

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public void MapUsers_ReturnsViewResult_WithMappedUsers()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Username = "user1" },
                new User { Username = "user2" }
            };
            _mockUserRepository.Setup(repo => repo.Map(It.IsAny<Func<User, User>>())).Returns((Func<User, User> mapFunction) =>
            {
                foreach (var user in users)
                {
                    mapFunction(user);
                }
                return users;
            });

            // Act
            var result = _controller.MapUsers();

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
            var viewResult = result as ViewResult;
            Assert.That(viewResult.Model, Is.TypeOf<List<User>>());
            var model = viewResult.Model as List<User>;
            Assert.That(model.Count, Is.EqualTo(2));
            Assert.That(model[0].Username, Is.EqualTo("USER1"));
            Assert.That(model[1].Username, Is.EqualTo("USER2"));
        }

        [Test]
        public void MapUsers_ReturnsViewResult_WithErrorMessage_OnException()
        {
            // Arrange
            _mockUserRepository.Setup(repo => repo.Map(It.IsAny<Func<User, User>>())).Throws(new Exception());

            // Act
            var result = _controller.MapUsers();

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
            var viewResult = result as ViewResult;
            Assert.That(viewResult.Model, Is.TypeOf<List<User>>());
            Assert.That((viewResult.Model as List<User>).Count, Is.EqualTo(0));
            Assert.That(_controller.ViewBag.ErrorMessage, Is.EqualTo("An error occurred while mapping users. Please try again later."));
        }
    }
}
