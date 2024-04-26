using AutoMapper;
using Data;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PL.Models;

namespace PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        // private readonly IDepRepositories _depRepositories;

        public ILogger<DepartmentController> _Logger { get; }

        public DepartmentController(
            IUnitOfWork unitOfWork,
            ILogger<DepartmentController> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            // _depRepositories = depRepositories;
            _Logger = logger;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult Go()
        {
            IEnumerable<DepartmentViewModel> Models;
            var departments = _unitOfWork._depRepository.GetAll();
            Models = _mapper.Map<IEnumerable<DepartmentViewModel>>(departments);
            return View(Models);
        }
        [HttpGet]
        public IActionResult Create()
        {

            return View(new DepartmentViewModel());
        }

        [HttpPost]
        public IActionResult Create(DepartmentViewModel model)
        {
            //Department department = new Department
            //{
            //    Name = model.Name,
            //    Code = model.Code,
            //    CreateAt = model.CreateAt

            //};

            var department=_mapper.Map<Department>(model);
            if (ModelState.IsValid) 
            {
                _unitOfWork._depRepository.Add(department);
                _unitOfWork.Complete();
                return RedirectToAction(nameof(Go));
            }
            return View(model);
            
          
        }

        public IActionResult Details(int? id) 
        {
            try 
            {
                var v = _unitOfWork._depRepository.GetEntityid(id);
                if (id != null && v != null)
                {

                    return View(v);
                }
             
                   
                return RedirectToAction("Error", "Home");

             


            }
            catch (Exception ex) 
            {
              _Logger.LogError(ex.Message);
                return RedirectToAction("Error","Home");
            }
        
        }

        public IActionResult Update(int? id) 
        {
            if (id is null)
                return NotFound();
            var v = _unitOfWork._depRepository.GetEntityid(id);
            if ( v is null)
                return NotFound();


            return View(v);

        }

        [HttpPost]
        public IActionResult Update(int id,Department department)
        {
         
            if (id != department.Id)
                return NotFound();


          
            try
            {
                if (ModelState.IsValid)
                {
                    _unitOfWork._depRepository.Update(department);
                    _unitOfWork.Complete();
                    return RedirectToAction(nameof(Go));
                }




            }
            catch (Exception ex) 
            {
                _Logger.LogError(ex.Message);
                return RedirectToAction("Error", "Home");
            }
            return View(department);
        }

        public IActionResult Delete(int? id) 
        {
            if (id is null)
                return NotFound();
            var v = _unitOfWork._depRepository.GetEntityid(id);
            if (v is null)
                return NotFound();

            _unitOfWork._depRepository.Delete(v);
            _unitOfWork.Complete();
            return RedirectToAction("Go");

        }
    }
}
