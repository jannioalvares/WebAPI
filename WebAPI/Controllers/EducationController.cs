using Microsoft.AspNetCore.Mvc;
using WebAPI.Contracts;
using WebAPI.Model;
using WebAPI.Utility;
using WebAPI.ViewModels.Educations;
using WebAPI.ViewModels.Universities;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EducationController : ControllerBase
    {

        private readonly IEducationRepository _educationRepository;
        private readonly IMapper<Education, EducationVM> _mapper;
        public EducationController(IEducationRepository educationRepository, IMapper<Education, EducationVM> mapper)
        {
            _educationRepository = educationRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var educations = _educationRepository.GetAll();
            if (!educations.Any())
            {
                return NotFound();
            }

            var data = educations.Select(_mapper.Map).ToList();
            return Ok(data);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var education = _educationRepository.GetByGuid(guid);
            if (education is null)
            {
                return NotFound();
            }

            var data = _mapper.Map(education);
            return Ok(data);
        }

        [HttpPost]
        public IActionResult Create(EducationVM educationVM)
        {
            var educationConverted = _mapper.Map(educationVM);
            var result = _educationRepository.Create(educationConverted);
            if (result is null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpPut]
        public IActionResult Update(EducationVM educationVM)
        {
            var educationConverted = _mapper.Map(educationVM);
            var isUpdated = _educationRepository.Update(educationConverted);
            if (!isUpdated)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var isDeleted = _educationRepository.Delete(guid);
            if (!isDeleted)
            {
                return BadRequest();
            }

            return Ok();
        }

    }
}
