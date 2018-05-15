using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace TrinugAspNetCoreWebApp.Tests
{
    public class StartupUnderTest : Startup
    {
        public StartupUnderTest(IConfiguration config) : base(config)
        {
        }

        protected override void ConfigureAppAuthentication(IServiceCollection services)
        {
            services.AddAuthentication(MockAuthenticationDefaults.AuthenticationScheme)
            .AddMockAuthentication();
        }
    }
}
