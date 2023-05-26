using WebAPI.Model;
using WebAPI.ViewModels.Educations;

namespace WebAPI.ViewModels.Universities
{
    public class UniversityEducationVM
    {
        public Guid? Guid { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Major { get; set; }
        public string Degree { get; set; }
        public float GPA { get; set; }
    }
}
