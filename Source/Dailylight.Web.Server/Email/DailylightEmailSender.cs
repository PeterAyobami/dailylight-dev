using Dailylight.Server.Core;
using System.Threading.Tasks;
using static Dna.FrameworkDI;

namespace Dailylight.Web.Server
{
    /// <summary>
    /// Handles sending emails specific to the Fasetto Word server
    /// </summary>
    public static class DailylightEmailSender
    {
        /// <summary>
        /// Sends a verification email to the specified user
        /// </summary>
        /// <param name="displayName">The users display name (typically first name)</param>
        /// <param name="email">The users email to be verified</param>
        /// <param name="verificationUrl">The URL the user needs to click to verify their email</param>
        /// <returns></returns>
        public static async Task<SendEmailResponse> SendUserVerificationEmailAsync(string displayName, string email, string verificationUrl)
        {
            return await DI.EmailTemplateSender.SendGeneralEmailAsync(new SendEmailDetails
            {
                IsHTML = true,
                FromEmail = Configuration["FasettoSettings:SendEmailFromEmail"],
                FromName = Configuration["FasettoSettings:SendEmailFromName"],
                ToEmail = email,
                ToName = displayName,
                Subject = "Verify Your Email - Fasetto Word"
            },
            "Verify Email",
            $"Hi {displayName ?? "stranger"},",
            "Thanks for creating an account with us.<br/>To continue please verify your email with us.",
            "Verify Email",
            verificationUrl
            );
        }

        /// <summary>
        /// Sends a password reset email to the specified user
        /// </summary>
        /// <param name="displayName">The users display name</param>
        /// <param name="email">The users email</param>
        /// <param name="passwordResetLink">The password reset url</param>
        /// <returns></returns>
        public static async Task<SendEmailResponse> SendPasswordResetLinkEmailAsync(string displayName, string email, string passwordResetLink)
        {
            return await DI.EmailTemplateSender.SendGeneralEmailAsync(new SendEmailDetails
            {
                IsHTML = true,
                FromEmail = Configuration["DailylightSettings:SendEmailFromEmail"],
                FromName = Configuration["DailylightSettings:SendEmailFromName"],
                ToEmail = email,
                ToName = displayName,
                Subject = "Reset Your Password - Daily Light Media"
            },
            "Reset Your Password",
            $"Hi {displayName},",
            "You have requested to reset the password to your account on Daily Light Media. Click the link below to get started.<br/><br/>" +
            "PS: If you did not initiate this request, kindly ignore this email",
            "RESET MY PASSWORD",
            passwordResetLink);
        }
    }
}
