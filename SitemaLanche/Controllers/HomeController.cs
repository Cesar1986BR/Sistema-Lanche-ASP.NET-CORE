using Microsoft.AspNetCore.Mvc;
using SitemaLanche.Repository;
using SitemaLanche.ViewModels;


namespace SitemaLanche.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILanchesRepository _lancheRepository;

        public HomeController(ILanchesRepository lancheRepository)
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
