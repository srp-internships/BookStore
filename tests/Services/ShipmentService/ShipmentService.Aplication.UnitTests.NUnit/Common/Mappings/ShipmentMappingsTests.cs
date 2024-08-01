using AutoMapper;
using ShipmentService.Aplication.Common.Mappings;
using ShipmentService.Domain.Entities.Shipments;

namespace ShipmentService.Aplication.UnitTests.NUnit.Common.Mappings
{
    [TestFixture]
    public class ShipmentMappingsTests
    {
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ShipmentMappings>(); 
            });

            _mapper = config.CreateMapper();
        }

        

    }
}
    

