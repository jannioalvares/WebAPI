using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Utility;

namespace WebAPI.ViewModels.Employees
{
    public class EmployeeVM
    {
        public Guid? Guid { get; set; }
        [NIKEmailPhoneValidation(nameof(Nik))]
        public string Nik { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public GenderLevel Gender { get; set; }
        public DateTime HiringDate { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
