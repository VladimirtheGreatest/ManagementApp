using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using TranslationManagement.Api.Controlers;
using TranslationManagement.Data.Configuration;
using TranslationManagement.Services.Contracts;
using TranslationManagement.Services.DTO;
using Xunit;

namespace TranslationManagement.Tests
{
    public class TranslatorManagementControllerTests
    {
        private readonly Mock<ITranslatorManagementService> _translatorManagementServiceMock;
        private readonly TranslatorManagementController _controller;

        public TranslatorManagementControllerTests()
        {
            _translatorManagementServiceMock = new Mock<ITranslatorManagementService>();
            _controller = new TranslatorManagementController(
                Mock.Of<ILogger<TranslatorManagementController>>(),
                _translatorManagementServiceMock.Object
            );
        }

        [Fact]
        public async Task GetTranslatorsByName_WithInvalidName_Should_ReturnBadRequest()
        {
            // Arrange
            var name = "";

            // Act
            var result = await _controller.GetTranslatorsByName(name);

            // Assert
            result.Should().BeOfType<ActionResult<List<TranslatorDto>>>();
            var badRequestResult = result.Result.Should().BeOfType<BadRequestObjectResult>().Subject;
            badRequestResult.Value.Should().Be(Constants.translatorNameErrorMessage);
        }

        [Fact]
        public async Task GetTranslatorsByName_WithValidName_ReturnsListOfTranslators()
        {
            // Arrange
            var name = "Batman";
            var expectedTranslators = new List<TranslatorDto>
        {
            new TranslatorDto { Id = 1, Name = name },
            new TranslatorDto { Id = 2, Name = name }
        };
            _translatorManagementServiceMock.Setup(service => service.GetTranslatorsByName(name))
                .ReturnsAsync(expectedTranslators);

            // Act
            var result = await _controller.GetTranslatorsByName(name);

            // Assert
            result.Should().BeOfType<ActionResult<List<TranslatorDto>>>();
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var actualTranslators = okResult.Value.Should().BeAssignableTo<List<TranslatorDto>>().Subject;
            actualTranslators.Should().BeEquivalentTo(expectedTranslators);
        }

        [Fact]
        public async Task AddTranslator_WithValidTranslator_Should_Return_Translator()
        {
            // Arrange
            var translator = new CreateTranslatorRequestDto { Name = "John", Status = 1 };
            _translatorManagementServiceMock.Setup(service => service.AddTranslator(translator))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.AddTranslator(translator);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().Be(translator);
        }

        [Fact]
        public async Task AddTranslator_WithInvalidTranslator_Should_ReturnsBadRequest()
        {
            // Arrange
            var translator = new CreateTranslatorRequestDto { Name = "Paladin", Status = 1 };
            _translatorManagementServiceMock.Setup(service => service.AddTranslator(translator))
                .ReturnsAsync(false);

            // Act
            var result = await _controller.AddTranslator(translator);

            // Assert
            var badRequestResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
            badRequestResult.Value.Should().Be(Constants.addTranslatorErrorMessage);
        }

        [Fact]
        public async Task GetTranslators_Should_ReturnListOfTranslators()
        {
            // Arrange
            var expectedTranslators = new List<TranslatorDto>
        {
            new TranslatorDto { Id = 1, Name = "Vladimir" },
            new TranslatorDto { Id = 2, Name = "Anakin" }
        };
            _translatorManagementServiceMock.Setup(service => service.GetTranslators())
                .ReturnsAsync(expectedTranslators);

            // Act
            var result = await _controller.GetTranslators();

            // Assert
            result.Should().BeOfType<ActionResult<List<TranslatorDto>>>();
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var actualTranslators = okResult.Value.Should().BeAssignableTo<List<TranslatorDto>>().Subject;
            actualTranslators.Should().BeEquivalentTo(expectedTranslators);
        }

        [Fact]
        public async Task UpdateTranslatorStatus_WithValidParameters_Should_ReturnOk()
        {
            // Arrange
            var translatorId = 1;
            var newStatus = 2;
            _translatorManagementServiceMock.Setup(service => service.UpdateTranslatorStatus(translatorId, newStatus))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateTranslatorStatus(translatorId, newStatus);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().Be(Constants.translatorUpdatedStatusMessage);
        }
    }

}
