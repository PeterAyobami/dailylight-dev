using System;

namespace Dailylight.Server.Core
{
    /// <summary>
    /// The user feedback database table representational model
    /// </summary>
    public class UserFeedbackDataModel
    {
        #region Private Members

        /// <summary>
        /// Feedback date backing store
        /// </summary>
        public DateTimeOffset mFeedbackDate;

        #endregion


        /// <summary>
        /// The unique id for this entry
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The id of user that made this feedback
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// The category of the users feedback
        /// </summary>
        public string FeedbackCategoryId { get; set; }

        /// <summary>
        /// The users feedback title
        /// </summary>
        public string FeedbackTitle { get; set; }

        /// <summary>
        /// The users feedback message
        /// </summary>
        public string FeedbackMessage { get; set; }

        /// <summary>
        /// The relational user that made this feedback
        /// </summary>
        public ApplicationUser User { get; set; }

        /// <summary>
        /// The relational feedback category of this feedback
        /// </summary>
        public FeedbackCategoryDataModel FeedbackCategory { get; set; }

        /// <summary>
        /// The date this feedback was made
        /// </summary>
        public DateTimeOffset FeedbackDate { get => DateTimeOffset.Now; set => mFeedbackDate = value; }
    }
}
