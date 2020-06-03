namespace Dailylight.Web.Server
{
    /// <summary>
    /// The transaction result of a payment transaction
    /// </summary>
    public class TransactionResultDataModel
    {
        /// <summary>
        /// The status result of the transaction
        /// </summary>
        public TransactionStatus Status { get; set; }

        /// <summary>
        /// The transaction message of a transaction
        /// </summary>
        public string TransactionMessage { get; set; }
    }
}
