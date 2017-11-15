using System.ComponentModel.DataAnnotations;

namespace PhotoExhibiter.Presentation.ViewModels.AccountViewModels
{
    public class LoginWithRecoveryCodeViewModel
    {
        [Required]
        [DataType (DataType.Text)]
        [Display (Name = "Recovery Code")]
        public string RecoveryCode { get; set; }
    }
}