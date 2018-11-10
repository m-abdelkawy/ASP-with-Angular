using Hacienda.Task.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hacienda.Task.Repository.Repositories
{
    public class CityRepository : RepositoryBase<City>
    {
        public CityRepository(Model1 _ctx) : base(_ctx)
        {

        }

        public virtual IQueryable<City> GetByCountry(Country country)
        {
            return GetAll().Where(c => c.CountryId == country.Id);
        }
    }
}
