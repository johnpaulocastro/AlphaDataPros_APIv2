using System.ComponentModel.DataAnnotations;

namespace ADPv2.Models.ViewModels
{
    public class BaseTransactionHistoryCCardRequestDto
    {
        [Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last Name is required.")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone No. is required.")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Street No. is required.")]
        public string StreetNumber { get; set; } = string.Empty;

        public string UnitNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Street Name is required.")]
        public string StreetName { get; set; } = string.Empty;

        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "State is required.")]
        public string State { get; set; } = string.Empty;

        [Required(ErrorMessage = "Postal Code is required.")]
        public string ZipCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string EmailAddress { get; set; } = string.Empty;

        [Required]
        [RegularExpression("^\\d+(\\.\\d+)?$", ErrorMessage = "Amount should be numeric only.")]
        [Range(1, Double.MaxValue, ErrorMessage = "Amount should be greater than 0")]
        public string Amount { get; set; }

        public string Notes { get; set; } = string.Empty;
    }

    public class TransactionHistoryRequestDto: BaseTransactionHistoryCCardRequestDto
    {
        public string BankName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Routing Number is required.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Routing Number should be numeric only.")]
        [StringLength(12, MinimumLength = 9, ErrorMessage = "Routing Number should be minimum of 9 digits.")]
        public string RoutingNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Account Number is required.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Account Number should be numeric only.")]
        [StringLength(17, MinimumLength = 8, ErrorMessage = "Account Number should be minimum of 8 digits.")]
        public string AccountNumber { get; set; } = string.Empty;

        [RegularExpression("^[0-9]*$", ErrorMessage = "Check Number should be numeric only.")]
        [StringLength(6, MinimumLength = 4, ErrorMessage = "Check Number should be minimum of 4 digits.")]
        public string CheckNo { get; set; } = string.Empty;
    }

    public class TransactionHistoryCCardRequestDto : BaseTransactionHistoryCCardRequestDto
    {
        public string BankName { get; set; } = string.Empty;
        public string RoutingNumber { get; set; } = string.Empty;
        public string AccountNumber { get; set; } = string.Empty;
        public string CheckNo { get; set; } = string.Empty;
    }
}
