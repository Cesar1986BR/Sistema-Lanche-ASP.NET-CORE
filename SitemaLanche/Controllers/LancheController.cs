using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SitemaLanche.Repository;
using Microsoft.AspNetCore.Mvc;
using SitemaLanche.ViewModels;

namespace SitemaLanche.Controllers
{
    public class LancheController : Controller
    {
        private readonly IlanchesRepository _lancheRepository;
        private readonly ICategoriaRepository _categoriaRepository;

        public LancheController(IlanchesRepository lancheRepository,
            ICategoriaRepository categoriaRepository)
        {
            _lancheRepository = lancheRepository;
            _categoriaRepository = categoriaRepository;
        }

        public IActionResult List()
        {
            //var lanches = _lancheRepository.Lanches;
            //  return View(lanches);

            var lancheListViewModel = new LancheListViewModel();
            lancheListViewModel.Lanches = _lancheRepository.Lanches;
            lancheListViewModel.CategoriaAtual = "Categoria Atual";
            return View(lancheListViewModel);
        }
    }
}