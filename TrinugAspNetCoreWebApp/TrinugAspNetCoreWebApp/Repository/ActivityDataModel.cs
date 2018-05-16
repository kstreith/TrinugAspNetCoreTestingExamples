using System;

namespace TrinugAspNetCoreWebApp.Repository
{
    public class ActivityDataModel
    {
        public string Name { get; set; }
        public Guid LocationId { get; set; }
        public int? AttendeeLimit { get; set; }
    }
}
