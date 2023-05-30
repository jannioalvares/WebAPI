using System.ComponentModel.DataAnnotations;
using WebAPI.Utility;

namespace WebAPI.ViewModels.Accounts
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "FirstName is required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required(ErrorMessage = "Birthdate is required")]
        public DateTime BirthDate { get; set; }
        [Required(ErrorMessage = "Gender is required")]
        public GenderLevel Gender { get; set; }
        [Required(ErrorMessage = "HiringDate is required")]
        public DateTime HiringDate { get; set; }
        [EmailAddress]
        [NIKEmailPhoneValidation(nameof(Email))]
        public string Email { get; set; }
        [Phone]
        [NIKEmailPhoneValidation(nameof(PhoneNumber))]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Major is required")]
        public string Major { get; set; }
        [Required(ErrorMessage = "Degree is required")]
        public string Degree { get; set; }

        [Range(0,4, ErrorMessage = "GPA must between 0-4 ")]
        public float GPA { get; set; }
        [Required(ErrorMessage = "UniversityCode is required")]
        public string UniversityCode { get; set; }
        [Required(ErrorMessage = "UniversityName is required")]
        public string UniversityName { get; set; }
        [PasswordValidation(ErrorMessage = "Password must contain uppercase, lowercase, number, symbol and minimum 6 character")]
        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
