using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SitemaLanche.Models;
using SitemaLanche.Repository;
using SitemaLanche.ViewModels;

namespace SitemaLanche.Controllers
{
    public class CarringoCompraController : Controller
    {
        private IlanchesRepository _lancheRepository;
        private readonly CarrinhoCompra _carrinhoCompra;

        public CarringoCompraController(IlanchesRepository lancheRepository, CarrinhoCompra carrinhoCompra)
        {
            _lancheRepository = lancheRepository;
            _carrinhoCompra = carrinhoCompra;
        }
        public IActionResult Index()
        {
            var itens = _carrinhoCompra.GetCarrinhoCompraItens();
            _carrinhoCompra.CarrinhoCompraItems = itens;

            var carrinhoCompraViewModel = new CarrinhoCompraViewModel
            {
                CarinhoCompra = _carrinhoCompra,
                CarrinhoCompraTotal = _carrinhoCompra.GetCarrinhoCompraTotal()
            };

            return View(carrinhoCompraViewModel);
        }
        public RedirectToActionResult AdicionarItenNoCarrinho(int lancheId)
        {
            var lancheSelecionado = _lancheRepository.Lanches.FirstOrDefault(p => p.lancheId == lancheId);
            if (lancheSelecionado != null)
            {
                _carrinhoCompra.AdiconarAoCarrinho(lancheSelecionado, 1);
               
            }
            return RedirectToAction("Index");
        }
        public RedirectToActionResult RemoverItenNoCarrinho(int lancheId)
        {
            var lancheSelecionado = _lancheRepository.Lanches.FirstOrDefault(p => p.lancheId == lancheId);
            if (lancheSelecionado != null)
            {
                _carrinhoCompra.RemoverDoCarrinho(lancheSelecionado);

            }
            return RedirectToAction("Index");
        }
    }
}