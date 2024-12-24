using NUnit.Framework;
using Moq;
using Microsoft.AspNetCore.Mvc;
using LoginApp.Controllers;
using LoginApp.Services;
using LoginApp.ViewModels;
using LoginApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LoginApp.Repositories;
namespace LoginApp.Tests.Controllers
{
    [TestFixture]
    public class IngredientsControllerTests
    {
        private Mock<IIngredientRepository> _mockIngredientRepository;
        private IngredientService _ingredientService;
        private IngredientsController _controller;

        [SetUp]
        public void Setup()
        {
            _mockIngredientRepository = new Mock<IIngredientRepository>();
            _ingredientService = new IngredientService(_mockIngredientRepository.Object);
            _controller = new IngredientsController(_ingredientService);
        }

        [Test]
        public void Ingredients_ReturnsViewResult()
        {
            // Arrange
            var ingredients = new List<Ingredient>
            {
                new Ingredient { Name = "Ingredient1", Source = "Source1", Classification = "Class1" },
                new Ingredient { Name = "Ingredient2", Source = "Source2", Classification = "Class2" }
            };
            _mockIngredientRepository.Setup(repo => repo.GetAllIngredients()).Returns(ingredients);

            // Act
            var result = _controller.Ingredients();

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
            var viewResult = result as ViewResult;
            Assert.That(viewResult.Model, Is.TypeOf<List<IngredientViewModel>>());
        }

        [Test]
        public void AddIngredient_RedirectsToIngredients_WhenModelStateIsValid()
        {
            // Arrange
            var model = new IngredientViewModel
            {
                Name = "Ingredient1",
                Source = "Source1",
                Classification = "Class1"
            };
            _mockIngredientRepository.Setup(repo => repo.AddIngredient(It.IsAny<Ingredient>())).Verifiable();

            // Act
            var result = _controller.AddIngredient(model);

            // Assert
            Assert.That(result, Is.TypeOf<RedirectToActionResult>());
            var redirectResult = result as RedirectToActionResult;
            Assert.That(redirectResult.ActionName, Is.EqualTo("Ingredients"));
        }

        [Test]
        public void AddIngredient_ReturnsViewResult_WhenModelStateIsInvalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("Name", "Required");

            // Act
            var result = _controller.AddIngredient(new IngredientViewModel());

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
            var viewResult = result as ViewResult;
            Assert.That(viewResult.Model, Is.TypeOf<List<IngredientViewModel>>());
        }

        [Test]
        public void UpdateIngredient_ReturnsViewResult_WithIngredient()
        {
            // Arrange
            var ingredient = new Ingredient { Id = 1, Name = "Ingredient1", Source = "Source1", Classification = "Class1" };
            _mockIngredientRepository.Setup(repo => repo.GetIngredientById(1)).Returns(ingredient);

            // Act
            var result = _controller.UpdateIngredient(1);

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
            var viewResult = result as ViewResult;
            Assert.That(viewResult.Model, Is.TypeOf<IngredientViewModel>());
        }

        [Test]
        public void UpdateIngredient_ReturnsNotFound_WhenIngredientIsNull()
        {
            // Arrange
            _mockIngredientRepository.Setup(repo => repo.GetIngredientById(1)).Returns((Ingredient)null);

            // Act
            var result = _controller.UpdateIngredient(1);

            // Assert
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void SearchIngredients_ReturnsViewResult_WithResults()
        {
            // Arrange
            var ingredients = new List<Ingredient>
            {
                new Ingredient { Name = "Ingredient1", Source = "Source1", Classification = "Class1" },
                new Ingredient { Name = "Ingredient2", Source = "Source2", Classification = "Class2" }
            };
            _mockIngredientRepository.Setup(repo => repo.SearchIngredients(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(ingredients);

            // Act
            var result = _controller.SearchIngredients("Ingredient1", "Source1", "Class1");

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
            var viewResult = result as ViewResult;
            Assert.That(viewResult.Model, Is.TypeOf<List<IngredientViewModel>>());
        }

        [Test]
        public void DeleteIngredients_ReturnsOk_WhenDeletionIsSuccessful()
        {
            // Arrange
            var ids = new int[] { 1, 2 };
            _mockIngredientRepository.Setup(repo => repo.DeleteIngredients(ids)).Verifiable();

            // Act
            var result = _controller.DeleteIngredients(ids);

            // Assert
            Assert.That(result, Is.TypeOf<OkResult>());
        }

        [Test]
        public void GetIngredients_ReturnsJsonResult_WithIngredients()
        {
            // Arrange
            var ingredients = new List<Ingredient>
            {
                new Ingredient { Name = "Ingredient1", Source = "Source1", Classification = "Class1" },
                new Ingredient { Name = "Ingredient2", Source = "Source2", Classification = "Class2" }
            };
            _mockIngredientRepository.Setup(repo => repo.GetAllIngredients()).Returns(ingredients);

            // Act
            var result = _controller.GetIngredients();

            // Assert
            Assert.That(result, Is.TypeOf<JsonResult>());
            var jsonResult = result as JsonResult;
            Assert.That(jsonResult.Value, Is.TypeOf<List<Ingredient>>());
        }

        [Test]
        public void MapIngredients_ReturnsViewResult_WithMappedIngredients()
        {
            // Arrange
            var ingredients = new List<Ingredient>
            {
                new Ingredient { Name = "ingredient1", Source = "Source1", Classification = "Class1" },
                new Ingredient { Name = "ingredient2", Source = "Source2", Classification = "Class2" }
            };
            _mockIngredientRepository.Setup(repo => repo.Map(It.IsAny<Func<Ingredient, Ingredient>>())).Returns((Func<Ingredient, Ingredient> mapFunction) =>
            {
                foreach (var ingredient in ingredients)
                {
                    mapFunction(ingredient);
                }
                return ingredients;
            });

            // Act
            var result = _controller.MapIngredients();

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
            var viewResult = result as ViewResult;
            Assert.That(viewResult.Model, Is.TypeOf<List<Ingredient>>());
            var model = viewResult.Model as List<Ingredient>;
            Assert.That(model.Count, Is.EqualTo(2));
            Assert.That(model[0].Name, Is.EqualTo("INGREDIENT1"));
            Assert.That(model[1].Name, Is.EqualTo("INGREDIENT2"));
        }
    }
}
