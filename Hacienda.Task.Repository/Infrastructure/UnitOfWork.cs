using Hacienda.Task.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hacienda.Task.Repository.Repositories
{
    public class UnitOfWork : IDisposable
    {
        private Model1 _ctx = new Model1();
        public PersonRepository PersonRepository
        {
            get { return new PersonRepository(_ctx); }
        }

        public CountryRepository CountryRepository
        {
            get { return new CountryRepository(_ctx); }
        }

        public CityRepository CityRepository
        {
            get { return new CityRepository(_ctx); }
        }

        public GenderRepository GenderRepository
        {
            get { return new GenderRepository(_ctx); }
        }

        internal void Update<TEntity>(TEntity entity) where TEntity : class
        {
            _ctx.Entry<TEntity>(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public int Commit()
        {
            return _ctx.SaveChanges();
        }
        public void Dispose()
        {
            GC.SuppressFinalize(_ctx);
        }
    }
}
