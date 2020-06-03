using System.ComponentModel.DataAnnotations;

namespace Dailylight.Server.Core
{
    /// <summary>
    /// The users login credentials
    /// </summary>
    public class LoginCredentials
    {
        /// <summary>
        /// The users username or email
        /// </summary>
        [Required(ErrorMessage = "Username or email is required")]
        public string UsernameOrEmail { get; set; }

        /// <summary>
        /// The users password
        /// </summary>
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
