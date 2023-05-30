using Microsoft.AspNetCore.Http;
using WebAPI.Context;
using WebAPI.Contracts;
using WebAPI.Model;
using WebAPI.ViewModels.Employees;
using WebAPI.ViewModels.Universities;

namespace WebAPI.Repositories
{
    public class UniversityRepository : GeneralRepository<University>, IUniversityRepository
    {
        public UniversityRepository(BookingManagementDbContext context) : base(context) { }

        public University CreateWithValidate(University university)
        {
            try
            {
                var existingUniversityWithCode = _context.Universities.FirstOrDefault(u => u.Code == university.Code);
                var existingUniversityWithName = _context.Universities.FirstOrDefault(u => u.Name == university.Name);

                if (existingUniversityWithCode != null & existingUniversityWithName != null)
                {
                    university.Guid = existingUniversityWithCode.Guid;

                    _context.SaveChanges();
                }
                else
                {
                    _context.Universities.Add(university);
                    _context.SaveChanges();
                }

                return university;

            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<UniversityEducationVM> GetUniversityEducation()
        {
            var universityEducations = new List<UniversityEducationVM>();
            var universities = GetAll();
            foreach (var university in universities)
            {
                var education = _context.Educations.FirstOrDefault(e => e.UniversityGuid == university.Guid);

                if (education != null)
                {
                    var result = new UniversityEducationVM
                    {
                        Guid = university.Guid,
                        Code = university.Code,
                        Name = university.Name,
                        Major = education.Major,
                        Degree = education.Degree,
                        GPA = education.Gpa
                    };
                    universityEducations.Add(result);
                }
            }
            return universityEducations;
        }
    }
}
