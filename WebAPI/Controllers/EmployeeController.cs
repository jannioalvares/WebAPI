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
                return NotFound(new ResponseVM<string>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Employee Not Found"
                });
            }

            var data = employees.Select(_mapper.Map).ToList();
            return Ok(new ResponseVM<List<EmployeeVM>>
            {
                Code = 200,
                Status = "OK",
                Message = "Employee Found",
                Data = data
            });
        }

        [HttpGet("{guid}/GetByGuid")]
        public IActionResult GetByGuid(Guid guid)
        {
            var response = new ResponseVM<EmployeeVM>();
            var employee = _employeeRepository.GetByGuid(guid);
            if (employee is null)
            {
                return NotFound(response.Failed("Employee Not Found"));
            }

            var data = _mapper.Map(employee);
            return Ok(response.Success(data, "Employee Found"));
        }

        [HttpPost]
        public IActionResult Create(EmployeeVM employeeVM)
        {
            var response = new ResponseVM<EmployeeVM>();
            var employeeConverted = _mapper.Map(employeeVM);
            var result = _employeeRepository.Create(employeeConverted);
            if (result is null)
            {
                return BadRequest(response.Failed("Employee Create Failed"));
            }

            return Ok(response.Success("Employee Create Success"));
        }

        [HttpPut]
        public IActionResult Update(EmployeeVM employeeVM)
        {
            var response = new ResponseVM<EmployeeVM>();
            var employeeConverted = _mapper.Map(employeeVM);
            var isUpdated = _employeeRepository.Update(employeeConverted);
            if (!isUpdated)
            {
                return BadRequest(response.Failed("Employee Update Failed"));
            }

            return Ok(response.Success("Employee Update Success"));
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var response = new ResponseVM<EmployeeVM>();
            var isDeleted = _employeeRepository.Delete(guid);
            if (!isDeleted)
            {
                return BadRequest(response.Failed("Employee Delete Failed"));
            }

            return Ok(response.Success("Employee Delete Success"));
        }
    }
}
