using IKEA.BLL.CustomModels.Employees;
using IKEA.BLL.Services.Employees;
using IKEA.DAL.Common.Enums;
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
        public IActionResult Index()
        {
            var employees = employeeservice.GetAllEmployees();
            return View(employees);
        }
        #endregion

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeToCreateDto employee)
        {
            if (!ModelState.IsValid)
                return View("Error");

            var Result = employeeservice.CreateEmployee(employee);
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
        public IActionResult Edit(int Id)
        {
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
                EmployeeType = employee.EmployeeType

    });
        }

        [HttpPost]
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
                EmployeeType = employee.EmployeeType

            };

            employeeservice.UpdateEmployee(employeeToUpdate);
            return RedirectToAction("Index");
        }
        #endregion}
    }
}
