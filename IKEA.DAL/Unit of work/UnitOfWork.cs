using IKEA.DAL.Data;
using IKEA.DAL.Repositories.Departments;
using IKEA.DAL.Repositories.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Unit_of_work
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext dbcontext;

        public IEmployeeRepository EmployeeRepository { get => new EmployeeRepository(dbcontext); }
        public IDepartmentRepository DepartmentRepository { get => new DepartmentRepository(dbcontext); }

        public UnitOfWork(ApplicationDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        public int Complete()
        {
            return dbcontext.SaveChanges();
        }
        public void Dispose()
        {
            dbcontext.Dispose();
        }
    }
}
