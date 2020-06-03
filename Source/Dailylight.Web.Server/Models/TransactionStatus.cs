namespace Dailylight.Web.Server
{
    /// <summary>
    /// Transaction status flags
    /// </summary>
    public enum TransactionStatus
    {
        /// <summary>
        /// The transaction failed
        /// </summary>
        Failed = 0,
        /// <summary>
        /// The transaction is not recognized
        /// </summary>
        NotRecognized = 1,
        /// <summary>
        /// The transaction was successful
        /// </summary>
        Successful = 2
    }
}
