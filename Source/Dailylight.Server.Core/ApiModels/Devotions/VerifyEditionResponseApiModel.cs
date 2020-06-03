namespace Dailylight.Server.Core
{
    /// <summary>
    /// Api model for edition verification response
    /// </summary>
    public class VerifyEditionResponseApiModel
    {
        /// <summary>
        /// The flag to tell if a user own a purchased edition
        /// </summary>
        public bool HasEdition { get; set; }
    }
}
