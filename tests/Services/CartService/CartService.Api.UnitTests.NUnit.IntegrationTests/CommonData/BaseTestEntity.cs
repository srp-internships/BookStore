using Microsoft.AspNetCore.TestHost;

namespace CartService.Api.UnitTests.NUnit.IntegrationTests.CommonData
{
    public abstract class BaseTestEntity
    {
        protected TestServer Server;

        public BaseTestEntity()
        {
            Server = new ServerApiFactory().Server;
        }
    }
}
