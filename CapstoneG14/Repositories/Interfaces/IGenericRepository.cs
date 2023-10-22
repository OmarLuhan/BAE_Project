using System.Linq.Expressions;

namespace CapstoneG14.Repositories.Interfaces
{
    public interface IGenericRepository
    {
        public interface IGenericRepository<TEntity> where TEntity : class
        {
            Task<TEntity?> Obtener(Expression<Func<TEntity, bool>> filters);
            Task<TEntity> Create(TEntity entity);
            Task<bool> Update(TEntity entity);
            Task<bool> Delete(TEntity entity);
            Task<IQueryable<TEntity>> Consultar(Expression<Func<TEntity, bool>>? filters = null);
        }
    }
}