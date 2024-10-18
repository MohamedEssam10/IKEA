using IKEA.BLL.CustomModels.Departments;
using IKEA.DAL.Entities.Departmetns;
using IKEA.DAL.Repositories.Common;
using IKEA.DAL.Repositories.Departments;
using Microsoft.EntityFrameworkCore;

namespace IKEA.BLL.Services.Departments
{
    public class DepartmentService : IDepartmentServices
    {
        private readonly IDepartmentRepository repository;

        public DepartmentService(IDepartmentRepository repository)
        {
            this.repository = repository;
        }

        public int CreateDepartment(DepartmentToCreateDto departmentDto)
        {
            var CreatedDepartment = new Department()
            { 
                Name = departmentDto.Name,
                Code = departmentDto.Code,
                Description = departmentDto.Description,
                CreationDate = departmentDto.CreationDate,
                CreatedBy = 1,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow
            };
            
            return repository.Add(CreatedDepartment);
        }

        public bool DeleteDepartment(int Id)
        {
            var department = repository.GetById(Id);
            if(department == null) return false;

            return repository.Delete(department) > 0;
        }

        public IEnumerable<DepartmentToReturnDto> GetAllDepartments()
        {
            var department = repository.GetAllAsQueryable().Select( D => new DepartmentToReturnDto
                {
                    Id = D.Id,
                    Code = D.Code,
                    Name = D.Name,
                    CreationDate = D.CreationDate
            }).AsNoTracking().ToList();

            return department;
        }

        public DepartmentDetailsToReturnDto? GetDepartmentById(int id)
        {
            var department = repository.GetById(id);

            if(department is null)
                return null;

            return new DepartmentDetailsToReturnDto()
            {
                Id = department.Id,
                CreatedBy = department.CreatedBy,
                CreatedOn = department.CreatedOn,
                LastModifiedBy  = department.LastModifiedBy,
                LastModifiedOn = department.LastModifiedOn,
                IsDeleted = department.IsDeleted,
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                CreationDate = department.CreationDate
            };
        }

        public int UpdateDepartment(DepartmentToUpdateDto departmentDto)
        {
            var UpdatedDepartment = new Department()
            {
                Id = departmentDto.Id,
                Name = departmentDto.Name,
                Code = departmentDto.Code,
                Description = departmentDto.Description,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow
            };

            return repository.Update(UpdatedDepartment);
        }
    }
}
