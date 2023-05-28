using Microsoft.AspNetCore.Mvc;
using WebAPI.Contracts;
using WebAPI.Model;
using WebAPI.Others;
using WebAPI.Repositories;
using WebAPI.ViewModels.Educations;
using WebAPI.ViewModels.Universities;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UniversityController : BaseController<University, UniversityVM>
    {
        private readonly IUniversityRepository _universityRepository;
        private readonly IMapper<University, UniversityVM> _mapper;

        public UniversityController(IUniversityRepository universityRepository,
            IMapper<University, UniversityVM> mapper) : base(universityRepository, mapper)
        {
            _universityRepository = universityRepository;
            _mapper = mapper;
        }

        [HttpGet("WithEducation")]
        public IActionResult GetAllWithEducation()
        {
            var response = new ResponseVM<IEnumerable<UniversityEducationVM>>();
            var univEdu = _universityRepository.GetUniversityEducation();
            if (!univEdu.Any())
            {
                return NotFound(response.NotFound("UniversityEducation Not Found"));
            }

            return Ok(response.Success(univEdu,"UniversityEducation Found"));
        }
    }
}
