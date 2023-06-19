using Client.Models;
using Client.Repositories.Interface;
using Client.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository repository;

        public EmployeeController(IEmployeeRepository repository)
        {
            this.repository = repository;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var result = await repository.Get();
            var employees = new List<Employee>();

            if (result.Data != null)
            {
                employees = result.Data.Select(e => new Employee
                {
                    Guid = e.Guid,
                    Nik = e.Nik,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    BirthDate = e.BirthDate,
                    Gender = e.Gender,
                    HiringDate = e.HiringDate,
                    Email = e.Email,
                    PhoneNumber = e.PhoneNumber,
                }).ToList();
            }

            return View(employees);
        }

        [Authorize]
        public async Task<IActionResult> MasterEmployee()
        {
            var result = await repository.GetMasterEmployee();
            var employees = new List<MasterEmployeeVM>();

            if (result.Data != null)
            {
                employees = result.Data.Select(e => new MasterEmployeeVM
                {
                    Guid = e.Guid,
                    NIK = e.NIK,
                    FullName = e.FullName,
                    BirthDate = e.BirthDate,
                    Gender = e.Gender,
                    HiringDate = e.HiringDate,
                    Email = e.Email,
                    PhoneNumber = e.PhoneNumber,
                    Major = e.Major,
                    Degree = e.Degree,
                    GPA = e. GPA,
                    UniversityName = e.UniversityName
                }).ToList();
            }

            return View(employees);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Creates()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Creates(Employee employee)
        {
            var result = await repository.Post(employee);
            if (result.Code == 409)
            {
                return RedirectToAction(nameof(Index));
            }
            else if (result.Code == 409)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View();
            }

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Manager, Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(Employee employee)
        {
            var result = await repository.Put(employee);
            if (result.Code == 409)
            {
                return RedirectToAction(nameof(Index));
            }
            else if (result.Code == 409)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View();
            }

            return RedirectToAction(nameof(Index));

        }

        [Authorize(Roles = "Manager, Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(Guid Guid)
        {
            var result = await repository.Get(Guid);
            var employee = new Employee();
            if (result.Data?.Guid is null)
            {
                return View(employee);
            }
            else
            {
                employee.Guid = result.Data.Guid;
                employee.Nik = result.Data.Nik;
                employee.FirstName = result.Data.FirstName;
                employee.LastName = result.Data.LastName;
                employee.BirthDate = result.Data.BirthDate;
                employee.Gender = result.Data.Gender;
                employee.HiringDate = result.Data.HiringDate;
                employee.Email = result.Data.Email;
                employee.PhoneNumber = result.Data.PhoneNumber;
                
            }

            return View(employee);
        }

        [Authorize(Roles = "Manager, Admin")]
        public async Task<IActionResult> Deletes(Guid Guid)
        {
            var result = await repository.Get(Guid);
            var employee = new Employee();
            if (result.Data?.Guid is null)
            {
                return View(employee);
            }
            else
            {
                employee.Guid = result.Data.Guid;
                employee.Nik = result.Data.Nik;
                employee.FirstName = result.Data.FirstName;
                employee.LastName = result.Data.LastName;
                employee.BirthDate = result.Data.BirthDate;
                employee.Gender = result.Data.Gender;
                employee.HiringDate = result.Data.HiringDate;
                employee.Email = result.Data.Email;
                employee.PhoneNumber = result.Data.PhoneNumber;
            }
            return View(employee);
        }

        [Authorize(Roles = "Manager, Admin")]
        [HttpPost]
        public async Task<IActionResult> Remove(Guid Guid)
        {
            var result = await repository.Deletes(Guid);
            if (result.Code == 409)
            {
                return RedirectToAction("Notfound", "Home");
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
