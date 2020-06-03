using System;
using System.Collections.Generic;

namespace Dailylight.Server.Core
{
    /// <summary>
    /// The devotions database table representational model
    /// </summary>
    public class DevotionsDataModel
    {
        #region Private Members

        /// <summary>
        /// The year of a devotion record
        /// </summary>
        private string mDevotionYear;

        /// <summary>
        /// The month of a devotion record
        /// </summary>
        private string mDevotionMonth;

        /// <summary>
        /// The day of a devotion record
        /// </summary>
        private string mDevotionDay;

        #endregion


        #region Public Properties

        /// <summary>
        /// The unique iddentity for this entry
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The title of the devotion
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The main anchor scripture of the devotion
        /// </summary>
        public string AnchorScripture { get; set; }

        /// <summary>
        /// The anchor scripture quote of the devotion
        /// </summary>
        public string TextQuote { get; set; }

        /// <summary>
        /// The first paragraph of the devotions text
        /// </summary>
        public string FirstTextParagraph { get; set; }

        /// <summary>
        /// The second paragraph of the devotions text
        /// </summary>
        public string SecondTextParagraph { get; set; }

        /// <summary>
        /// The third paragraph of the devotions text
        /// </summary>
        public string ThirdTextParagraph { get; set; }

        /// <summary>
        /// The further reading texts of the devotion
        /// </summary>
        public string FurtherReading { get; set; }

        /// <summary>
        /// The momory verse of the devotion
        /// </summary>
        public string MemoryVerse { get; set; }

        /// <summary>
        /// The prayer point of the devotion
        /// </summary>
        public string PrayerPoint { get; set; }

        /// <summary>
        /// The prophetic declaration of the devotion
        /// </summary>
        public string PropheticDeclaration { get; set; }

        /// <summary>
        /// The image url of the devotion
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// The day of a devotion record
        /// </summary>
        public string DevotionDay { get => Date.ToString("dd MMMM yyyy"); set => mDevotionDay = value; }

        /// <summary>
        /// The month category of this devotion record
        /// </summary>
        public string DevotionMonth { get => Date.ToString("MMMM yyyy"); set => mDevotionMonth = value; }

        /// <summary>
        /// The year category of this devotion record
        /// </summary>
        public string DevotionYear { get => Date.ToString("yyyy"); set => mDevotionYear = value; }

        /// <summary>
        /// The devotion edition to this entry
        /// </summary>
        public DevotionEditionsDataModel DevotionEdition { get; set; }

        /// <summary>
        /// The relational reviews of this devotion
        /// </summary>
        public List<DevotionReviewsDataModel> DevotionReviews { get; set; }

        /// <summary>
        /// The date of the devotion
        /// </summary>
        public DateTimeOffset Date { get; set; }

        #endregion
    }
}
