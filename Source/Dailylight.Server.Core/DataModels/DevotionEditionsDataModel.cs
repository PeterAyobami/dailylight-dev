using System;

namespace Dailylight.Server.Core
{
    /// <summary>
    /// The devotions editions database table representational model
    /// </summary>
    public class DevotionEditionsDataModel
    {
        #region Private Members

        /// <summary>
        /// Backing store for <see cref="DateAdded"/>
        /// </summary>
        private DateTimeOffset mDateAdded;

        #endregion


        /// <summary>
        /// The unique id for this entry
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The actual edition of the devotion
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

        /// <summary>
        /// The image url of this edition
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// The date and time this record was added
        /// </summary>
        public DateTimeOffset DateAdded { get => DateTimeOffset.UtcNow; set => mDateAdded = value; }
    }
}
