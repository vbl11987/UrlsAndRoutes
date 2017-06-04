using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using UrlsAndRoutes.Infrastructure;

namespace UrlsAndRoutes
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<RouteOptions>(options => {
                options.ConstraintMap.Add("weekday", typeof(WeekDayConstraint));
                //setting urls to lower case, default value is false
                options.LowercaseUrls = true;
                //appending trailing slash to url, default value is false
                options.AppendTrailingSlash = true;
            });
            services.AddMvc();   
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
                

            app.UseMvc(routes => {
                //making available the area admin section
                routes.MapRoute(
                    name : "areas",
                    template : "{area:exists}/{controller=Home}/{action=Index}"
                );


                //using the Legacy Route
                routes.Routes.Add(new LegacyRoute(app.ApplicationServices ,"/articles/Windows_3.1_Overview.html",
                    "old/.NET_1.0_Class_Library"));

                routes.MapRoute(
                    name : "default",
                    template : "{controller}/{action}/{id?}"
                );

                routes.MapRoute(
                    name : "out",
                    template : "outbound/{controller=Home}/{action=Index}"
                );

                routes.MapRoute(
                    name : "MyRouteNotInlineConstraints",
                    template : "{controller}/{action}/{id?}",
                    defaults : new { controller = "Home", action = "Index" },
                    constraints : new {
                        id = new CompositeRouteConstraint( new IRouteConstraint[]{
                            new AlphaRouteConstraint(), new MinLengthRouteConstraint(3),
                            new WeekDayConstraint()
                        })
                    }
                );
                routes.MapRoute(
                    name : "MyRouteInlineConstraints",
                    template : "{controller}/{action}/{id:alpha:min(3):weekday?}"
                );
                routes.MapRoute(
                    name : "MyRoutes",
                    template : "{controller=Home}/{action=Index}/{id:alpha:min(6)?}"
                );
                routes.MapRoute(
                    name : "MyRoute",
                    template : "{controller=Home}/{action=Index}/{id?}/{*catchall}"
                );
            });
        }
    }
}
