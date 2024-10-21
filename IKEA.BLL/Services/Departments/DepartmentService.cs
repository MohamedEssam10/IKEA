using IKEA.BLL.CustomModels.Departments;
using IKEA.DAL.Entities.Departmetns;
using IKEA.DAL.Repositories.Common;
using IKEA.DAL.Repositories.Departments;
using IKEA.DAL.Unit_of_work;
using Microsoft.EntityFrameworkCore;

namespace IKEA.BLL.Services.Departments
{
    public class DepartmentService : IDepartmentServices
    {
        private readonly IUnitOfWork unitofwork;

        public DepartmentService(IUnitOfWork unitofwork)
        {
            this.unitofwork = unitofwork;
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
            
            unitofwork.DepartmentRepository.Add(CreatedDepartment);
            return unitofwork.Complete();
        }

        public bool DeleteDepartment(int Id)
        {
            var DepartmentRepo = unitofwork.DepartmentRepository;
            var department = DepartmentRepo.GetById(Id);
            if(department == null) return false;

            DepartmentRepo.Delete(department);
            return unitofwork.Complete() > 0;
        }

        public IEnumerable<DepartmentToReturnDto> GetAllDepartments()
        {
            var department = unitofwork.DepartmentRepository.GetAllAsQueryable().Select( D => new DepartmentToReturnDto
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
            var department = unitofwork.DepartmentRepository.GetById(id);

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

            unitofwork.DepartmentRepository.Update(UpdatedDepartment);
            return unitofwork.Complete();
        }
    }
}
