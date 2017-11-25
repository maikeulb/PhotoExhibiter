using System.ComponentModel.DataAnnotations;

namespace PhotoExhibiter.Features.Account.AccountViewModels
{
    public class LoginWithRecoveryCodeViewModel
    {
        [Required]
        [DataType (DataType.Text)]
        [Display (Name = "Recovery Code")]
        public string RecoveryCode { get; set; }
    }
}
