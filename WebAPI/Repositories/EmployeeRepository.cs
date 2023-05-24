using System;
using System.Reflection;
using WebAPI.Context;
using WebAPI.Contracts;
using WebAPI.Model;
using WebAPI.Utility;
using WebAPI.ViewModels.Educations;
using WebAPI.ViewModels.Employees;
using WebAPI.ViewModels.Universities;

namespace WebAPI.Repositories
{
    public class EmployeeRepository : GeneralRepository<Employee>, IEmployeeRepository
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEducationRepository _educationRepository;
        private readonly IUniversityRepository _universityRepository;

        public EmployeeRepository(BookingManagementDbContext context,
            IEducationRepository educationRepository,
            IUniversityRepository universityRepository) : base(context)
        {
            _educationRepository = educationRepository;
            _universityRepository = universityRepository;
        }

        public IEnumerable<MasterEmployeeVM> GetAllMasterEmployee()
        {
            var employees = GetAll();
            var educations = _educationRepository.GetAll();
            var universities = _universityRepository.GetAll();

            var employeeEducations = new List<MasterEmployeeVM>();

            foreach (var employee in employees)
            {
                var education = educations.FirstOrDefault(e => e.Guid == employee.Guid);
                var university = universities.FirstOrDefault(u => u.Guid == education.UniversityGuid);

                var employeeEducation = new MasterEmployeeVM
                {
                    Guid = employee.Guid,
                    NIK = employee.Nik,
                    FullName = employee.FirstName + " " + employee.LastName,
                    BirthDate = employee.BirthDate,
                    Gender = employee.Gender.ToString(),
                    HiringDate = employee.HiringDate,
                    Email = employee.Email,
                    PhoneNumber = employee.PhoneNumber,
                    Major = education.Major,
                    Degree = education.Degree,
                    GPA = education.Gpa,
                    UniversityName = university.Name
                };

                employeeEducations.Add(employeeEducation);
            }

            return employeeEducations;
        }


        MasterEmployeeVM? IEmployeeRepository.GetEmployeeById(Guid guid)
        {
            var employee = GetByGuid(guid);
            var education = _educationRepository.GetByGuid(guid);
            var university = _universityRepository.GetByGuid(education.UniversityGuid);

            var data = new MasterEmployeeVM
            {
                Guid = employee.Guid,
                NIK = employee.Nik,
                FullName = employee.FirstName + " " + employee.LastName,
                BirthDate = employee.BirthDate,
                Gender = employee.Gender.ToString(),
                HiringDate = employee.HiringDate,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                Major = education.Major,
                Degree = education.Degree,
                GPA = education.Gpa,
                UniversityName = university.Name
            };

            return data;         
        }

        public object GetEmployeeAll(Guid guid)
        {
            var employee = GetByGuid(guid);
            var education = _educationRepository.GetByGuid(guid);
            var university = _universityRepository.GetByGuid(education.UniversityGuid);

            var data = new
            {
                Guid = employee.Guid,
                NIK = employee.Nik,
                Fullname = employee.FirstName + " " + employee.LastName,
                BirthDate = employee.BirthDate,
                Gender = employee.Gender.ToString(),
                HiringDate = employee.HiringDate,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                Major = education.Major,
                Degree = education.Degree,
                Gpa = education.Gpa,
                University = university.Name
            };

            return data;
        }
    }
}

