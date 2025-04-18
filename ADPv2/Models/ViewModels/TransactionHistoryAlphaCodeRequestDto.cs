using System.ComponentModel.DataAnnotations;

namespace ADPv2.Models.ViewModels
{
    public class TransactionHistoryAlphaCodeRequestDto
    {
        [Required(ErrorMessage = "AlphaCode is required.")]
        public string AlphaCode { get; set; } = string.Empty;

        [Required]
        [RegularExpression("^\\d+(\\.\\d+)?$", ErrorMessage = "Amount should be numeric only.")]
        public decimal Amount { get; set; }
    }
}
