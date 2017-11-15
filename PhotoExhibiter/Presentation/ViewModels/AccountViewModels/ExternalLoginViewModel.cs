using System.ComponentModel.DataAnnotations;

namespace PhotoExhibiter.Presentation.ViewModels.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}