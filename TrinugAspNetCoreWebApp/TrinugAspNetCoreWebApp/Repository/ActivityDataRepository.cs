using System;

namespace TrinugAspNetCoreWebApp.Repository
{
    public class ActivityDataRepository : IActivityDataRepository
    {
        public int CreateActivity(ActivityDataModel activity)
        {
            // TODO: Store data in CosmosDb
            throw new NotImplementedException();
        }
        public ActivityDataModel GetActivity(int id)
        {
            // TODO: Retrieve data from CosmosDb
            throw new NotImplementedException();
        }
    }
}
