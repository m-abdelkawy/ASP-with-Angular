using Hacienda.Task.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hacienda.Task.Repository.Repositories
{
    public class GenderRepository : RepositoryBase<Gender>
    {
        public GenderRepository(Model1 _ctx) : base(_ctx)
        {

        }
    }
}
