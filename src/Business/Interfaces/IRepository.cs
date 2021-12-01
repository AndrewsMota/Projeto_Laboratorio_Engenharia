using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Business.Models;

namespace Business.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entidade
    {
        Task Adicionar(TEntity entity);
        Task Atualizar(TEntity entity);
        Task RemoverPorId(Guid id);
        Task<List<TEntity>> ObterTodos();
        Task<TEntity> ObterPorId(Guid id);
        Task<IList<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate);
        Task<int> SaveChanges();
    }
}