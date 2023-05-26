using Microsoft.AspNetCore.Mvc;
using WebAPI.Contracts;
using WebAPI.Model;
using WebAPI.Others;
using WebAPI.Repositories;
using WebAPI.ViewModels.Roles;
using WebAPI.ViewModels.Rooms;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {

        private readonly IRoleRepository _roleRepository;
        private readonly IMapper<Role, RoleVM> _mapper;
        public RoleController(IRoleRepository roleRepository, IMapper<Role, RoleVM> mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var response = new ResponseVM<RoleVM>();
            var roles = _roleRepository.GetAll();
            if (!roles.Any())
            {
                return NotFound(response.NotFound("Role Not Found"));
            }

            var data = roles.Select(_mapper.Map).ToList();
            return Ok(response.Success("Role Found"));
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var response = new ResponseVM<RoleVM>();
            var role = _roleRepository.GetByGuid(guid);
            if (role is null)
            {
                return NotFound(response.NotFound("Role Not Found"));
            }

            var data = _mapper.Map(role);
            return Ok(response.Success("Role Found"));
        }

        [HttpPost]
        public IActionResult Create(RoleVM roleVM)
        {
            var response = new ResponseVM<RoleVM>();
            var roleConverted = _mapper.Map(roleVM);
            var result = _roleRepository.Create(roleConverted);
            if (result is null)
            {
                return BadRequest(response.Failed("Role Create Failed"));
            }

            return Ok(response.Success("Role Create Success"));
        }

        [HttpPut]
        public IActionResult Update(RoleVM roleVM)
        {
            var response = new ResponseVM<RoleVM>();
            var roleConverted = _mapper.Map(roleVM); 
            var isUpdated = _roleRepository.Update(roleConverted);
            if (!isUpdated)
            {
                return BadRequest(response.Failed("Role Update Failed"));
            }

            return Ok(response.Success("Role Update Success"));
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var response = new ResponseVM<RoleVM>();
            var isDeleted = _roleRepository.Delete(guid);
            if (!isDeleted)
            {
                return BadRequest(response.Failed("Role Delete Failed"));
            }

            return Ok(response.Success("Role Delete Success"));
        }
    }
}
