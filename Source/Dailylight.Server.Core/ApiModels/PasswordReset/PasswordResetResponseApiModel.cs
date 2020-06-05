namespace Dailylight.Server.Core
{
    /// <summary>
    /// The api response for password reset
    /// </summary>
    public class PasswordResetResponseApiModel
    {
        /// <summary>
        /// Flag indicating if operation was successful
        /// </summary>
        public bool Successful => ErrorMessage == null;

        /// <summary>
        /// Error message of a failed operation
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}
