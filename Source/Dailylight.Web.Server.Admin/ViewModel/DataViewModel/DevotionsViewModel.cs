using System;
using System.ComponentModel.DataAnnotations;

namespace Dailylight.Web.Server.Admin
{
    /// <summary>
    /// The devotions data view model
    /// </summary>
    public class DevotionsViewModel
    {
        /// <summary>
        /// The devotions id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The user specified title of the devotion
        /// </summary>
        [Required(ErrorMessage = "Devotion title must be specified")]
        public string Title { get; set; }

        /// <summary>
        /// The user specified main anchor scripture of the devotion
        /// </summary>
        [Required(ErrorMessage = "Devotion anchor scripture must be specified")]
        public string AnchorScripture { get; set; }

        /// <summary>
        /// The user specified anchor scripture quote of the devotion
        /// </summary>
        [Required(ErrorMessage = "Devotion scripture quote must be specified")]
        public string TextQuote { get; set; }

        /// <summary>
        /// The user specified first paragraph of the devotions text
        /// </summary>
        [Required(ErrorMessage = "Please specify first text paragraph")]
        public string FirstTextParagraph { get; set; }

        /// <summary>
        /// The user specified second paragraph of the devotions text
        /// </summary>
        [Required(ErrorMessage = "Please specify second text paragraph")]
        public string SecondTextParagraph { get; set; }

        /// <summary>
        /// The user specified third paragraph of the devotions text
        /// </summary>
        [Required(ErrorMessage = "Please specify third text paragraph")]
        public string ThirdTextParagraph { get; set; }

        /// <summary>
        /// The user specified further reading texts of the devotion
        /// </summary>
        [Required(ErrorMessage = "Please specify further reading texts")]
        public string FurtherReading { get; set; }

        /// <summary>
        /// The muser specified memory verse of the devotion
        /// </summary>
        public string MemoryVerse { get; set; }

        /// <summary>
        /// The user specified prayer point of the devotion
        /// </summary>
        public string PrayerPoint { get; set; }

        /// <summary>
        /// The user specified prophetic declaration of the devotion
        /// </summary>
        public string PropheticDeclaration { get; set; }

        /// <summary>
        /// The user specified image url of the devotion
        /// </summary>
        [Required(ErrorMessage = "Please add an image")]
        public string ImageUrl { get; set; }

        /// <summary>
        /// The user specified date of the devotion
        /// </summary>
        [Required(ErrorMessage = "Devotion date must be specified")]
        public DateTimeOffset Date { get; set; } = DateTimeOffset.Now.AddMonths(1);
    }


    /// <summary>
    /// The devotions view model for filtering devotions monthly categories
    /// </summary>
    public class DevotionsCategoryViewModel
    {
        /// <summary>
        /// The month of the devotion
        /// </summary>
        public string DevotionMonth { get; set; }
    }
}
