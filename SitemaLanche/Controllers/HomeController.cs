using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SitemaLanche.Models;
using SitemaLanche.Repository;

namespace SitemaLanche.Controllers
{
    public class HomeController : Controller
    {
        private readonly IlanchesRepository _lancheRepository;

        public HomeController(IlanchesRepository lancheRepository)
        {

            _lancheRepository = lancheRepository;
        }

        public IActionResult Index()
        {

            var homeViewModel = new HomeViewModel
            {
                LanchesPreferidos = _lancheRepository.LanchesPreferidos
            };

            return View(homeViewModel);
        }

   }

}
