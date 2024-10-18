using IKEA.DAL.Data;
using IKEA.DAL.Entities.Departmetns;
using IKEA.DAL.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace IKEA.DAL.Repositories.Departments
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext dbcontext) : base(dbcontext){}


    }
}
