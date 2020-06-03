using System;

namespace Dailylight.Server.Core
{
    /// <summary>
    /// The purchased editions database table representational model
    /// </summary>
    public class PurchasedEditionsDataModel
    {
        #region Private Members

        /// <summary>
        /// The backing store for <see cref="DatePurchased"/>
        /// </summary>
        private DateTimeOffset mDatePurchased;

        #endregion


        /// <summary>
        /// The unique id for this entry
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The user id of this purchased edition
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// The relational user of this purchased edition
        /// </summary>
        public ApplicationUser User { get; set; }

        /// <summary>
        /// The purchased edition id
        /// </summary>
        public string EditionId { get; set; }

        /// <summary>
        /// The relational purchased edition
        /// </summary>
        public DevotionEditionsDataModel Edition { get; set; }

        /// <summary>
        /// The date this edition was purchased
        /// </summary>
        public DateTimeOffset DatePurchased { get => DateTimeOffset.Now; set => mDatePurchased = value; }
    }
}
