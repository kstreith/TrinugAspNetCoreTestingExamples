using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrinugAspNetCoreWebApp.Models;
using TrinugAspNetCoreWebApp.Repository;

namespace TrinugAspNetCoreWebApp.Controllers
{
    [Route("api/[controller]")]
    public class ActivityController : Controller
    {
        private readonly IActivityDataRepository _activityDataRepository;
        private readonly ILocationDataRepository _locationDataRepository;

        public ActivityController(IActivityDataRepository activityRepository,
            ILocationDataRepository locationRepository)
        {
            _activityDataRepository = activityRepository;
            _locationDataRepository = locationRepository;
        }

        // POST api/activity
        [HttpPost]
        public IActionResult Post([FromBody]Activity activity)
        {
            var location = _locationDataRepository.LookupLocationByAlias(activity.Location);
            if (location == null)
            {
                return new BadRequestObjectResult(new List<string> { $"Location '{activity.Location}' does not exist" });
            }
            var activityModel = new ActivityDataModel
            {
                Name = activity.Name,
                AttendeeLimit = activity.AttendeeLimit,
                LocationId = location.Id
            };
            var newActivityId = _activityDataRepository.CreateActivity(activityModel);
            return new OkResult();
        }
    }
}
