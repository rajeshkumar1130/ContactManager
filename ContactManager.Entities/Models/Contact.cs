using System.ComponentModel.DataAnnotations;

namespace ContactManager.Entities.Models
{
    public class Contact
    {
        [Required]
        public int ContactId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(12, MinimumLength = 0)]
        [Required(AllowEmptyStrings = true, ErrorMessage = "Phone number is invalid.")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Phone number is invalid.")]
        [RegularExpression("^\\D?(\\d{3})\\D?\\D?(\\d{3})\\D?(\\d{4})$", ErrorMessage = "Phone number is invalid.")]
        public string PhoneNumber { get; set; }

        [Required]
        [RegularExpression("Active|Inactive", ErrorMessage = "Status must be either Active or Inactive")]
        public string Status { get; set; }
    }
}
