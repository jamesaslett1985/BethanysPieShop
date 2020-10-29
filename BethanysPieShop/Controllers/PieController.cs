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
        public ViewResult List()
        {
            //ViewBag.CurrentCategory = "Cheese cakes"; //ViewBag is a dynamic object used to transfer temporary data (not included in model) from Controller to View. View Model is a better approach
            PiesListViewModel piesListViewModel = new PiesListViewModel(); //news up instance of PiesListViewModel
            piesListViewModel.Pies = _pieRepository.AllPies; //assigns _pieRepository to piesListViewModel to return all pies from MockCategoryRepository
            piesListViewModel.CurrentCategory = "Cheese cakes"; //assigns string to the CurrentCategory property in PiesListViewModel
            return View(piesListViewModel);
        }

        public IActionResult Details (int id)
        {
            var pie = _pieRepository.GetPieById(id);
            if (pie == null)
                return NotFound();
            return View(pie);
        }
    }
}

