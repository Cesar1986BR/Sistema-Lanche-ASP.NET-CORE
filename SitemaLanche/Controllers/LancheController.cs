using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SitemaLanche.Repository;
using Microsoft.AspNetCore.Mvc;
using SitemaLanche.Models;
using SitemaLanche.ViewModels;

namespace SitemaLanche.Controllers
{
    public class LancheController : Controller
    {
        private readonly ILanchesRepository _lancheRepository;
        private readonly ICategoriaRepository _categoriaRepository;

        public LancheController(ILanchesRepository lancheRepository,
            ICategoriaRepository categoriaRepository)
        {
            _lancheRepository = lancheRepository;
            _categoriaRepository = categoriaRepository;
        }

        public IActionResult List(string categoria)
        {

            string _categoria = categoria;
            IEnumerable<Lanche> lanches; //lista de lanches
            string categoriaAtual = string.Empty;


            if (string.IsNullOrEmpty(categoria))// se nao tiver categoria lçista todas categorias
            {
                lanches = _lancheRepository.Lanches.OrderBy(l => l.lancheId);
                categoria = "Todos os Lanches";
            }
            else
            {
                //se categoria for igual Normal lista tods lanches categoria normal
                if (string.Equals("Normal",_categoria,StringComparison.OrdinalIgnoreCase))
                {
                    lanches = _lancheRepository.Lanches.Where(l =>
                    l.Categoria.CategoriaNome.Equals("Normal")).OrderBy(l => l.lancheId);
                }
                else
                {
                    //se categoria for igual Natural lista tods lanches categoria Natural
                    lanches = _lancheRepository.Lanches.Where(l =>
                    l.Categoria.CategoriaNome.Equals("Natural")).OrderBy(l => l.lancheId);
                }

                categoriaAtual = _categoria;
            }

            var lancheListViewModel = new LancheListViewModel
            {
                Lanches = lanches,
                CategoriaAtual = categoriaAtual
            };
            return View(lancheListViewModel);
        }

        public IActionResult Details(int lancheId)
        {
            var lanche = _lancheRepository.Lanches.FirstOrDefault(l => l.lancheId == lancheId);

            if (lanche == null)
            {
                return View("~/Views/Error/Error.cshtml");
            }

            return View(lanche);
        }

        public IActionResult Search(string searchString)
        {
            string _searchString = searchString;
            IEnumerable<Lanche> lanches;
            string _categoriaAtual = string.Empty;

            if (string.IsNullOrEmpty(_searchString))
            {
                lanches = _lancheRepository.Lanches.OrderBy(l => l.lancheId);
            }
            else
            {
                lanches = _lancheRepository.Lanches.Where(l => l.Nome.ToLower().Contains(_searchString.ToLower()));
            }
            return View("~/Views/Lanche/List.cshtml", new LancheListViewModel { Lanches = lanches, CategoriaAtual = "Todos os lanches" });
        }
    }
}