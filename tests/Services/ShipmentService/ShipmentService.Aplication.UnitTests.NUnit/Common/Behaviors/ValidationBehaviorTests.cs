using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Moq;
using ShipmentService.Aplication.Common.Behaviors;
using ShipmentService.Domain.Entities;
using ShipmentService.Domain.Entities.Shipments;
using ValidationException = FluentValidation.ValidationException;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace ShipmentService.Aplication.UnitTests.NUnit.Common.Behaviors
{
    [TestFixture]
    public class ValidationBehaviorTests
    {
        private Mock<IValidator<TestRequest>> _validatorMock;
        private ValidationBehavior<TestRequest, TestResponse> _validationBehavior;

        [SetUp]
        public void SetUp()
        {
            _validatorMock = new Mock<IValidator<TestRequest>>();
            _validationBehavior = new ValidationBehavior<TestRequest, TestResponse>(new[] { _validatorMock.Object });
        }

        [Test]
        public async Task Handle_ShouldCallNext_WhenValidationSucceeds()
        {
            // Arrange
            var request = new TestRequest();
            var next = new Mock<RequestHandlerDelegate<TestResponse>>();
            _validatorMock.Setup(v => v.Validate(It.IsAny<ValidationContext<TestRequest>>()))
                          .Returns(new ValidationResult());

            // Act
            await _validationBehavior.Handle(request, next.Object, CancellationToken.None);

            // Assert
            next.Verify(n => n(), Times.Once);
        }

        [Test]
        public void Handle_ShouldThrowValidationException_WhenValidationFails()
        {
            // Arrange
            var request = new TestRequest();
            var next = new Mock<RequestHandlerDelegate<TestResponse>>();
            var validationFailures = new List<ValidationFailure>
        {
            new ValidationFailure("Property1", "Error message 1"),
            new ValidationFailure("Property2", "Error message 2")
        };
            _validatorMock.Setup(v => v.Validate(It.IsAny<ValidationContext<TestRequest>>()))
                          .Returns(new ValidationResult(validationFailures));

            // Act & Assert
            Assert.ThrowsAsync<ValidationException>(() => _validationBehavior.Handle(request, next.Object, CancellationToken.None));
        }
    }
}
