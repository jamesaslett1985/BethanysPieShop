using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BethanysPieShop.Models;
using BethanysPieShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShop.Controllers
{
    public class PieController : Controller
    {
        //we need access to data in our repositories. These are fields that don't get initialized - we do that in the constructor
        private readonly IPieRepository _pieRepository;
        private readonly ICategoryRepository _categoryRepository;

        //constructor
        public PieController(IPieRepository pieRepository, ICategoryRepository categoryRepository) //IPieRepository and ICategoryRepository are injected because they are registered in Startup.
                                                                                                   //Whenever a class requires any of these types, they will be automatically injected by the built-in dependency injection system, so do not need to be newed up
        {
            _pieRepository = pieRepository; //sets our local _pieRepository to the injected pieRepository
            _categoryRepository = categoryRepository;
        }

        //action method - should be public
        public ViewResult List(string category)
        {
            IEnumerable<Pie> pies;
            string currentCategory;

            if (string.IsNullOrEmpty(category))
            {
                pies = _pieRepository.AllPies.OrderBy(p => p.PieId);
                currentCategory = "All pies";
            }
            else
            {
                pies = _pieRepository.AllPies.Where(p => p.Category.CategoryName == category)
                    .OrderBy(p => p.PieId);
                currentCategory = _categoryRepository.AllCategories.FirstOrDefault(c => c.CategoryName == category)?.CategoryName;
            }

            return View(new PiesListViewModel
            {
                Pies = pies,
                CurrentCategory = currentCategory
            });
        }
    }
}

