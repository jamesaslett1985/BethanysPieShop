using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BethanysPieShop.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualBasic;

namespace BethanysPieShop
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //register framework services
            services.AddControllersWithViews();

            //register our own services
            services.AddScoped<IPieRepository, MockPieRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                /*middleware components. These run sequentially, therefore the order in which they are added is important. Typically the endpoint call would be at the end
                and a component to compress the response going back to the client should be added before the use static files component, otherwise the static files would not be compressed
                */

                app.UseDeveloperExceptionPage(); //gives useful error page
                app.UseStatusCodePages(); //gives useful status codes, eg: 404
            }
   
            app.UseHttpsRedirection(); //redirects Http to Https
            app.UseStaticFiles(); //use of static files eg: images, JS, CSS etc. By default this will look for wwwroot folder

            app.UseRouting(); //UseRouting and UseEndpoints enable MVC to repond to incoming requests

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Home}/{action=Index}/{id?}");
                });
            });
        }
    }
}
