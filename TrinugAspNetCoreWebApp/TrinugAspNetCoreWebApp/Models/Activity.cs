using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrinugAspNetCoreWebApp.Models
{
    public class Activity
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public int? AttendeeLimit { get; set; }
    }
}
