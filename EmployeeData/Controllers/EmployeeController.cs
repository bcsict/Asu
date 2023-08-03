using EmployeeData.DAL;
using EmployeeData.Models;
using EmployeeData.Models.DBEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EmployeeData.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeDbContext _context;

        public object Id { get; private set; }

        public EmployeeController(EmployeeDbContext context)
        {
            this._context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var employees = _context.Employees.ToList();
            List<EmployeeViewModel> employeeList = new();

            if (employees != null)
            {
                
                    foreach(var employee in employees)
                    {
                    var EmployeeViewModel = new EmployeeViewModel()
                    {
                        Id = employee.ID,
                        FirstName = employee.FirstName,
                        LastName = employee.LastName,
                        DateOfBirth = employee.DateOfBirth,
                        Email = employee.Email,
                        Salary  = employee.Salary,
                    };
                    employeeList.Add(EmployeeViewModel);
                    }
                    return View(employeeList);
            }

            return View(employeeList);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(EmployeeViewModel employeeData)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var employee = new Employee()
                    {
                        FirstName = employeeData.FirstName,
                        LastName = employeeData.LastName,
                        DateOfBirth = employeeData.DateOfBirth,
                        Email = employeeData.Email,
                        Salary = employeeData.Salary,
                    };
                    _context.Employees.Add(employee);
                    _context.SaveChanges();
                    TempData["SuccessMessage"] = "Employee Created Seccessfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["errorMessage"] = "Model data is not valid";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return View();
            }
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            try
            {
                var employee = _context.Employees.SingleOrDefault(x => x.ID == Id);
                if (employee != null)
                {
                    var employeeView = new EmployeeViewModel() 
                    {
                        Id = employee.ID,
                        FirstName = employee.FirstName,
                        LastName = employee.LastName,
                        DateOfBirth = employee.DateOfBirth,
                        Email = employee.Email,
                        Salary = employee.Salary,
                    };
                    return View(employeeView);
                }
                else
                {
                    TempData["errorMessage"] = $"Employee details not available with the Id: {Id}";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Edit(EmployeeViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var employee = new Employee()
                    {
                        ID = model.Id,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        DateOfBirth = model.DateOfBirth,
                        Email = model.Email,
                        Salary = model.Salary,
                    };
                    _context.Employees.Update(employee);
                    _context.SaveChanges();
                    TempData["successMessage"] = "Employee details updated successfully!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["errorMessage"] = "Model Data is invalid.";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return View();
            }
        }

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            try
            {
                var employee = _context.Employees.SingleOrDefault(x => x.ID == Id);
                if (employee != null)
                {
                    var employeeView = new EmployeeViewModel()
                    {
                        Id = employee.ID,
                        FirstName = employee.FirstName,
                        LastName = employee.LastName,
                        DateOfBirth = employee.DateOfBirth,
                        Email = employee.Email,
                        Salary = employee.Salary,
                    };
                    return View(employeeView);
                }
                else
                {
                    TempData["errorMessage"] = $"Employee details not available with the Id: {Id}";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Delete(EmployeeViewModel model)
        {
            var employee = _context.Employees.SingleOrDefault(x => x.ID == model.Id);
            try
            {

                if (employee != null)
                {
                    _context.Employees.Remove(employee);
                    _context.SaveChanges();
                    TempData["successMessage"] = "EMployee deleted";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["errorMessage"] = $"Employee details not available with the Id: {Id}";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return View();
            }
        }
    }
}
