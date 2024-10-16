using IKEA.DAL.Entities;

namespace IKEA.DAL.Repositories.Departments
{
    public interface IDepartmentRepository
    {
        public IEnumerable<Department> GetAll(bool WithAsNoTracking = true);
        public IQueryable<Department> GetAllAsQueryable(bool WithAsNoTracking = true);
        public Department? GetById(int Id);
        public int Add(Department Entity);
        public int Delete(Department Entity);
        public int Update(Department Entity);
    }
}
