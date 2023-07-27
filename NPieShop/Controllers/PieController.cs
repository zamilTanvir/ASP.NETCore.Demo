using Microsoft.AspNetCore.Mvc;
using NPieShop.Models;
using NPieShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NPieShop.Controllers
{
    public class PieController : Controller
    {
        private readonly IPieRepository _pieRepository;
        private readonly ICategoryRepository _categoryRepository;

        public PieController(IPieRepository pieRepository, ICategoryRepository categoryRepository)
        {
            _pieRepository = pieRepository;
            _categoryRepository = categoryRepository;
        }
         
        /*public ViewResult List()
        {
            PiesListViewModel PiesListViewModel = new PiesListViewModel();
            PiesListViewModel.Pies = _pieRepository.AllPies;
            PiesListViewModel.CurrentCategory = "Cheese Cakes";
            return View(PiesListViewModel);
        }*/
        public ViewResult List(string category)
        {
            IEnumerable<Pie> pies;
            string currentCategory;

            if (string.IsNullOrEmpty(category)){
                pies = _pieRepository.AllPies.OrderBy(p => p.PieId);
                currentCategory = "All Pies";
            }
            else
            {
                pies = _pieRepository.AllPies.Where(p => p.Category.CategoryName == category);
                currentCategory = _categoryRepository.AllCategories.FirstOrDefault(c => c.CategoryName == category).ToString();
            }

            return View(new PiesListViewModel
            {
                Pies = pies,
                CurrentCategory = currentCategory
            }) ; 
   
        }

        public IActionResult Details(int id)
        {
            var pie = _pieRepository.GetPieById(id);
            if (pie == null)
                return NotFound();
            return View(pie);
        }
    }
}
