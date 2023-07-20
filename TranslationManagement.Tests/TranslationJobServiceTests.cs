using Microsoft.AspNetCore.Http;
using Moq;
using System.IO;
using System;
using TranslationManagement.Services;
using TranslationManagement.Services.Contracts;
using TranslationManagement.Services.DTO;
using Xunit;
using FluentAssertions;
using TranslationManagement.Data.Configuration;
using System.Xml.Linq;

namespace TranslationManagement.Tests
{
    public class TranslationJobServiceTests
    {
        [Theory]
        [InlineData(10, 0.1)] 
        [InlineData(0, 0)] 
        [InlineData(100, 1)] 
        [InlineData(50, 0.5)] 
        public void CalculatePrice_Should_ReturnCorrectPrice(int contentLength, double expectedPrice)
        {
            // Arrange
            var pricingService = new PricingService();

            // Act
            double actualPrice = pricingService.CalculatePrice(contentLength);

            // Assert
            Assert.Equal(expectedPrice, actualPrice);
        }

        [Theory]
        [InlineData("test.xml", @"<?xml version=""1.0"" encoding=""UTF-8""?>
                          <Root>
                              <Customer>Trash Customer</Customer>
                              <Content>Test content</Content>
                          </Root>", "Trash Customer" )]
        public void ProcessFile_ShouldSetDtoPropertiesCorrectly_ForXml(string fileName, string content,string expectedCustomer)
        {
            // Arrange
            var formFileMock = new Mock<IFormFile>();
            formFileMock.Setup(f => f.FileName).Returns(fileName);
            var fileService = new FileService();

            var dto = new CreateTranslatorJobRequestDto();

            using var stream = new MemoryStream();
            using (var writer = new StreamWriter(stream))
            {
                writer.Write(content);
                writer.Flush();
                stream.Position = 0;
                formFileMock.Setup(f => f.OpenReadStream()).Returns(stream);

                // Act
                var result = fileService.ProcessFile(formFileMock.Object, dto, expectedCustomer);

                // Assert
                result.Should().NotBeNull();
                result.CustomerName.Should().Be(expectedCustomer)
                    ;
                var xdoc = XDocument.Parse(content);
                var originalContent = xdoc.Root.Element("Content").Value;
                result.OriginalContent.Should().Be(originalContent);
            }
        }

        [Fact]
        public void ProcessFile_WithUnsupportedFile_ShouldThrowNotSupportedException()
        {
            // Arrange
            var formFileMock = new Mock<IFormFile>();
            formFileMock.Setup(f => f.FileName).Returns("test.jpg");
            var fileService = new FileService();

            var dto = new CreateTranslatorJobRequestDto();

            var customer = "Obiwan";
            using var stream = new MemoryStream();
            formFileMock.Setup(f => f.OpenReadStream()).Returns(stream);

            // Act
            Action act = () => fileService.ProcessFile(formFileMock.Object, dto, customer);

            // Assert
            act.Should().Throw<NotSupportedException>().WithMessage(Constants.unsupportedFileFormat);
        }
    }
}
