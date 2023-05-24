using Microsoft.AspNetCore.Mvc;
using WebAPI.Contracts;
using WebAPI.Model;
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
        private readonly IEducationRepository _educationRepository;
        private readonly IMapper<University, UniversityVM> _mapper;
        private readonly IMapper<Education, EducationVM> _educationMapper;

        public UniversityController(IUniversityRepository universityRepository, IEducationRepository educationRepository,
            IMapper<University, UniversityVM> mapper, IMapper<Education, EducationVM> educationMapper)
        {
            _universityRepository = universityRepository;
            _educationRepository = educationRepository;
            _mapper = mapper;
            _educationMapper = educationMapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var universities = _universityRepository.GetAll();
            if (!universities.Any())
            {
                return NotFound();
            }

            var data = universities.Select(_mapper.Map).ToList();

            return Ok(data);
        }

        [HttpGet("WithEducation")]
        public IActionResult GetAllWithEducation()
        {
            var universities = _universityRepository.GetAll();
            if (!universities.Any())
            {
                return NotFound();
            }

            var results = new List<UniversityEducationVM>();
            foreach (var university in universities)
            {
                var education = _educationRepository.GetByUniversityId(university.Guid);
                //var educationMapped = education.Select(_mapper.Map).ToList();

                var result = new UniversityEducationVM
                {
                    Guid = university.Guid,
                    Code = university.Code,
                    Name = university.Name,
                    //Educations = education.Select(_mapper.Map).ToList();
                };

                results.Add(result);
            }

            return Ok(results);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var university = _universityRepository.GetByGuid(guid);
            if (university is null)
            {
                return NotFound();
            }

            var data = _mapper.Map(university);

            return Ok(university);
        }

        [HttpPost]
        public IActionResult Create(UniversityVM universityVM)
        {
            var universityConverted = _mapper.Map(universityVM);

            var result = _universityRepository.Create(universityConverted);
            if (result is null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpPut]
        public IActionResult Update(UniversityVM universityVM)
        {
            var universityConverted = _mapper.Map(universityVM);

            var isUpdated = _universityRepository.Update(universityConverted);
            if (!isUpdated)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var isDeleted = _universityRepository.Delete(guid);
            if (!isDeleted)
            {
                return BadRequest();
            }

            return Ok();
        }

    }
}
