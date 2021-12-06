
using Microsoft.AspNetCore.Mvc;
using NerdStore.Vendas.Application.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NerdStore.WebApp.MVC.Extensions
{
    public class CartViewComponent : ViewComponent
    {
        private readonly IPedidoQueries _pedidoQueries;

        // TODO: Obter cliente logado
        protected Guid ClienteId = Guid.Parse("54f589fc-2f22-4980-a097-e29828020119");


        public CartViewComponent(IPedidoQueries pedidoQueries)
        {
            _pedidoQueries = pedidoQueries;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var carrinho = await _pedidoQueries.ObterCarrinhoCliente(ClienteId);
            var itens = carrinho?.Items.Count ?? 0;

            return View(itens);
        }
    }
}