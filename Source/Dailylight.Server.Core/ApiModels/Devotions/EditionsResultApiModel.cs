namespace Dailylight.Server.Core
{
    /// <summary>
    /// The editions result api model
    /// </summary>
    public class EditionsResultApiModel
    {
        /// <summary>
        /// The id if the devotion edition
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The serial number of the devotion edition
        /// </summary>
        public int Edition { get; set; }

        /// <summary>
        /// The year of the devotion edition
        /// </summary>
        public string EditionYear { get; set; }

        /// <summary>
        /// The price of the devotion edition
        /// </summary>
        public float EditionPrice { get; set; }
    }
}
