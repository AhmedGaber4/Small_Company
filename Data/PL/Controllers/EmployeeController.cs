using AutoMapper;
using Data;
using Logic.Interfaces;
using Logic.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PL.Helper;
using PL.Models;

namespace PL.Controllers
{
    public class EmployeeController : Controller
    {
       // private readonly IEmpRepositories _empRepositories  ;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ILogger<EmployeeController> _Logger { get; }

        public EmployeeController(
            IUnitOfWork unitOfWork,
            ILogger<EmployeeController> logger,
            IMapper mapper)
        {
            //_empRepositories = empRepositories;
           _unitOfWork = unitOfWork;
            _Logger = logger;
            _mapper = mapper;
        }

       
        public IActionResult Goo(string SearchValue= "")
        {
           IEnumerable<Employee> employees; 
           IEnumerable<EmployeeViewModel> models;
            if (string.IsNullOrEmpty(SearchValue) )
            {
                employees = _unitOfWork._empRepositories.GetAll();
                models = _mapper.Map<IEnumerable<EmployeeViewModel>>(employees);
            }
                
            
            else
            { 
                employees = _unitOfWork._empRepositories.Search(SearchValue); 
                models = _mapper.Map<IEnumerable<EmployeeViewModel>>(employees); 
            }
                

          return View(models);
            
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.departments = _unitOfWork._depRepository.GetAll();

            return View(new EmployeeViewModel());
        }

        [HttpPost]
        public IActionResult Create(EmployeeViewModel model )
        {
           // ModelState["Department"].ValidationState =ModelValidationState.Valid;
           
            if (ModelState.IsValid)
            { 
                //Employee employee = new Employee 
                //{
                //    Name = model.Name,
                //    Email = model.Email,
                //    Adreses = model.Adreses,
                //    DepartmentId = model.DepartmentId,
                //    HiringDate = model.HiringDate,
                //    Salary = model.Salary,
                //    Isactive = model.Isactive

                //};
                var employee =_mapper.Map<Employee>(model);

                employee.ImageUrl = DocumentSettings.Uploadfile(model.Image,"Image");
                _unitOfWork._empRepositories.Add(employee);
                _unitOfWork.Complete();
                return RedirectToAction(nameof(Goo));
            }
            return View(model);


        }
        public IActionResult Details(int? id)
        {

            try
            {
                var v = _unitOfWork._empRepositories.GetEntityid(id);
                var employee = _mapper.Map<EmployeeViewModel>(v);
                if (id != null && employee != null)
                {

                    return View(employee);
                }


                return RedirectToAction("Error", "Home");




            }
            catch (Exception ex)
            {
                _Logger.LogError(ex.Message);
                return RedirectToAction("Error", "Home");
            }

        }
        public IActionResult Update(int? id)
        {
            
            if (id is null)
                return NotFound();
            var v = _unitOfWork._empRepositories.GetEntityid(id);
            var employee = _mapper.Map<EmployeeViewModel>(v);
            if (employee is null)
                return NotFound();
            ViewBag.departments = _unitOfWork._depRepository.GetAll();

            return View(employee);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, EmployeeViewModel model)
        {
           

            if (id != model.Id)
                return NotFound();

      // ModelState["Department"].ValidationState = ModelValidationState.Valid;
            try
            {
                if (ModelState.IsValid)
                {
                    var  e = _mapper.Map<Employee>(model);
                    DocumentSettings.Deletefile("Image", e.ImageUrl);
                    e.ImageUrl = DocumentSettings.Uploadfile(model.Image, "Image");
                    _unitOfWork._empRepositories.Update(e);
                    _unitOfWork.Complete();
                    return RedirectToAction(nameof(Goo));
                }


                ViewBag.departments = _unitOfWork._depRepository.GetAll();


            }
            catch (Exception ex)
            {
                _Logger.LogError(ex.Message);
                return RedirectToAction("Error", "Home");
            }
         
            return View(model);
        }
        public IActionResult Delete(int? id)
        {
            if (id is null)
                return NotFound();
            var v = _unitOfWork._empRepositories.GetEntityid(id);
            if (v is null)
                return NotFound();
            DocumentSettings.Deletefile("Image", v.ImageUrl);
            _unitOfWork._empRepositories.Delete(v);
            _unitOfWork.Complete();
            return RedirectToAction("Goo");

        }


    }
}
