using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Moq;
using ShipmentService.Aplication.Common.Behaviors;
using ShipmentService.Aplication.CQRS.Shipments.Commands.Create;
using ShipmentService.Domain.Entities;
using ShipmentService.Domain.Entities.Shipments;
using ValidationException = FluentValidation.ValidationException;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace ShipmentService.Aplication.UnitTests.NUnit.Common.Behaviors
{
    [TestFixture]
    public class ValidationBehaviorTests
    {
        private Mock<IValidator<CreateShipmentCommand>> _validatorMock;
        private ValidationBehavior<CreateShipmentCommand, Guid> _validationBehavior;

        [SetUp]
        public void Setup()
        {
            _validatorMock = new Mock<IValidator<CreateShipmentCommand>>();
            _validationBehavior = new ValidationBehavior<CreateShipmentCommand, Guid>(new[] { _validatorMock.Object });
        }
        #region Handle_ShouldProceed_WhenValidationSucceeds
        [Test]
        public async Task Handle_ShouldProceed_WhenValidationSucceeds()
        {
            //Arrage
            var command = new CreateShipmentCommand(
                Guid.NewGuid(),
                Guid.NewGuid(),
                new ShippingAddress
                {
                    City = "Khujand",
                    Country = "Tajikistan",
                    Street = "R.Jalil"
                },
                new List<ShipmentItem>
                {
                    new ShipmentItem
                    {
                        ItemId=Guid.NewGuid(),
                        BookName="Book",
                        Quantity=1
                    }
                }
             );

            _validatorMock
                .Setup(v => v.Validate(It.IsAny<ValidationContext<CreateShipmentCommand>>()))
                .Returns(new ValidationResult());

            var next = new RequestHandlerDelegate<Guid>(() => Task.FromResult(Guid.NewGuid()));
            //Act

            var result = await _validationBehavior.Handle(command, next, CancellationToken.None);

            //Assert
            Assert.IsNotNull(result);
            _validatorMock.Verify(v => v.Validate(It.IsAny<ValidationContext<CreateShipmentCommand>>()), Times.Once);
        }
        #endregion

        #region Handle_ShouldThrowValidationException_WhenValidationFails
        [Test]
        public async Task Handle_ShouldThrowValidationException_WhenValidationFails()
        {
            // Arrange
            var command = new CreateShipmentCommand(
                 Guid.NewGuid(),
                 Guid.NewGuid(),
                 new ShippingAddress
                 {
                     City = "Khujand",
                     Country = "Tajikistan",
                     Street = "R.Jalil"
                 },
                 new List<ShipmentItem>
                 {
                    new ShipmentItem
                    {
                        ItemId=Guid.NewGuid(),
                        BookName="Book",
                        Quantity=1
                    }
                 }
              );

            var validationFailures = new List<ValidationFailure>
            {
                new ValidationFailure("PropertyName", "ErrorMessage")
            };

            _validatorMock
                .Setup(v => v.Validate(It.IsAny<ValidationContext<CreateShipmentCommand>>()))
                .Returns(new ValidationResult(validationFailures));

            var next = new RequestHandlerDelegate<Guid>(() => Task.FromResult(Guid.NewGuid()));

            // Act & Assert
            var ex = Assert.ThrowsAsync<ValidationException>(async () => await _validationBehavior.Handle(command, next, CancellationToken.None));
            Assert.AreEqual(validationFailures, ex.Errors);
        }
        #endregion
    }
}
