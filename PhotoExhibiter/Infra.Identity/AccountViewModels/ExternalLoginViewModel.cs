using System.ComponentModel.DataAnnotations;

namespace PhotoExhibiter.Infra.Identity.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}