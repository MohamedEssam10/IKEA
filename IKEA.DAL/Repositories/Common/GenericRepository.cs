using IKEA.DAL.Data;
using IKEA.DAL.Entities;
using IKEA.DAL.Entities.Employees;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Repositories.Common
{
    public class GenericRepository<T> : IGenericRepository<T> where T : ModelBase
    {
        private protected readonly ApplicationDbContext dbcontext;

        public GenericRepository(ApplicationDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public IEnumerable<T> GetAll(bool WithAsNoTracking = true)
        {
            if (WithAsNoTracking)
                return dbcontext.Set<T>().AsNoTracking().ToList();
            return dbcontext.Set<T>().ToList();
        }
        public IQueryable<T> GetAllAsQueryable()
        {
            return dbcontext.Set<T>();
        }
        public T? GetById(int Id)
        {
            //var Employee = dbcontext.Employees.Local.FirstOrDefault(D => D.Id == Id);

            //if (Employee is null)
            //    Employee = dbcontext.Employees.FirstOrDefault(D => D.Id == Id);

            //return Employee;

            return dbcontext.Set<T>().Find(Id);
        }
        public void Add(T Entity) => dbcontext.Set<T>().Add(Entity);
                   
        public void Update(T Entity) => dbcontext.Set<T>().Update(Entity);
        
        public void Delete(T Entity)
        {
            if(Entity is Employee)
            {
                Entity.IsDeleted = true;
                Update(Entity); 
            }

            dbcontext.Set<T>().Remove(Entity);
        }
    }
}
