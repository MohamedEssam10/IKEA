using IKEA.BLL.CustomModels.Employees;
using IKEA.DAL.Entities.Employees;
using IKEA.DAL.Repositories.Employees;
using Microsoft.EntityFrameworkCore;

namespace IKEA.BLL.Services.Employees
{
    public class EmployeeServices : IEmployeeServices
    {
        private readonly IEmployeeRepository repository;

        public EmployeeServices(IEmployeeRepository repository)
        {
            this.repository = repository;
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
                LastModifiedOn = DateTime.UtcNow
            };

            return repository.Add(CreatedEmployee);
        }

        public bool DeleteEmployee(int Id)
        {
            var employee = repository.GetById(Id);
            if (employee == null) return false;

            return repository.Delete(employee) > 0;
        }

        public IEnumerable<EmployeeToReturnDto> GetAllEmployees()
        {
            var employee = repository.GetAllAsQueryable().Where(E => !E.IsDeleted).Select(E => new EmployeeToReturnDto
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

            }).AsNoTracking().ToList();

            return employee;
        }

        public EmployeeDetailsToReturnDto? GetEmployeeById(int id)
        {
            var employee = repository.GetById(id);

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
                EmployeeType = employee.EmployeeType
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
                LastModifiedOn = DateTime.UtcNow
            };

            return repository.Update(UpdatedEmployee);
        }
    }
}

