using System;

namespace TrinugAspNetCoreWebApp.Repository
{
    public class LocationDataModel
    {
        public Guid Id { get; set; }
        public string Alias { get; set; }
        public string Description { get; set; }
        public string Street1 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
    }
}
