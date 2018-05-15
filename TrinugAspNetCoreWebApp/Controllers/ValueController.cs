﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TrinugAspNetCoreWebApp.Controllers
{
    [Route("api/[controller]")]
    public class ValueController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id > 100) {
                return new NotFoundResult();
            }
            return new OkObjectResult($"value {id}");
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]string value)
        {
            if (value == "bad")
            {
                return new BadRequestObjectResult(new List<string> { "Input cannot have a value of 'bad'" });
                //throw new ValidationException {
                //    Errors = new List<string> { "Input cannot have a value of 'bad'" }
                //};
            }
            return new OkResult();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [Authorize("DeleteValue")]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
