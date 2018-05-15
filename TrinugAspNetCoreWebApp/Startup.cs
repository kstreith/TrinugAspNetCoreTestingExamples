using System;
using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using TrinugAspNetCoreWebApp.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace TrinugAspNetCoreWebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            ConfigureAppAuthentication(services);
            services.AddAuthorization(opt => opt.AddPolicy("DeleteValue", policy => policy.RequireRole("Delete")));
        }

        protected virtual void ConfigureAppAuthentication(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opt => {
                opt.MetadataAddress = "https://myauthserver.itsnull.com/";
                opt.RequireHttpsMetadata = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            //app.UseExceptionHandler(appBuilder => appBuilder.Use(async (context, next) =>
            //{
            //    Exception exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
            //    if (exception == null)
            //    {
            //        await next.Invoke();
            //    }
            //    var validationException = exception as ValidationException;
            //    if (validationException != null)
            //    {
            //        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            //        var responseContent = JsonConvert.SerializeObject(validationException.Errors);
            //        context.Response.ContentType = "application/json";
            //        await context.Response.WriteAsync(responseContent);
            //    }
            //    else
            //    {
            //        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            //    }
            //}));



            //app.UseExceptionHandler(appBuilder =>
            //    appBuilder.UseMiddleware<ErrorResponseMiddleware>()
            //);

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
