using Azure;
using Microsoft.AspNetCore.Mvc;
using Nest;
using WebAPI.Contracts;
using WebAPI.Model;
using WebAPI.Others;
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
            var response = new ResponseVM<IEnumerable<EducationVM>>();
            var educations = _educationRepository.GetAll();
            if (!educations.Any())
            {
                return NotFound(response.NotFound("Education Not Found"));
            }

            var data = educations.Select(_mapper.Map).ToList();
            return Ok(response.Success(data, "Education Found"));
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var response = new ResponseVM<EducationVM>();
            var education = _educationRepository.GetByGuid(guid);
            if (education is null)
            {
                return NotFound(response.NotFound("Education Not Found"));
            }

            var data = _mapper.Map(education);
            return Ok(response.Success(data, "Education Found"));
        }

        [HttpPost]
        public IActionResult Create(EducationVM educationVM)
        {
            var response = new ResponseVM<EducationVM>();
            var educationConverted = _mapper.Map(educationVM);
            var result = _educationRepository.Create(educationConverted);
            if (result is null)
            {
                return BadRequest(response.Failed("Education Create Failed"));
            }

            return Ok(response.Success("Education Create Success"));
        }

        [HttpPut]
        public IActionResult Update(EducationVM educationVM)
        {
            var response = new ResponseVM<EducationVM>();
            var educationConverted = _mapper.Map(educationVM);
            var isUpdated = _educationRepository.Update(educationConverted);
            if (!isUpdated)
            {
                return BadRequest(response.Failed("Education Update Failed"));
            }

            return Ok(response.Success("Education Update Success"));
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var response = new ResponseVM<EducationVM>();
            var isDeleted = _educationRepository.Delete(guid);
            if (!isDeleted)
            {
                return BadRequest(response.Failed("Education Update Failed"));
            }
            return Ok(response.Success("Education Update Success"));
        }

    }
}
