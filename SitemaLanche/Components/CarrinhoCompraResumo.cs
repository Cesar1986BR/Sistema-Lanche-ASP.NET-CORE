using Microsoft.AspNetCore.Mvc;
using SitemaLanche.Models;
using SitemaLanche.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SitemaLanche.Components
{
    public class CarrinhoCompraResumo : ViewComponent
    {
        private readonly CarrinhoCompra _carrinhoCompra;

        public CarrinhoCompraResumo(CarrinhoCompra carrinhoCompra)
        {
            _carrinhoCompra = carrinhoCompra;
        }
        public IViewComponentResult Invoke()
        {
             var items = _carrinhoCompra.GetCarrinhoCompraItens();
            //para testar 
            //var items = new List<CarrinhoCompraItem>() { new CarrinhoCompraItem(), new CarrinhoCompraItem() };
            _carrinhoCompra.CarrinhoCompraItens = items;

            var carrinhoCompraVM = new CarrinhoCompraViewModel
            {
               CarrinhoCompra = _carrinhoCompra,
               CarrinhoCompraTotal = _carrinhoCompra.GetCarrinhoCompraTotal() //retorna total de itens no carrinho
            };
            return View(carrinhoCompraVM);
        }
    }
}
