using System.ComponentModel.DataAnnotations;
using WebAPI.Contracts;

namespace WebAPI.Utility
{
    public class RoomNameValidation : ValidationAttribute
    {
        private readonly string _propertyName;

        public RoomNameValidation(string propertyName)
        {
            _propertyName = propertyName;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return new ValidationResult($"{_propertyName} is required.");
            var roomRepository = validationContext.GetService(typeof(IRoomRepository))
                                        as IRoomRepository;

            bool checkEmailAndPhone = roomRepository.CheckRoomName(value.ToString());
            if (checkEmailAndPhone) return new ValidationResult($"{_propertyName} '{value}' already exists.");
            return ValidationResult.Success;
        }
    }
}
