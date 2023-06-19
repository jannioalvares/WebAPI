using Nest;
using System.ComponentModel.DataAnnotations;
using WebAPI.Utility;

namespace Client.ViewModel
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "FirstName is required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Birthdate is required")]
        public DateTime BirthDate { get; set; }
        [Required(ErrorMessage = "Gender is required")]
        public GenderLevel Gender { get; set; }
        [Required(ErrorMessage = "HiringDate is required")]
        [Display(Name = "Hiring Date")]
        public DateTime HiringDate { get; set; }
        [EmailAddress]

        [Display(Name = "Email")]
        public string Email { get; set; }
        [Phone]

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Major is required")]
        public string Major { get; set; }
        [Required(ErrorMessage = "Degree is required")]
        public string Degree { get; set; }

        [Range(0.0, 4.0, ErrorMessage = "GPA must between 0-4 ")]
        public float GPA { get; set; }
        [Required(ErrorMessage = "UniversityCode is required")]
        [Display(Name = "University Code")]
        public string UniversityCode { get; set; }
        [Required(ErrorMessage = "UniversityName is required")]
        [Display(Name = "University Name")]
        public string UniversityName { get; set; }
        [DataType(DataType.Password)]
        [PasswordValidation(ErrorMessage = "Password must contain uppercase, lowercase, number, symbol and minimum 6 character")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
