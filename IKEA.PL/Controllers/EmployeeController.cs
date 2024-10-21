using IKEA.BLL.CustomModels.Employees;
using IKEA.BLL.Services.Departments;
using IKEA.BLL.Services.Employees;
using IKEA.DAL.Common.Enums;
using IKEA.DAL.Entities.Departmetns;
using IKEA.PL.ViewModels.Empolyees;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers
{
    public class EmployeeController : Controller
    {
        #region Services
        private readonly IEmployeeServices employeeservice;
        private readonly ILogger<EmployeeController> logger;
        private readonly IWebHostEnvironment environment;


        public EmployeeController(IEmployeeServices employeeservice, ILogger<EmployeeController> logger, IWebHostEnvironment environment)
        {
            this.employeeservice = employeeservice;
            this.logger = logger;
            this.environment = environment;
        }
        #endregion

        #region Index
        [HttpGet]
        public IActionResult Index(string search)
        {
            var employees = employeeservice.GetEmployees(search);

            //if (HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            //{
            //    // Return partial view for AJAX requests
            //    return PartialView("_EmployeeListPartial", employees);
            //}
            if (string.IsNullOrEmpty(search))
                return View(employees);

            return PartialView("Partials/_EmployeeListPartial", employees);  
        }
        #endregion

        #region Create
        [HttpGet]
        
        public IActionResult Create([FromServices]IDepartmentServices departmentservice)
        {
            ViewData["Departments"] = departmentservice.GetAllDepartments();

            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]

        public IActionResult Create(EmployeeToCreateDto employee)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var Result = employeeservice.CreateEmployee(employee);

            if (Result > 0)
                TempData["Message"] = "Employee has been Created Successfully.";
            else
                TempData["Message"] = "An Error Ocurred While Creating a new Employee.";
                
            return RedirectToAction("Index");
        }
        #endregion

        #region Delete
        //[HttpGet]
        //public IActionResult Delete(int? id)
        //{
        //    int Id = id ?? -1;
        //    var employee = employeeservice.GetEmployeeById(Id);
        //    return View(employee);
        //}

        [HttpPost]
        //[ValidateAntiForgeryToken]

        public IActionResult Delete(int Id)
        {
            var deleted = employeeservice.DeleteEmployee(Id);
            return RedirectToAction("Index");
        }
        #endregion

        #region Details
        [HttpGet]
        public IActionResult Details(int Id)
        {
            var employee = employeeservice.GetEmployeeById(Id);
            return View(employee);
        }
        #endregion

        #region Update
        [HttpGet]
        public IActionResult Edit(int Id, [FromServices]IDepartmentServices departmentservice)
        {
            ViewData["Departments"] = departmentservice.GetAllDepartments();

            var employee = employeeservice.GetEmployeeById(Id);
            return View(new EmployeeEditViewModel()
            {
                Name         = employee.Name,
                Address      = employee.Address,
                Email        = employee.Email,
                PhoneNumber  = employee.PhoneNumber,
                Age          = employee.Age,
                Salary       = employee.Salary,
                HiringDate   = employee.HiringDate,
                Gender       = employee.Gender,
                EmployeeType = employee.EmployeeType,
                DepartmentId = employee.DepartmentId
    });
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]

        public IActionResult Edit([FromRoute] int Id, EmployeeEditViewModel employee)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var employeeToUpdate = new EmployeeToUpdateDto()
            {
                Id           = Id,
                Name         = employee.Name,
                Address      = employee.Address,
                Email        = employee.Email,
                PhoneNumber  = employee.PhoneNumber,
                Age          = employee.Age,
                Salary       = employee.Salary,
                HiringDate   = employee.HiringDate,
                Gender       = employee.Gender,
                EmployeeType = employee.EmployeeType,
                DepartmentId = employee.DepartmentId,
                Image        = employee.Image

            };

            employeeservice.UpdateEmployee(employeeToUpdate);
            return RedirectToAction("Index");
        }
        #endregion}
    }
}
