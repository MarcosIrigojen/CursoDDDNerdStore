using MediatR;
using NerdStore.Core.Messages;
using NerdStore.Vendas.Domain;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NerdStore.Vendas.Application.Commands
{
    public class PedidoCommandHandler : IRequestHandler<AdicionarItemPedidoCommand, bool>
    {
        private readonly IPedidoRepository _pedidoRepository;

        public PedidoCommandHandler(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<bool> Handle(AdicionarItemPedidoCommand comando, CancellationToken cancellationToken)
        {
            if (!ValidarComando(comando)) return false;

            var pedido = await _pedidoRepository.ObterPedidoRascunhoPorClienteId(comando.ClienteId);

            var pedidoItem = new PedidoItem(comando.ProdutoId, comando.Nome, comando.Quantidade, comando.ValorUnitario);

            if (pedido == null)
            {
                pedido = Pedido.PedidoFactory.NovoPedidoRascunho(comando.ClienteId);
                pedido.AdicionarItem(pedidoItem);

                _pedidoRepository.Adicionar(pedido);
            }
            else
            {
                var pedidoItemExistente = pedido.PedidoItemExistente(pedidoItem);
                pedido.AdicionarItem(pedidoItem);

                if (pedidoItemExistente)
                {
                    _pedidoRepository.AtualizarItem(pedido.PedidoItems.FirstOrDefault(p => p.ProdutoId == pedidoItem.ProdutoId));
                }
                else
                {
                    pedido.AdicionarItem(pedidoItem);

                }
            }

            return await _pedidoRepository.UnitOfWork.Commit();
        }

        private bool ValidarComando(Command message)
        {
            if (message.EhValido()) return true;

            foreach (var error in message.ValidationResult.Errors)
            {
                // lançar evento de erro
            }

            return false;
        }
    }
}