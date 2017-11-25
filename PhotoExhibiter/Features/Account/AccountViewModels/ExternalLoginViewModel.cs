using System.ComponentModel.DataAnnotations;

namespace PhotoExhibiter.Features.Account.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
