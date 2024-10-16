using IKEA.BLL.CustomModels.Departments;

namespace IKEA.BLL.Services.Departments
{
    public interface IDepartmentServices
    {
        IEnumerable<DepartmentToReturnDto> GetAllDepartments();
        DepartmentDetailsToReturnDto? GetDepartmentById(int id);
        int CreateDepartment(DepartmentToCreateDto departmentDto);
        bool DeleteDepartment(int Id);
        int UpdateDepartment(DepartmentToUpdateDto departmentDto);


    }
}
