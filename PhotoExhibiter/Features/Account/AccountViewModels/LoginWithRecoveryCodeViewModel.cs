using System.ComponentModel.DataAnnotations;

namespace PhotoExhibiter.Infra.Identity.AccountViewModels
{
    public class LoginWithRecoveryCodeViewModel
    {
        [Required]
        [DataType (DataType.Text)]
        [Display (Name = "Recovery Code")]
        public string RecoveryCode { get; set; }
    }
}
