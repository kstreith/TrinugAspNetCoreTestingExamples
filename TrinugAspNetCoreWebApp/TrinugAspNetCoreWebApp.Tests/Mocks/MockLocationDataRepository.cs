using System;
using TrinugAspNetCoreWebApp.Repository;

namespace TrinugAspNetCoreWebApp.Tests.Mocks
{
    public class MockLocationDataRepository : ILocationDataRepository
    {
        public LocationDataModel LookupLocationByAlias(string alias)
        {
            if (alias == "AisOffice")
            {
                return new LocationDataModel
                {
                    Id = Guid.NewGuid()
                };
            }
            return null;
        }
    }
}
