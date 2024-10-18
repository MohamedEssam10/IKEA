using IKEA.DAL.Common.Enums;

namespace IKEA.PL.ViewModels.Empolyees
{
    public class EmployeeEditViewModel
    {
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public int Age { get; set; }
        public double Salary { get; set; }
        public DateOnly HiringDate { get; set; }
        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }
    }
}
