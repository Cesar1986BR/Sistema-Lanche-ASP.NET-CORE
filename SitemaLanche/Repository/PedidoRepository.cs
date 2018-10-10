using SitemaLanche.Context;
using SitemaLanche.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SitemaLanche.Repository
{
    public class PedidoRepository: IPedidoRepository
    {
        private readonly CarrinhoCompra _carrinhoCompra;
        private readonly AppDbContext _appDbContext;

        public PedidoRepository(AppDbContext appDbContext, CarrinhoCompra carrinhoCompra)
        {
            _carrinhoCompra = carrinhoCompra;
            _appDbContext = appDbContext;
        }

        public void CriarPedido(Pedido pedido)
        {
            pedido.PedidoEnviado = DateTime.Now;
            _appDbContext.Pedidos.Add(pedido);

            var carrinhoCompraItens = _carrinhoCompra.CarrinhoCompraItens;

            foreach (var carrinhoitem in carrinhoCompraItens)
            {
                var pedidoDetalhe = new PedidoDetalhe
                {
                    Quantidade = carrinhoitem.Quantidade,
                    LancheId = carrinhoitem.Lanche.lancheId,
                    PedidoId = pedido.PedidoId,
                    Preco = carrinhoitem.Lanche.Preco,
                };

                _appDbContext.PedidoDetalhes.Add(pedidoDetalhe); 
            }
            _appDbContext.SaveChanges();



        }
    }
}
