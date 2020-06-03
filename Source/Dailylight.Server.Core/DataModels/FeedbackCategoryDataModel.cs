using System;

namespace Dailylight.Server.Core
{
    /// <summary>
    /// The feedback category database table representational model
    /// </summary>
    public class FeedbackCategoryDataModel
    {
        #region Private Members

        /// <summary>
        /// Backing store for the feedback date
        /// </summary>
        private DateTimeOffset mDateAdded;

        #endregion


        /// <summary>
        /// The unique id for this entry
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The feedback category title
        /// </summary>
        public string FeedbackCategory { get; set; }

        /// <summary>
        /// The date this category was added
        /// </summary>
        public DateTimeOffset DateAdded { get => DateTimeOffset.Now; set => mDateAdded = value; }
    }
}
