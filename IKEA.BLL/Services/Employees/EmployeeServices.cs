using IKEA.BLL.Common.Services.Attachments;
using IKEA.BLL.CustomModels.Employees;
using IKEA.DAL.Entities.Departmetns;
using IKEA.DAL.Entities.Employees;
using IKEA.DAL.Repositories.Employees;
using IKEA.DAL.Unit_of_work;
using Microsoft.EntityFrameworkCore;

namespace IKEA.BLL.Services.Employees
{
    public class EmployeeServices : IEmployeeServices
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IAttachmentService attachmentservice;
        public EmployeeServices(IUnitOfWork unitOfWork, IAttachmentService attachmentservice)
        {
            this.unitOfWork = unitOfWork;
            this.attachmentservice = attachmentservice;
        }

        public int CreateEmployee(EmployeeToCreateDto employeeDto)
        {
            var CreatedEmployee = new Employee()
            {
                Name = employeeDto.Name,
                Salary = employeeDto.Salary,
                Address = employeeDto.Address,
                Age = employeeDto.Age,
                PhoneNumber = employeeDto.PhoneNumber,
                HiringDate = employeeDto.HiringDate,
                Email = employeeDto.Email,
                Gender = employeeDto.Gender,
                EmployeeType = employeeDto.EmployeeType,
                CreatedBy = 1,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow,
                DepartmentId = employeeDto.DepartmentId,
                
            };

            if (employeeDto.Image is not null)
                CreatedEmployee.Image = attachmentservice.Upload(employeeDto.Image, "Images");

            unitOfWork.EmployeeRepository.Add(CreatedEmployee);
            return unitOfWork.Complete();
        }

        public bool DeleteEmployee(int Id)
        {
            var EmployeeRepo = unitOfWork.EmployeeRepository;
            var employee = EmployeeRepo.GetById(Id);
            if (employee == null) return false;

            EmployeeRepo.Delete(employee);
            return unitOfWork.Complete() > 0;
        }

        public IEnumerable<EmployeeToReturnDto> GetEmployees(string search)
        {
            var employee = unitOfWork.EmployeeRepository.GetAllAsQueryable().Where(E => !E.IsDeleted && (string.IsNullOrEmpty(search) || E.Name.Contains(search)) ).Include(E => E.Department).Select(E => new EmployeeToReturnDto
            {
                Id = E.Id,
                Name = E.Name,
                Salary = E.Salary,
                Address = E.Address,
                Age = E.Age,
                PhoneNumber = E.PhoneNumber,
                HiringDate = E.HiringDate,
                Email = E.Email,
                Gender = E.Gender,
                EmployeeType = E.EmployeeType,
                DepartmentId = E.DepartmentId,
                Department = E.Department.Name,
                Image = E.Image

            }).AsNoTracking().ToList();

            return employee;
        }

        public EmployeeDetailsToReturnDto? GetEmployeeById(int id)
        {
            var employee = unitOfWork.EmployeeRepository.GetById(id);

            if (employee is null)
                return null;

            return new EmployeeDetailsToReturnDto()
            {
                Id = employee.Id,
                CreatedBy = employee.CreatedBy,
                CreatedOn = employee.CreatedOn,
                LastModifiedBy = employee.LastModifiedBy,
                LastModifiedOn = employee.LastModifiedOn,
                IsDeleted = employee.IsDeleted,
                Name = employee.Name,
                Age = employee.Age,
                Salary = employee.Salary,
                Address = employee.Address,
                PhoneNumber = employee.PhoneNumber,
                Email = employee.Email,
                Gender = employee.Gender,
                EmployeeType = employee.EmployeeType,
                DepartmentId = employee.DepartmentId,
                Department = employee.Department.Name,
                Image = employee.Image
            };
        }

        public int UpdateEmployee(EmployeeToUpdateDto employeeDto)
        {
            var UpdatedEmployee = new Employee()
            {
                Id = employeeDto.Id,
                Name = employeeDto.Name,
                Age = employeeDto.Age,
                Salary = employeeDto.Salary,
                Address = employeeDto.Address,
                PhoneNumber = employeeDto.PhoneNumber,
                Email = employeeDto.Email,
                Gender = employeeDto.Gender,
                EmployeeType = employeeDto.EmployeeType,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow,
                DepartmentId = employeeDto.DepartmentId,
                Image = employeeDto.Image
            };

            unitOfWork.EmployeeRepository.Update(UpdatedEmployee);
            return unitOfWork.Complete();
        }
    }
}

