using System.ComponentModel.DataAnnotations;
using WebAPI.Contracts;

namespace WebAPI.Utility
{
    public class NIKEmailPhoneValidation : ValidationAttribute
    {
        private readonly string _propertyName;

        public NIKEmailPhoneValidation(string propertyName)
        {
            _propertyName = propertyName;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return new ValidationResult($"{_propertyName} is required.");
            var employeeRepository = validationContext.GetService(typeof(IEmployeeRepository))
                                        as IEmployeeRepository;

            bool checkEmailAndPhone = employeeRepository.CheckNIKEmailPhone(value.ToString());
            if (checkEmailAndPhone) return new ValidationResult($"{_propertyName} '{value}' already exists.");
            return ValidationResult.Success;
        }

    }
}
