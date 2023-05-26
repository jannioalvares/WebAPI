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
    public class UniversityController : ControllerBase
    {
        private readonly IUniversityRepository _universityRepository;
        private readonly IMapper<University, UniversityVM> _mapper;

        public UniversityController(IUniversityRepository universityRepository, IMapper<University, UniversityVM> mapper)
        {
            _universityRepository = universityRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var response = new ResponseVM<IEnumerable<UniversityVM>>();
            var universities = _universityRepository.GetAll();
            if (!universities.Any())
            {
                return NotFound(response.NotFound("University Not Found"));
            }

            var data = universities.Select(_mapper.Map).ToList();

            return Ok(response.Success(data, "University Found"));
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

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var response = new ResponseVM<UniversityVM>();
            var university = _universityRepository.GetByGuid(guid);
            if (university is null)
            {
                return NotFound(response.NotFound("University Not Found"));
            }
            var data = _mapper.Map(university);
            return Ok(response.Success(data, "University Found"));
        }

        [HttpPost]
        public IActionResult Create(UniversityVM universityVM)
        {
            var response = new ResponseVM<UniversityVM>();
            var universityConverted = _mapper.Map(universityVM);
            var result = _universityRepository.Create(universityConverted);
            if (result is null)
            {
                return BadRequest(response.Failed("University Create Failed"));
            }

            return Ok(response.Success("University Create Success"));
        }

        [HttpPut]
        public IActionResult Update(UniversityVM universityVM)
        {
            var response = new ResponseVM<UniversityVM>();
            var universityConverted = _mapper.Map(universityVM);
            var isUpdated = _universityRepository.Update(universityConverted);
            if (!isUpdated)
            {
                return BadRequest(response.Failed("University Update Failed"));
            }

            return Ok(response.Success("University Update Success"));
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var response = new ResponseVM<UniversityVM>();
            var isDeleted = _universityRepository.Delete(guid);
            if (!isDeleted)
            {
                return BadRequest(response.Failed("University Delete Failed"));
            }

            return Ok(response.Success("University Delete Success"));
        }

    }
}
