using Dailylight.Server.Core;
using System.Threading.Tasks;

namespace Dailylight.Web.Server.Admin
{
    /// <summary>
    /// The devotion modify page view model interaction logic
    /// </summary>
    public class DevotionModifyPageViewModel : DevotionReviewPageViewModel
    {
        protected bool FlashDevotionModifiedSuccess { get; set; }
        protected bool ShowingDevotionModifyConfirmationDialog { get; set; }

        public DevotionsViewModel DevotionViewModel { get; set; }


        protected async Task UpdateDevotionAsync(DevotionsViewModel model)
        {
            // Get the devotion to update...
            var devotion = await Context.Devotions.FindAsync(model.Id);

            // Set new data
            devotion.Date = model.Date;
            devotion.Title = model.Title;
            devotion.AnchorScripture = model.AnchorScripture;
            devotion.FurtherReading = model.FurtherReading;
            devotion.TextQuote = model.TextQuote;
            devotion.FirstTextParagraph = model.FirstTextParagraph;
            devotion.SecondTextParagraph = model.SecondTextParagraph;
            devotion.ThirdTextParagraph = model.ThirdTextParagraph;
            devotion.ImageUrl = model.ImageUrl.Substring(12);
            devotion.MemoryVerse = model.MemoryVerse;
            devotion.PrayerPoint = model.PrayerPoint;
            devotion.PropheticDeclaration = model.PropheticDeclaration;

            // Update...
            Context.Devotions.Update(devotion);

            // Save changes
            await Context.SaveChangesAsync();

            // Clear entry data
            devotion.Title = string.Empty;
            devotion.AnchorScripture = string.Empty;
            devotion.FurtherReading = string.Empty;
            devotion.TextQuote = string.Empty;
            devotion.FirstTextParagraph = string.Empty;
            devotion.SecondTextParagraph = string.Empty;
            devotion.ThirdTextParagraph = string.Empty;
            devotion.ImageUrl = string.Empty;
            devotion.PrayerPoint = string.Empty;
            devotion.MemoryVerse = string.Empty;
            devotion.PropheticDeclaration = string.Empty;

            // Flash that the operation was successful
            FlashDevotionModifiedSuccess = true;

            // Drop the dialog
            ShowingDevotionModifyConfirmationDialog = false;

            NavigationManager.NavigateTo("/devotions");
        }
    }
}
