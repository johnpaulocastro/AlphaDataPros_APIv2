using System.ComponentModel.DataAnnotations;

namespace ADPv2.Models.ViewModels.ApiViewModels
{
    public class ApiAccountCredentialsRequestDto : AccountCredentialsRequestDto
    {
        [Required(ErrorMessage = "Secure Key is required.")]
        public string SecureKey { get; set; } = string.Empty;
    }
}
