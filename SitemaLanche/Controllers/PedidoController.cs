using Microsoft.AspNetCore.Mvc;
using SitemaLanche.Models;
using SitemaLanche.Repository;

namespace SitemaLanche.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly CarrinhoCompra _carrinhoCompra;

        public PedidoController(IPedidoRepository pedidoRepository, CarrinhoCompra carrinhoCompra)
        {
            _pedidoRepository = pedidoRepository;
            _carrinhoCompra = carrinhoCompra;
        }


        public IActionResult Checkout()
        {
            return View();
        }
         
        [HttpPost]
        public IActionResult Checkout(Pedido pedido)
        {

            var itens = _carrinhoCompra.GetCarrinhoCompraItens();
            _carrinhoCompra.CarrinhoCompraItens = itens;


            if (_carrinhoCompra.CarrinhoCompraItens.Count == 0)
            {
                ModelState.AddModelError("","Seu carrinho está vazio, inclua um lanche...");
            }

            if (ModelState.IsValid)
            {
                _pedidoRepository.CriarPedido(pedido);
                _carrinhoCompra.LimparCarrinho();
                return RedirectToAction("CheckouCompleto");
            }

            return View(pedido);
        }

        public IActionResult CheckouCompleto()
        {

            ViewBag.CheckoutCompletoMensagem = "Obrigado pelo seu pedido!";
            return View();

        }
    }
}