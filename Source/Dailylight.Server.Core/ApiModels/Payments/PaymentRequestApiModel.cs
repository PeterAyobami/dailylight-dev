using System;

namespace Dailylight.Server.Core
{
    /// <summary>
    /// The payment request api model
    /// </summary>
    public class PaymentRequestApiModel
    {
        #region Private Members

        /// <summary>
        /// Backing store for <see cref="Reference"/>
        /// </summary>
        private string mReference = Guid.NewGuid().ToString();

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public PaymentRequestApiModel()
        {
            Reference = mReference;
        }

        #endregion

        #region

        /// <summary>
        /// The amount charged for the transaction
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// The edition that is to be purchased
        /// </summary>
        public string EditionId { get; set; }

        /// <summary>
        /// The payment transaction reference
        /// </summary>
        public string Reference { get; set; }

        #endregion
    }
}
