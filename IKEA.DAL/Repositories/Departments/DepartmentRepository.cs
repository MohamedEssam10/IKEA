using IKEA.DAL.Data;
using IKEA.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace IKEA.DAL.Repositories.Departments
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext dbcontext;
        public DepartmentRepository(ApplicationDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public IEnumerable<Department> GetAll(bool WithAsNoTracking = true)
        {
            if (WithAsNoTracking)
                return dbcontext.Departments.AsNoTracking().ToList();
            return dbcontext.Departments.ToList();
        }

        public IQueryable<Department> GetAllAsQueryable(bool WithAsNoTracking = true)
        {
            return dbcontext.Departments;
        }

        public Department? GetById(int Id)
        {
            //var department = dbcontext.Departments.Local.FirstOrDefault(D => D.Id == Id);

            //if (department is null)
            //    department = dbcontext.Departments.FirstOrDefault(D => D.Id == Id);

            //return department;

            return dbcontext.Departments.Find(Id);
        }
        public int Add(Department Entity)
        {
            dbcontext.Departments.Add(Entity);
            return dbcontext.SaveChanges(); // returns how many rows affected
        }
        public int Update(Department Entity)
        {
            dbcontext.Departments.Update(Entity);
            return dbcontext.SaveChanges();
        }
        public int Delete(Department Entity)
        {
            dbcontext.Departments.Remove(Entity);
            return dbcontext.SaveChanges();
        }
        
    }
}
