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
        public void Add(T Entity);
        public void Delete(T Entity);
        public void Update(T Entity);
    }
}
