using System.ComponentModel.DataAnnotations;

namespace Dailylight.Web.Server.Admin
{
    /// <summary>
    /// The editions data view model
    /// </summary>
    public class EditionsViewModel
    {
        /// <summary>
        /// The term of the edition
        /// </summary>
        [Required(ErrorMessage = "Please specify the edition term")]
        public int Edition { get; set; }

        /// <summary>
        /// The yaer of the edition
        /// </summary>
        [Required(ErrorMessage = "Please specify the year of the edition")]
        public string EditionYear { get; set; }

        /// <summary>
        /// The price of the edition
        [Required(ErrorMessage = "Please specify a price for this edition")]
        public float EditionPrice { get; set; }

        /// <summary>
        /// The image url of the edition
        /// </summary>
        [Required(ErrorMessage = "An image is required for this entry")]
        public string ImageUrl { get; set; }
    }
}
