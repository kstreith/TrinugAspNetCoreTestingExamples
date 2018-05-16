using System;
using System.Collections.Generic;
using TrinugAspNetCoreWebApp.Repository;

namespace TrinugAspNetCoreWebApp.Tests.Mocks
{
    public class MockActivityDataRepository : IActivityDataRepository
    {
        private readonly List<ActivityDataModel> activities = new List<ActivityDataModel>();
        public int CreateActivity(ActivityDataModel activity)
        {
            activities.Add(activity);
            return activities.Count;
        }

        public ActivityDataModel GetActivity(int id)
        {
            if (id >= 0 && id < activities.Count)
            {
                return activities[id];
            }
            return null;
        }
    }
}
