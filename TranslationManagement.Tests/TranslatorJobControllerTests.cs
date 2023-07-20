using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Tasks;
using TranslationManagement.Api.Controllers;
using TranslationManagement.Services.Contracts;
using TranslationManagement.Services.DTO;
using Xunit;

namespace TranslationManagement.Tests
{
    public class TranslatorJobControllerTests
    {
        private readonly Mock<ITranslationJobService> _translationJobServiceMock;
        private readonly Mock<IFileService> _fileServiceMock;
        private readonly Mock<IPricingService> _pricingMock;
        private readonly TranslationJobController _controller;

        public TranslatorJobControllerTests()
        {
            _translationJobServiceMock = new Mock<ITranslationJobService>();
            _fileServiceMock = new Mock<IFileService>();
            _pricingMock = new Mock<IPricingService>();
            _controller = new TranslationJobController(
            Mock.Of<ILogger<TranslationJobController>>(),
           _translationJobServiceMock.Object
        );
        }
        [Fact]
        public async Task CreateJobWithFile_Should_CreateJob_AndReturnOkResult()
        {
            // Arrange
            var formFileMock = new Mock<IFormFile>();
            var customer = "Vladimir";
            var jobRequestDto = new CreateTranslatorJobRequestDto();

            _fileServiceMock.Setup(f => f.ProcessFile(It.IsAny<IFormFile>(), It.IsAny<CreateTranslatorJobRequestDto>(), It.IsAny<string>()))
                            .Returns(jobRequestDto);

            _pricingMock.Setup(p => p.CalculatePrice(It.IsAny<int>()))
                               .Returns(10.0); 

            // Act
            var result = await _controller.CreateJobWithFile(formFileMock.Object, customer);

            // Assert
            _translationJobServiceMock.Verify(t => t.AddJob(It.IsAny<CreateTranslatorJobRequestDto>()), Times.Once);
            result.Should().BeOfType<OkResult>();
        }

    }
}
