using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using BethanysPieShop.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualBasic;

namespace BethanysPieShop
{
    public class Startup
    {
        public IConfiguration Configuration { get; } //by default appsettings.json gets read out and will arrive in an instance of IConfiguration which is passed in via constructor injection.
        //in this IConfiguration we have access to the properties in appsettings.json. 

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))); //gets defaultconnection parameters from appsettings.json

            //MOCK REPOSITORIES
            //services.AddScoped<IPieRepository, MockPieRepository>(); //registers our service with its interface 
            //services.AddScoped<ICategoryRepository, MockCategoryRepository>();

            //REAL REPOSITORIES
            services.AddScoped<IPieRepository, PieRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            services.AddControllersWithViews();

            //services.AddSingleton- creates a single instance for the entire application and reuse it
            //services.AddTransient - creates new instance every time it is called
            //services.AddScoped - instance created per request and used until it goes out of scope. Like a singleton per request
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

            app.UseRouting(); //Enables convention-based routing

            app.UseEndpoints(endpoints => //where we register our endpoints
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}