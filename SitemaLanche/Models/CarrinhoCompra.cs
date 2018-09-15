using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SitemaLanche.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SitemaLanche.Models
{
    public class CarrinhoCompra
    {
        private readonly AppDbContext _context;

        public CarrinhoCompra(AppDbContext contexto)
        {
            _context = contexto;
        }
        public string CarrinhoCompraId { get; set; }// tipo GUID
        public List<CarrinhoCompraItem> CarrinhoCompraItems { get; set; }
       

        public static CarrinhoCompra GetCarrinho(System.IServiceProvider service)
        {

            //define uma sessão
            ISession session = service.GetRequiredService <IHttpContextAccessor>()?.HttpContext.Session; //retorna nulo se não existir uma sessão, se tiver sessão retorna sessão do contexto

            //obtem um serviço do tipo do nosso contexto 
            var context = service.GetService<AppDbContext>();

            //obtem ou gera o Id do carrinho
            string carringoId = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();// retorna id do carrinho se ele ja exsitir, se nao cria nova chave GUID

            //atribui o id do carrinho na Sessão
            session.SetString("CarrinhoId", carringoId);

            //retorna o carrinho com o contexto e o Id atribuido ou obtido
            return new CarrinhoCompra(context)
            {
                CarrinhoCompraId = carringoId
            };
        }

        public void AdiconarAoCarrinho(Lanche lanche, int quantidade)
        {
            var carrinhoCompraItem = _context.CarrinhoCompraItens.SingleOrDefault(s => s.Lanche.lancheId == lanche.lancheId && s.CarrinhoCompraId == CarrinhoCompraId);

            if (carrinhoCompraItem == null)
            {
                carrinhoCompraItem = new CarrinhoCompraItem
                {
                    CarrinhoCompraId = CarrinhoCompraId,
                    Lanche = lanche,
                    Quantidade = 1
                };

                _context.CarrinhoCompraItens.Add(carrinhoCompraItem);
            }
            else
            {
                carrinhoCompraItem.Quantidade++;
            }
            _context.SaveChanges();
        }

        public int RemoverDoCarrinho(Lanche lanche)
        {
            var carrinhoCompraItem =
                    _context.CarrinhoCompraItens.SingleOrDefault(
                        s => s.Lanche.lancheId == lanche.lancheId && s.CarrinhoCompraId == CarrinhoCompraId);

            var quantidadeLocal = 0;

            if (carrinhoCompraItem != null)
            {
                if (carrinhoCompraItem.Quantidade > 1)
                {
                    carrinhoCompraItem.Quantidade--;
                    quantidadeLocal = carrinhoCompraItem.Quantidade;
                }
                else
                {
                    _context.CarrinhoCompraItens.Remove(carrinhoCompraItem);
                }
            }

            _context.SaveChanges();

            return quantidadeLocal;
        }

        public List<CarrinhoCompraItem> GetCarrinhoCompraItens()
        {
            return CarrinhoCompraItems ??
                   (CarrinhoCompraItems =
                       _context.CarrinhoCompraItens.Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
                           .Include(s => s.Lanche)
                           .ToList());
        }

        public void LimparCarrinho()
        {
            var carrinhoItens = _context.CarrinhoCompraItens
                                 .Where(carrinho => carrinho.CarrinhoCompraId == CarrinhoCompraId);

            _context.CarrinhoCompraItens.RemoveRange(carrinhoItens);

            _context.SaveChanges();
        }

        public decimal GetCarrinhoCompraTotal()
        {
            var total = _context.CarrinhoCompraItens.Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
                .Select(c => c.Lanche.Preco * c.Quantidade).Sum();

            return total;
        }

    }
}
