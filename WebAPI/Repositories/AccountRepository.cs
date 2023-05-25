using WebAPI.Context;
using WebAPI.Contracts;
using WebAPI.Model;
using WebAPI.ViewModels.Accounts;
using WebAPI.ViewModels.Login;

namespace WebAPI.Repositories
{
    public class AccountRepository : GeneralRepository<Account>, IAccountRepository
    {
        public AccountRepository(BookingManagementDbContext context) : base(context)
        {
        }

        public AccountEmpVM Login(LoginVM loginVM)
        {
            var account = GetAll();
            var employee = _context.Employees.ToList();
            var query = from emp in employee
                        join acc in account
                        on emp.Guid equals acc.Guid
                        where emp.Email == loginVM.Email
                        select new AccountEmpVM
                        {
                            Email = emp.Email,
                            Password = acc.Password

                        };
            return query.FirstOrDefault();
        }

        public int Register(RegisterVM registerVM)
        {
            try
            {
                var university = new University
                {
                    Code = registerVM.Code,
                    Name = registerVM.Name

                };
                _universityRepository.CreateWithValidate(university);

                var employee = new Employee
                {
                    NIK = GenerateNIK(),
                    FirstName = registerVM.FirstName,
                    LastName = registerVM.LastName,
                    BirthDate = registerVM.BirthDate,
                    Gender = registerVM.Gender,
                    HiringDate = registerVM.HiringDate,
                    Email = registerVM.Email,
                    PhoneNumber = registerVM.PhoneNumber,
                };
                var result = _employeeRepository.CreateWithValidate(employee);

                if (result != 3)
                {
                    return result;
                }

                var education = new Education
                {
                    Guid = employee.Guid,
                    Major = registerVM.Major,
                    Degree = registerVM.Degree,
                    GPA = registerVM.GPA,
                    UniversityGuid = university.Guid
                };
                _educationRepository.Create(education);

                var account = new Account
                {
                    Guid = employee.Guid,
                    Password = registerVM.Password,
                    IsDeleted = false,
                    IsUsed = true,
                    OTP = 0
                };

                Create(account);

                return 3;

            }
            catch
            {
                return 0;
            }
        }

        private string GenerateNIK()
        {
            var lastNik = _context.Employees.ToList().OrderByDescending(e => int.Parse(e.Nik)).FirstOrDefault();

            if (lastNik != null)
            {
                int lastNikNumber;
                if (int.TryParse(lastNik.Nik, out lastNikNumber))
                {
                    return (lastNikNumber + 1).ToString();
                }
            }

            return "100000";
        }
    }
}
