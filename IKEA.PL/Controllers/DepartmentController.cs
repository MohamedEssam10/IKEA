using IKEA.BLL.CustomModels.Departments;
using IKEA.BLL.Services.Departments;
using IKEA.PL.ViewModels.Departments;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers
{
    public class DepartmentController : Controller
    {

        #region Services
        private readonly IDepartmentServices departmentservice;
        private readonly ILogger<DepartmentController> logger;
        private readonly IWebHostEnvironment environment;

        
        public DepartmentController(IDepartmentServices departmentservice, ILogger<DepartmentController> logger, IWebHostEnvironment environment)
        {
            this.departmentservice = departmentservice;
            this.logger = logger;
            this.environment = environment;
        }
        #endregion

        #region Index
        [HttpGet]
        public IActionResult Index()
        {
            var departments = departmentservice.GetAllDepartments();
            return View(departments);
        }
        #endregion

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DepartmentToCreateDto department)
        {
            if (!ModelState.IsValid)
                return View("Error");

            var Result = departmentservice.CreateDepartment(department);
            return RedirectToAction("Index");
        }
        #endregion

        #region Delete
        //[HttpGet]
        //public IActionResult Delete(int? id)
        //{
        //    int Id = id ?? -1;
        //    var department = departmentservice.GetDepartmentById(Id);
        //    return View(department);
        //}

        [HttpPost]
        public IActionResult Delete(int Id)
        {
            var deleted = departmentservice.DeleteDepartment(Id);
            return RedirectToAction("Index");
        }
        #endregion

        #region Details
        [HttpGet]
        public IActionResult Details(int Id)
        {
            var department = departmentservice.GetDepartmentById(Id);
            return View(department);
        }
        #endregion

        #region Update
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var department = departmentservice.GetDepartmentById(Id);
            return View(new DepartmentEditViewModel()
            {
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                CreationDate = department.CreationDate
            });
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int Id, DepartmentEditViewModel department)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var departmentToUpdate = new DepartmentToUpdateDto()
            {
                Id = Id,
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                CreationDate = department.CreationDate
            };

            departmentservice.UpdateDepartment(departmentToUpdate);
            return RedirectToAction("Index");
        } 
        #endregion


    }
}
