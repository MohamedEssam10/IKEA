using IKEA.BLL.CustomModels.Departments;
using IKEA.BLL.Services.Departments;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentServices departmentservice;
        private readonly ILogger<DepartmentController> logger;
        private readonly IWebHostEnvironment environment;

        public DepartmentController(IDepartmentServices departmentservice, ILogger<DepartmentController> logger, IWebHostEnvironment environment)
        {
            this.departmentservice = departmentservice;
            this.logger = logger;
            this.environment = environment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var departments = departmentservice.GetAllDepartments();
            return View(departments);
        }

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

        public IActionResult Delete(int Id)
        {
            departmentservice.DeleteDepartment(Id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(int Id)
        {
            var department = departmentservice.GetDepartmentById(Id);
            return View(department);
        }


    }
}
