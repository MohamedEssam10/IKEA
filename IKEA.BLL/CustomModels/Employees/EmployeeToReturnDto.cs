using IKEA.DAL.Common.Enums;
using IKEA.DAL.Entities.Departmetns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.CustomModels.Employees
{
    public class EmployeeToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public int Age { get; set; }
        public double Salary { get; set; }
        public DateOnly HiringDate { get; set; }
        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }
        public int? DepartmentId { get; set; }
        public string? Department { get; set; }

        public string? Image {  get; set; }
    }
}
