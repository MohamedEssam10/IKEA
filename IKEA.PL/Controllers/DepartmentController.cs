﻿using AutoMapper;
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
        private readonly IMapper mapper;


        public DepartmentController(IDepartmentServices departmentservice, ILogger<DepartmentController> logger, IWebHostEnvironment environment, IMapper mapper)
        {
            this.mapper = mapper;
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
        //[ValidateAntiForgeryToken]
        public IActionResult Create(DepartmentToCreateDto department)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var Result = departmentservice.CreateDepartment(department);

            if (Result > 0)
                TempData["Message"] = "Department has been Created Successfully.";
            else
                TempData["Message"] = "An Error Ocurred While Creating a new Department.";
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
        //[ValidateAntiForgeryToken]
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

            var departmentVM = mapper.Map<DepartmentDetailsToReturnDto, DepartmentEditViewModel>(department);
            return View(departmentVM);

        }

        [HttpPost]
        //[ValidateAntiForgeryToken]

        public IActionResult Edit([FromRoute] int Id, DepartmentEditViewModel department)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var departmentToUpdate = mapper.Map<DepartmentEditViewModel,DepartmentToUpdateDto>(department);

            departmentservice.UpdateDepartment(departmentToUpdate);
            return RedirectToAction("Index");
        } 
        #endregion


    }
}
