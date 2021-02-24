﻿using System;
using System.Threading.Tasks;

namespace NerdStore.Catalogo.Domain.Tests
{
    public interface IEstoqueService : IDisposable
    {
        Task<bool> DebitarEstoque(Guid produtoId, int quantidade);
        Task<bool> ReporEstoque(Guid produtoId, int quantidade);
    }
}
