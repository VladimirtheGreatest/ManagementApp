using AutoMapper;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranslationManagement.Data.Entities;
using TranslationManagement.Repository.Contracts;
using TranslationManagement.Services.DTO;
using TranslationManagement.Services;
using Xunit;
using TranslationManagement.Data;

namespace TranslationManagement.Tests
{
    public class TranslatorManagementServiceTests
    {
        private readonly Mock<IRepository> _repositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly TranslatorManagementService _service;

        public TranslatorManagementServiceTests()
        {
            _repositoryMock = new Mock<IRepository>();
            _mapperMock = new Mock<IMapper>();
            _service = new TranslatorManagementService(
                _repositoryMock.Object,
                _mapperMock.Object
            );
        }


        [Fact]
        public async Task AddTranslator_WithInvalidTranslator_ShouldThrowException()
        {
            // Arrange
            var translator = new CreateTranslatorRequestDto { Name = "Problematic", Status = 1 };

            _repositoryMock.Setup(x => x.TranslatorsStatuses.GetTranslationStatusByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((TranslatorStatus)null);

            // Act
            Func<Task> action = async () => await _service.AddTranslator(translator);

            // Assert
            await action.Should().ThrowAsync<ArgumentException>().WithMessage($"unknown status id : {translator.Status}");
        }
    }

}
