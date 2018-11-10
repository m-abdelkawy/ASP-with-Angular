using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hacienda.Task.Repository.Repositories
{
    public interface IRepository<TEntity>
    {
        //CRUD Operations

        //Create
        void Add(TEntity entity);

        //Retrieve
        IQueryable<TEntity> GetAll();
        TEntity GetById(params object[] id);

        //UPDATE
        void Update(TEntity entity, UnitOfWork uow);


        //DELETE
        void Delete(params object[] id);
    }
}
