using System.ComponentModel.DataAnnotations;

namespace Dailylight.Web.Server
{
    /// <summary>
    /// The password reset view model
    /// </summary>
    public class PasswordResetViewModel
    {
        /// <summary>
        /// The users user id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// The users new password
        /// </summary>
        [MaxLength(30, ErrorMessage = "Password characters cannot exceed thirty")]
        [MinLength(6, ErrorMessage = "Password characters cannot be less than six")]
        [Required(ErrorMessage = "This field is required")]
        public string NewPassword { get; set; }

        /// <summary>
        /// The users new password confirmation
        /// </summary>
        [Compare("NewPassword", ErrorMessage = "Passwords do not match")]
        [Required(ErrorMessage = "This field is required")]
        public string ConfirmNewPassword { get; set; }

        /// <summary>
        /// The users password reset token
        /// </summary>
        public string PasswordResetToken { get; set; }        
    }
}
