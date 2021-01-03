using Microsoft.EntityFrameworkCore;
using NPista.Data.EFCore.Repositorios.Intefarces;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NPista.Data.EFCore.Repositorios
{
    public abstract class RepositorioBase<TEntity> : IDisposable, IRepositorio<TEntity> where TEntity : class
    {
        private readonly DbContext _contexto;

        public RepositorioBase(DbContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<TEntity> AdicionarAsync(TEntity entity)
        {
            ValidateEntity(entity);

            var addedEntity = await _contexto.Set<TEntity>().AddAsync(entity);
            await _contexto.SaveChangesAsync();

            return addedEntity.Entity;
        }

        public async Task<TEntity> AtualizarAsync(TEntity entity)
        {
            ValidateEntity(entity);

            var entityUpdated = _contexto.Set<TEntity>().Update(entity).Entity;
            await _contexto.SaveChangesAsync();

            return entityUpdated;
        }

        public IQueryable<TEntity> Buscar(Expression<Func<TEntity, bool>> clasulaWhere)
        {
            return BuscarTodos().Where(clasulaWhere).AsQueryable();
        }

        public IQueryable<TEntity> BuscarTodos()
        {
            return _contexto.Set<TEntity>().AsQueryable();
        }

        public async Task ExcluirAsync(Expression<Func<TEntity, bool>> clasulaWhere)
        {
            _contexto.Set<TEntity>()
                .Where(clasulaWhere).ToList()
                .ForEach(del => _contexto.Set<TEntity>().Remove(del));
            await _contexto.SaveChangesAsync();
        }

        public void ValidateEntity(TEntity entity)
        {
            if(entity == null) new ArgumentNullException("entity");
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

    }
}
