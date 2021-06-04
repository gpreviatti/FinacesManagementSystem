using System;

namespace Tests
{
    public abstract class BaseTest
    {
        protected readonly string _environment;

        protected readonly Guid _userAdminId = Guid.Parse("430E0144-289F-4A95-8F14-BACFABB3FE8A");
        protected readonly Guid _testUser01Id = Guid.Parse("CB43D078-87F1-4864-853A-E626922B8109");

        public BaseTest()
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Testing");
            _environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        }
    }
}
