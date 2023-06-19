using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using WebAPI.Contracts;
using WebAPI.Model;
using WebAPI.Others;
using WebAPI.Repositories;
using WebAPI.Utility;
using WebAPI.ViewModels.Bookings;
using WebAPI.ViewModels.Educations;
using WebAPI.ViewModels.Employees;
using WebAPI.ViewModels.Universities;

namespace WebAPI.Controllers
{
    [ApiController]
    /*[Authorize(Roles = $"{nameof(RoleLevel.Admin)}, {nameof(RoleLevel.Manager)}")]*/
    [Route("api/[controller]")]
    public class EmployeeController : BaseController<Employee, EmployeeVM>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper<Employee, EmployeeVM> _mapper;
        public EmployeeController(IEmployeeRepository employeeRepository, 
            IMapper<Employee, EmployeeVM> mapper) : base(employeeRepository, mapper)
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
                return NotFound(new ResponseMessageVM<string>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Employee Not Found"
                });
            }

            return Ok(new ResponseMessageVM<IEnumerable<MasterEmployeeVM>>
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
                return NotFound(new ResponseMessageVM<string>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Employee Not Found"
                });
            }

            return Ok(new ResponseMessageVM<MasterEmployeeVM>
            {
                Code = 200,
                Status = "OK",
                Message = "Employee Found",
                Data = masterEmployees
            });
        }
    }
}
