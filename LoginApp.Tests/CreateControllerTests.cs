using NUnit.Framework;
using Moq;
using Microsoft.AspNetCore.Mvc;
using LoginApp.Controllers;
using LoginApp.Services;
using LoginApp.ViewModels;
using LoginApp.Models;
using System.Threading.Tasks;
using LoginApp.Repositories;

namespace LoginApp.Tests.Controllers
{
    [TestFixture]
    public class CreateControllerTests
    {
        private Mock<INutrientRepository> _mockNutrientRepository;
        private NutrientService _nutrientService;
        private CreateController _controller;

        [SetUp]
        public void Setup()
        {
            _mockNutrientRepository = new Mock<INutrientRepository>();
            _nutrientService = new NutrientService(_mockNutrientRepository.Object);
            _controller = new CreateController(_nutrientService);
        }

        [Test]
        public void Create_ReturnsViewResult_ForGetRequest()
        {
            // Act
            var result = _controller.Create();

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public async Task Create_ReturnsRedirectToNextPage_WhenModelStateIsValid()
        {
            // Arrange
            var nutrientViewModel = new NutrientViewModel
            {
                BaseWeight = 100,
                Calories = 200,
                TotalFat = 10,
                SaturatedFat = 5,
                Cholesterol = 50,
                Sodium = 300,
                Carbohydrate = 60
            };

            // Act
            var result = await _controller.Create(nutrientViewModel);

            // Assert
            Assert.That(result, Is.TypeOf<RedirectToActionResult>());
        }

        [Test]
        public async Task Create_ReturnsViewResult_WhenModelStateIsInvalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("BaseWeight", "Required");

            // Act
            var result = await _controller.Create(new NutrientViewModel());

            // Assert----asserts that the result is a ViewResult.

            Assert.That(result, Is.TypeOf<ViewResult>());
        }
    }
}
