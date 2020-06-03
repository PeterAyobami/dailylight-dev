using System;

namespace Dailylight.Server.Core
{
    /// <summary>
    /// The devotion reviews database table representational model
    /// </summary>
    public class DevotionReviewsDataModel
    {
        #region Private Members

        /// <summary>
        /// The review date backing store
        /// </summary>
        private DateTimeOffset mReviewDate;

        #endregion


        /// <summary>
        /// The unique id for this entry
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The user id that made this review
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// The devotion id this review point to
        /// </summary>
        public string DevotionId { get; set; }

        /// <summary>
        /// The users review text
        /// </summary>
        public string ReviewText { get; set; }

        /// <summary>
        /// The relational user of this review
        /// </summary>
        public ApplicationUser User { get;set; }

        /// <summary>
        /// The relational devotion of this review
        /// </summary>
        public DevotionsDataModel Devotion { get; set; }

        /// <summary>
        /// The date this review was made
        /// </summary>
        public DateTimeOffset ReviewDate { get => DateTimeOffset.UtcNow; set => mReviewDate = value; }
        }
}
