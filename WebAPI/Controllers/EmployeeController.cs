using Microsoft.AspNetCore.Mvc;
using System;
using WebAPI.Contracts;
using WebAPI.Model;
using WebAPI.Repositories;
using WebAPI.ViewModels.Bookings;
using WebAPI.ViewModels.Educations;
using WebAPI.ViewModels.Employees;
using WebAPI.ViewModels.Universities;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEducationRepository _educationRepository;
        private readonly IUniversityRepository _universityRepository;
        private readonly IMapper<Employee, EmployeeVM> _mapper;
        public EmployeeController(IEmployeeRepository employeeRepository, 
            IEducationRepository educationRepository, 
            IUniversityRepository universityRepository, 
            IMapper<Employee, EmployeeVM> mapper) 
        {
            _employeeRepository = employeeRepository;
            _educationRepository = educationRepository;
            _universityRepository = universityRepository;
            _mapper = mapper;
        }

        [HttpGet("GetAllMasterEmployee")]
        public IActionResult GetAll()
        {
            var masterEmployees = _employeeRepository.GetAllMasterEmployee();
            if (!masterEmployees.Any())
            {
                return NotFound();
            }

            return Ok(masterEmployees);
        }

        [HttpGet]
        public IActionResult GetEmployee ()
        {
            var employees = _employeeRepository.GetAll();
            if (!employees.Any())
            {
                return NotFound();
            }

            var data = employees.Select(_mapper.Map).ToList();
            return Ok(data);
        }

        [HttpGet("{guid}/GetByGuid")]
        public IActionResult GetByGuid(Guid guid)
        {
            var employee = _employeeRepository.GetByGuid(guid);
            if (employee is null)
            {
                return NotFound();
            }

            var data = _mapper.Map(employee);
            return Ok(data);
        }

        [HttpPost]
        public IActionResult Create(EmployeeVM employeeVM)
        {
            var employeeConverted = _mapper.Map(employeeVM);
            var result = _employeeRepository.Create(employeeConverted);
            if (result is null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpPut]
        public IActionResult Update(EmployeeVM employeeVM)
        {
            var employeeConverted = _mapper.Map(employeeVM);
            var isUpdated = _employeeRepository.Update(employeeConverted);
            if (!isUpdated)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var isDeleted = _employeeRepository.Delete(guid);
            if (!isDeleted)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
