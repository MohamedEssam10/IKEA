using IKEA.BLL.CustomModels.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Services.Employees
{
    public interface IEmployeeServices
    {
        IEnumerable<EmployeeToReturnDto> GetEmployees(string search);
        EmployeeDetailsToReturnDto? GetEmployeeById(int id);
        int CreateEmployee(EmployeeToCreateDto employeeDto);
        bool DeleteEmployee(int Id);
        int UpdateEmployee(EmployeeToUpdateDto employeeDto);
    }
}
