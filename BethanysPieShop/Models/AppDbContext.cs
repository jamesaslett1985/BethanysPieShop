using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShop.Models
{
    public class AppDbContext : DbContext //should inherit from DbContext class as default
    {
        //A DbContext must have an instance of DbContextOptions for it to execute, so we've specified it here in the contructor
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        //these are the DBsets that we want AppDbContext to manage
        public DbSet<Pie> Pies { get; set; }
        public DbSet<Category> Categories { get; set; }

        //we connect to the DB using connection strings in appsettings.json

    }
}
