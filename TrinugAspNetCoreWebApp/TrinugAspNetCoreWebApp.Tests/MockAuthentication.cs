using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace TrinugAspNetCoreWebApp.Tests
{
    public class MockAuthenticationHandler : AuthenticationHandler<MockAuthenticationOptions>
    {
        public MockAuthenticationHandler(IOptionsMonitor<MockAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock)
        { }
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var roles = Context.RequestServices.GetRequiredService<MockRoles>();
            var authHeaders = Context.Request.Headers["Authorization"];
            if (authHeaders.Any() && roles.Any())
            {
                var claims = roles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();
                var claimsIdentity = new ClaimsIdentity(claims, Scheme.Name);
                var authProperties = new AuthenticationProperties();

                return Task.FromResult(AuthenticateResult.Success(
                    new AuthenticationTicket(
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties,
                        Scheme.Name)));
            }

            return Task.FromResult(AuthenticateResult.Fail("MockAuthenticationOptions setting 'IsAuthenticated' is set to 'false'."));
        }

    }

    public class MockRoles : List<string> { }
    public class MockAuthenticationOptions : AuthenticationSchemeOptions
    {
    }

    public class MockAuthenticationPostConfigureOptions : IPostConfigureOptions<MockAuthenticationOptions>
    {
        public void PostConfigure(string name, MockAuthenticationOptions options)
        {
        }
    }

    public static class MockAuthenticationDefaults
    {
        /// <summary>
        /// Default value for AuthenticationScheme property in the MockAuthenticationOptions
        /// </summary>
        public const string AuthenticationScheme = "MockAuthenticationScheme";
    }

    public static class MockAuthenticationExtensions
    {
        public static AuthenticationBuilder AddMockAuthentication(this AuthenticationBuilder builder)
            => builder.AddMockAuthentication(MockAuthenticationDefaults.AuthenticationScheme, _ => { });

        public static AuthenticationBuilder AddMockAuthentication(this AuthenticationBuilder builder, Action<MockAuthenticationOptions> configureOptions)
            => builder.AddMockAuthentication(MockAuthenticationDefaults.AuthenticationScheme, configureOptions);

        public static AuthenticationBuilder AddMockAuthentication(this AuthenticationBuilder builder, string authenticationScheme, Action<MockAuthenticationOptions> configureOptions)
            => builder.AddMockAuthentication(authenticationScheme, displayName: null, configureOptions: configureOptions);

        public static AuthenticationBuilder AddMockAuthentication(this AuthenticationBuilder builder, string authenticationScheme, string displayName, Action<MockAuthenticationOptions> configureOptions)
        {
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IPostConfigureOptions<MockAuthenticationOptions>, MockAuthenticationPostConfigureOptions>());
            return builder.AddScheme<MockAuthenticationOptions, MockAuthenticationHandler>(authenticationScheme, displayName, configureOptions);
        }
    }
}
