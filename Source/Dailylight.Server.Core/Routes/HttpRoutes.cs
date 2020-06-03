namespace Dailylight.Server.Core
{
    /// <summary>
    /// the http routes constant
    /// </summary>
    public class HttpRoutes
    {
        /// <summary>
        /// The route to the login page
        /// </summary>
        public const string Login = "/login";

        /// <summary>
        /// The route to log a user out
        /// </summary>
        public const string Logout = "/logout";

        /// <summary>
        /// The route to the register page
        /// </summary>
        public const string Register = "/register";

        /// <summary>
        /// The route to download dailylight android 
        /// apk file
        /// </summary>
        public const string DownloadDLApk = "/dailylight/apk/download";
    }
}
