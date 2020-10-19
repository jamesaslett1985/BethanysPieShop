using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BethanysPieShop
{
    public class Program
    {

        //ASP .NET Core applications run as a console application, so Main is called first
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args) //sets defaults up front, eg: environment will be set to Development                             
                .ConfigureWebHostDefaults(webBuilder => //configures Kestrel/IIS etc. 
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
