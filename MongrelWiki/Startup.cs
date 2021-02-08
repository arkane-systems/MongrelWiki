#region header

// MongrelWiki - Startup.cs
// 
// Created by: Alistair J R Young (avatar) at 2021/02/07 4:19 PM.

#endregion

#region using

using ArkaneSystems.MongrelWiki.Configurations;
using ArkaneSystems.MongrelWiki.Services;

using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

#endregion

namespace ArkaneSystems.MongrelWiki
{
    public class Startup
    {
        public Startup (IConfiguration configuration) => this.Configuration = configuration;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services)
        {
            // Configure MongoDB access.
            services.Configure<DatabaseConfiguration> (config: this.Configuration.GetSection (key: "Database"));
            services.AddScoped<WikiService> ();

            // Configure Windows Authentication.
            services.AddAuthentication (defaultScheme: NegotiateDefaults.AuthenticationScheme).AddNegotiate ();

            // Use MVC.
            services.AddControllersWithViews ();

            // Set up fallback authorization policy such that an authenticated user is required for every route
            // not explicitly configured otherwise.
            services.AddAuthorization (configure: options =>
                                                  {
                                                      options.FallbackPolicy = new AuthorizationPolicyBuilder ()
                                                                              .RequireAuthenticatedUser ()
                                                                              .Build ();
                                                  });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment ())
            {
                app.UseDeveloperExceptionPage ();
            }
            else
            {
                app.UseExceptionHandler (errorHandlingPath: "/Home/Error");

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts ();
            }

            app.UseHttpsRedirection ();
            app.UseStaticFiles ();

            app.UseRouting ();

            app.UseAuthentication ();
            app.UseAuthorization ();

            app.UseEndpoints (configure: endpoints =>
                                         {
                                             endpoints.MapControllerRoute (
                                                                           name: "default",
                                                                           pattern: "{controller=Home}/{action=Index}/{id?}");
                                         });
        }
    }
}
