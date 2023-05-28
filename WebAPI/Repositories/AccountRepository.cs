﻿using WebAPI.Context;
using WebAPI.Contracts;
using WebAPI.Model;
using WebAPI.ViewModels.Accounts;

namespace WebAPI.Repositories
{
    public class AccountRepository : GeneralRepository<Account>, IAccountRepository
    {
        private readonly IUniversityRepository _universityRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEducationRepository _educationRepository;
        public AccountRepository(BookingManagementDbContext context,
            IUniversityRepository universityRepository,
            IEmployeeRepository employeeRepository,
            IEducationRepository educationRepository) : base(context)
        {
            _universityRepository = universityRepository;
            _employeeRepository = employeeRepository;
            _educationRepository = educationRepository;
        }

        public int ChangePasswordAccount(Guid? employeeId, ChangePasswordVM changePasswordVM)
        {
            var account = new Account();
            account = _context.Set<Account>().FirstOrDefault(a => a.Guid == employeeId);
            if (account == null || account.Otp != changePasswordVM.OTP)
            {
                return 2;
            }
            // Cek apakah OTP sudah digunakan
            if (account.IsUsed)
            {
                return 3;
            }
            // Cek apakah OTP sudah expired
            if (account.ExpiredTime < DateTime.Now)
            {
                return 4;
            }
            // Cek apakah NewPassword dan ConfirmPassword sesuai
            if (changePasswordVM.NewPassword != changePasswordVM.ConfirmPassword)
            {
                return 5;
            }
            // Update password
            account.Password = changePasswordVM.NewPassword;
            account.IsUsed = true;
            try
            {
                var updatePassword = Update(account);
                if (!updatePassword)
                {
                    return 0;
                }
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public LoginVM Login(LoginVM loginVM)
        {
            var account = GetAll();
            var employee = _context.Employees.ToList();

            var query = from emp in employee
                        join acc in account
                        on emp.Guid equals acc.Guid
                        where emp.Email == loginVM.Email
                        select new LoginVM
                        {
                            Email = emp.Email,
                            Password = acc.Password
                        };
            var validate = query.FirstOrDefault();

            if (validate != null && Hashing.ValidatePassword(loginVM.Password, validate.Password))
            {
                loginVM.Password = validate.Password;
            }

            return validate;
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
                    Nik = GenerateNIK(),
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
                    Gpa = registerVM.GPA,
                    UniversityGuid = university.Guid
                };
                _educationRepository.Create(education);

                var account = new Account
                {
                    Guid = employee.Guid,
                    Password = Hashing.HashPassword(registerVM.Password),
                    IsDeleted = false,
                    IsUsed = true,
                    Otp = 0
                };

                Create(account);

                return 3;

            }
            catch
            {
                return 0;
            }
        }

        public int UpdateOTP(Guid? employeeId)
        {
            var account = new Account();
            account = _context.Set<Account>().FirstOrDefault(a => a.Guid == employeeId);
            //Generate OTP
            Random rnd = new Random();
            var getOtp = rnd.Next(100000, 999999);
            account.Otp = getOtp;

            //Add 5 minutes to expired time
            account.ExpiredTime = DateTime.Now.AddMinutes(5);
            account.IsUsed = false;
            try
            {
                var check = Update(account);


                if (!check)
                {
                    return 0;
                }
                return getOtp;
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
