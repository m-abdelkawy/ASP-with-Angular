using Hacienda.Task.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hacienda.Task.Repository.Repositories
{
    public class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private DbSet<TEntity> _set;
        public RepositoryBase(Model1 _ctx)
        {
            _set = _ctx.Set<TEntity>();
        }
        public void Add(TEntity entity)
        {
            _set.Add(entity);
        }

        public void Delete(params object[] id)
        {
            _set.Remove(_set.Find(id));
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return _set;
        }

        public virtual TEntity GetById(params object[] id)
        {
            return _set.Find(id);
        }

        public void Update(TEntity entity, UnitOfWork uow)
        {
            _set.Attach(entity);
            uow.Update(entity);
        }
    }
}
