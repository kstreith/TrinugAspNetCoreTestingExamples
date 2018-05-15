using System.Collections.Generic;

namespace TrinugAspNetCoreWebApp
{
    public class ValidationException : ClientException
    {
        public List<string> Errors { get; set; }
    }
}
