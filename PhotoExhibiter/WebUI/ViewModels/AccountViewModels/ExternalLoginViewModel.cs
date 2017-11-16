using System.ComponentModel.DataAnnotations;

namespace PhotoExhibiter.WebUI.ViewModels.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}