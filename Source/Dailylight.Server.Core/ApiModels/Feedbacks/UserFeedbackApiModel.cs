namespace Dailylight.Server.Core
{
    /// <summary>
    /// The api model for user feedback
    /// </summary>
    public class UserFeedbackApiModel
    {
        /// <summary>
        /// The users feedback title
        /// </summary>
        public string FeedbackTitle { get; set; }

        /// <summary>
        /// The category of the users feedback
        /// </summary>
        public string FeedbackCategory { get; set; }

        /// <summary>
        /// The users feedback message
        /// </summary>
        public string FeedbackMessage { get; set; }
    }
}
