using System.ComponentModel.DataAnnotations;

namespace PhotoExhibiter.Infra.Identity.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
