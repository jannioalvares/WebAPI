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
        public EmployeeRepository(BookingManagementDbContext context) : base(context){}

        public int CreateWithValidate(Employee employee)
        {
            try
            {
                bool ExistsByEmail = _context.Employees.Any(e => e.Email == employee.Email);
                if (ExistsByEmail)
                {
                    return 1;
                }

                bool ExistsByPhoneNumber = _context.Employees.Any(e => e.PhoneNumber == employee.PhoneNumber);
                if (ExistsByPhoneNumber)
                {
                    return 2;
                }

                Create(employee);
                return 3;

            }
            catch
            {
                return 0;
            }
        }

        public Guid? FindGuidByEmail(string email)
        {
            try
            {
                var employee = _context.Employees.FirstOrDefault(e => e.Email == email);
                if (employee == null)
                {
                    return null;
                }
                return employee.Guid;
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<MasterEmployeeVM> GetAllMasterEmployee()
        {
            var employees = GetAll();
            var educations = _context.Educations.ToList();
            var universities = _context.Universities.ToList();

            var employeeEducations = new List<MasterEmployeeVM>();

            foreach (var employee in employees)
            {
                var education = educations.FirstOrDefault(e => e.Guid == employee?.Guid);
                var university = universities.FirstOrDefault(u => u.Guid == education?.UniversityGuid);

                if (education != null && university != null)
                {
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
            }

            return employeeEducations;
        }

        MasterEmployeeVM? IEmployeeRepository.GetMasterEmployeeByGuid(Guid guid)
        {
            var employee = GetByGuid(guid);
            var education = _context.Educations.FirstOrDefault(e => e.Guid == guid);
            var university = _context.Universities.FirstOrDefault(u => u.Guid == education.UniversityGuid);

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
    }
}

