using Dailylight.Server.Core;
using System;
using System.Threading.Tasks;

namespace Dailylight.Web.Server.Admin
{
    /// <summary>
    /// The devotion entry page view model interaction logic
    /// </summary>
    public class DevotionEntryPageViewModel : BaseViewModel
    {
        #region Protected Members

        /// <summary>
        /// The <see cref="DevotionsViewModel"/> member
        /// </summary>
        protected DevotionsViewModel mDevotions;

        #endregion


        #region Flags

        /// <summary>
        /// The flag that indicates the appearance state of devotion flash data
        /// </summary>
        protected bool FlashDevotionAddedSuccess { get; set; }

        /// <summary>
        /// The flag that controls the appearance state of the devotion entry confirmation dialog
        /// </summary>
        protected bool ShowingDevotionEntryConfirmationDialog { get; set; }

        #endregion


        #region Public Properties

        /// <summary>
        /// The <see cref="DevotionsViewModel"/> properties
        /// </summary>
        protected DevotionsViewModel Devotions { get; set; } = new DevotionsViewModel();

        #endregion


        #region Commands

        /// <summary>
        /// The command to review the just added devotion
        /// </summary>
        /// <param name="devotions">The specified devotion</param>
        /// <returns></returns>
        protected async Task GoToDevotionAsync(DevotionsViewModel devotions)
        {

        }


        /// <summary>
        /// The command to add a new devotion
        /// </summary>
        /// <param name="devotions">The specified devotion properties</param>
        /// <returns></returns>
        protected async Task AddDevotionAsync(DevotionsViewModel model)
        {
            // Assign the values
            var devotion = new DevotionsDataModel
            {
                Id = Guid.NewGuid().ToString("N"),
                Date = model.Date,
                Title = model.Title,
                AnchorScripture = model.AnchorScripture,
                TextQuote = model.TextQuote,
                FirstTextParagraph = model.FirstTextParagraph,
                SecondTextParagraph = model.SecondTextParagraph,
                ThirdTextParagraph = model.ThirdTextParagraph,
                FurtherReading = model.FurtherReading,
                MemoryVerse = model.MemoryVerse,
                PrayerPoint = model.PrayerPoint,
                PropheticDeclaration = model.PropheticDeclaration,
                ImageUrl = model.ImageUrl.Substring(12)
            };

            // Add the record
            await Context.Devotions.AddAsync(devotion);

            // Commit changes to database
            await Context.SaveChangesAsync();

            // Clear entry data
            Devotions.Title = string.Empty;
            Devotions.AnchorScripture = string.Empty;
            Devotions.FurtherReading = string.Empty;
            Devotions.TextQuote = string.Empty;
            Devotions.FirstTextParagraph = string.Empty;
            Devotions.SecondTextParagraph = string.Empty;
            Devotions.ThirdTextParagraph = string.Empty;
            Devotions.ImageUrl = string.Empty;
            Devotions.PrayerPoint = string.Empty;
            Devotions.MemoryVerse = string.Empty;
            Devotions.PropheticDeclaration = string.Empty;

            // Flash that the operation was successful
            FlashDevotionAddedSuccess = true;

            // Drop the dialog
            ShowingDevotionEntryConfirmationDialog = false;
        }

        #endregion
    }
}
