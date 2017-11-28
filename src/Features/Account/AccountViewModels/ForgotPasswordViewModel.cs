using System.ComponentModel.DataAnnotations;

namespace PhotoExhibiter.Features.Account.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
