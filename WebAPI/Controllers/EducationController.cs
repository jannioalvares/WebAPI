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
    public class EducationController : BaseController<Education, EducationVM>
    {

        private readonly IEducationRepository _educationRepository;
        private readonly IMapper<Education, EducationVM> _mapper;
        public EducationController(IEducationRepository educationRepository, 
            IMapper<Education, EducationVM> mapper) : base(educationRepository, mapper)
        {
            _educationRepository = educationRepository;
            _mapper = mapper;
        }
    }
}
