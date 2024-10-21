using AutoMapper;
using IKEA.BLL.CustomModels.Departments;
using IKEA.DAL.Entities.Departmetns;
using IKEA.PL.ViewModels.Departments;

namespace IKEA.PL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {

            #region Employee

            #endregion

            #region Department
            CreateMap<DepartmentDetailsToReturnDto, DepartmentEditViewModel>();
            CreateMap<DepartmentEditViewModel,DepartmentToUpdateDto>();
            #endregion


        }
    }
}
