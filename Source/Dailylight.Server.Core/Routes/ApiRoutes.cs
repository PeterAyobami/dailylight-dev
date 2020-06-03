namespace Dailylight.Server.Core
{
    /// <summary>
    /// The relative routes to all Api calls in the server
    /// </summary>
    public class ApiRoutes
    {
        #region Login / Register / Verify

        /// <summary>
        /// The route to the Register Api method
        /// </summary>
        public const string Register = "api/register";

        /// <summary>
        /// The route to the Login Api method
        /// </summary>
        public const string Login = "api/login";

        /// <summary>
        /// The route to the VerifyEmail Api method
        /// </summary>
        /// <remarks>
        ///     Pass the userId and emailToken as get parameters.
        ///     i.e. /api/verify/email?userId=...&emailToken=...
        /// </remarks>
        public const string VerifyEmail = "api/verify/email";

        #endregion


        #region Devotions / Feedbacks / Reviews

        /// <summary>
        /// The route to the devotion editions stream endpoint
        /// </summary>
        public const string Editions = "api/editions/stream";

        /// <summary>
        /// The route to download devotion
        /// </summary>
        public const string Devotions = "api/devotions/download";

        /// <summary>
        /// The route to the feedback endpoint
        /// </summary>
        public const string Feedback = "api/feedback";

        /// <summary>
        /// The route to the review endpoint
        /// </summary>
        public const string Review = "api/review";

        /// <summary>
        /// The route to verify edition purchase
        /// </summary>
        public const string VerifyEdition = "api/edition/verify";

        /// <summary>
        /// The route endpoint to update the users profile
        /// </summary>
        public const string UpdateUserProfile = "api/user/profile/update";

        #endregion

        #region Payment Process / Payment Verification

        /// <summary>
        /// The route endpoint to process payments
        /// </summary>
        public const string ProcessPayment = "api/payment/process";

        /// <summary>
        /// The route to the payment verification endpoint
        /// </summary>
        public const string VerifyPayment = "api/payment/verify";

        #endregion
    }
}
