using IKEA.DAL.Entities;
using IKEA.DAL.Entities.Departmetns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Repositories.Common
{
    public interface IGenericRepository<T> where T : ModelBase
    {
        public IEnumerable<T> GetAll(bool WithAsNoTracking = true);
        public IQueryable<T> GetAllAsQueryable();
        public T? GetById(int Id);
        public int Add(T Entity);
        public int Delete(T Entity);
        public int Update(T Entity);
    }
}
