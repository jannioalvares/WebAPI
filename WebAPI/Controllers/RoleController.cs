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
    public class RoleController : BaseController<Role, RoleVM>
    {

        private readonly IRoleRepository _roleRepository;
        private readonly IMapper<Role, RoleVM> _mapper;
        public RoleController(IRoleRepository roleRepository, 
            IMapper<Role, RoleVM> mapper) : base (roleRepository, mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }        
    }
}
