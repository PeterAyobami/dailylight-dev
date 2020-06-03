using System;
using System.Collections.Generic;
using System.Text;

namespace Dailylight.Server.Core
{
    /// <summary>
    /// The api model for user reviews
    /// </summary>
    public class DevotionReviewsApiModel
    {
        /// <summary>
        /// The id of devotion this review is pointing towards
        /// </summary>
        public string DevotionId { get; set; }

        /// <summary>
        /// The message of this review
        /// </summary>
        public string ReviewMessage { get; set; }
    }
}
