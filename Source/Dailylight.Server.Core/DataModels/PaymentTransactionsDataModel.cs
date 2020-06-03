using System;

namespace Dailylight.Server.Core
{
    /// <summary>
    /// The payment transacxtions database table representational model
    /// </summary>
    public class PaymentTransactionsDataModel
    {
        /// <summary>
        /// The unique id for this entry
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The reference id for this transaction
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// The user id related to this transaction
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// The purchased edition id
        /// </summary>
        public string EditionId { get; set; }

        /// <summary>
        /// The flag to indicate verified 
        /// status of a payment transaction
        /// </summary>
        public bool PaymentVerified { get; set; }

        /// <summary>
        /// The related user to this transaction
        /// </summary>
        public ApplicationUser User { get; set; }

        /// <summary>
        /// The related purchased devotion edition data model
        /// </summary>
        public DevotionEditionsDataModel DevotionEditions { get; set; }

        /// <summary>
        /// The date of this transaction
        /// </summary>
        public DateTimeOffset TransactionDate { get; set; }
    }
}
