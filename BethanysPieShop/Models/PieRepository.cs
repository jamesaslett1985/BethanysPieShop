using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShop.Models
{
    public class PieRepository : IPieRepository
    {
        private readonly AppDbContext _appDbContext;

        //since we registered AddDbContext in in our service collection (ConfigureServices), this is managed through the dependency injection container,
        //so here we can simply create a Constructor, and through Constructor injection we get access to AppDbContext in our PieRepository
        //appDbContext is our intermediary between the code and the database - PieRepository will use AppDbContext for persisting and reading data from the database
        public PieRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext; //our local _appDbContext = the injected appDbContext
        }

        public IEnumerable<Pie> AllPies
        {
            get
            {
                return _appDbContext.Pies.Include(c => c.Category); //gets all Pies from AppDbContext > DbSet<Pie> Pies. This will send off a SQL query that will read out all the pies
                //Include(c => c.Category) also returns the navigation to the Category class in our Pie class. This is done on a databse level rather than in code; it will ensure that the
                //database performs the optimal query. 
            }
        }

        public IEnumerable<Pie> PiesOfTheWeek
        {
            get
            {
                return _appDbContext.Pies.Include(c => c.Category).Where(p => p.IsPieOfTheWeek); //returns pies where pie of the week is true
            }
        }

        public Pie GetPieById(int pieId)
        {
            return _appDbContext.Pies.FirstOrDefault(p => p.PieId == pieId); //returns first pie that matches the given Id
        }
    }
}
