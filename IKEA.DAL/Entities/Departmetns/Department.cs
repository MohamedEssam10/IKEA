using IKEA.DAL.Entities.Employees;
using System.ComponentModel.DataAnnotations;

namespace IKEA.DAL.Entities.Departmetns
{
    public class Department : ModelBase
    {
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateOnly CreationDate { get; set; }
        public virtual ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
