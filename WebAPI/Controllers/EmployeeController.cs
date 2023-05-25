using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using WebAPI.Contracts;
using WebAPI.Model;
using WebAPI.Others;
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
        private readonly IMapper<Employee, EmployeeVM> _mapper;
        public EmployeeController(IEmployeeRepository employeeRepository, IMapper<Employee, EmployeeVM> mapper) 
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        [HttpGet("GetAllMasterEmployee")]
        public IActionResult GetAll()
        {
            var masterEmployees = _employeeRepository.GetAllMasterEmployee();
            if (!masterEmployees.Any())
            {
                return NotFound(new ResponseVM<string>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Employee Not Found"
                });
            }

            return Ok(new ResponseVM<IEnumerable<MasterEmployeeVM>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Employee Found",
                Data = masterEmployees
            });
        }

        [HttpGet("GetMasterEmployeeByGuid")]
        public IActionResult GetMasterEmployeeByGuid(Guid guid)
        {
            var masterEmployees = _employeeRepository.GetMasterEmployeeByGuid(guid);
            if (masterEmployees is null)
            {
                return NotFound(new ResponseVM<string>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Employee Not Found"
                });
            }

            return Ok(new ResponseVM<MasterEmployeeVM>
            {
                Code = 200,
                Status = "OK",
                Message = "Employee Found",
                Data = masterEmployees
            });
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
