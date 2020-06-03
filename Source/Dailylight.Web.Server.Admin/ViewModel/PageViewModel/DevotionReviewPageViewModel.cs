using Dailylight.Server.Core;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;

namespace Dailylight.Web.Server.Admin
{
    /// <summary>
    /// The devotions page view model interaction logic
    /// </summary>
    public class DevotionReviewPageViewModel : BaseViewModel
    {
        #region Protected Members

        /// <summary>
        /// The devotions title filter string
        /// </summary>
        protected string mFilterDevotionString;

        /// <summary>
        /// The devotions data model
        /// </summary>
        protected List<DevotionsDataModel> mDevotions = new List<DevotionsDataModel>();

        #endregion


        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public DevotionReviewPageViewModel()
        {
            this.mShowingBlankContentPage = true;
        }

        #endregion


        #region Flags

        /// <summary>
        /// Flag indicating that the devotions list view is in search mode
        /// </summary>
        protected bool mShowingSearchResuslts;

        /// <summary>
        /// Flag indicating the devotions filter options
        /// </summary>
        protected FilterOptions mFilterOptions;

        /// <summary>
        /// The display flag of the devotion content page
        /// </summary>
        protected bool mShowingBlankContentPage;

        /// <summary>
        /// The display flag of the devotion entry dialog
        /// </summary>
        protected bool mShowingDevotionEntryDialog;

        /// <summary>
        /// The flag that indicates the appearance state of the modify dialog
        /// </summary>
        protected bool mShowingModifyDevotionDialog;

        /// <summary>
        /// The flag that indicates when the devotion update is running
        /// </summary>
        protected bool mDevotionUpdateIsRunning;

        /// <summary>
        /// The flag that controls the appreance of the successfully deleted flash message
        /// </summary>
        protected bool  mShowingDevotionDeletionSuccessfulFlashMessage;

        /// <summary>
        /// 
        /// </summary>
        protected bool mShowingDevotionDeletionFailedFlashMessage;

        #endregion


        #region Injected Services



        #endregion


        #region Public Properties

        /// <summary>
        /// The devotions data model properties
        /// </summary>
        public List<DevotionsDataModel> Devotions
        {
            get
            {
                switch (mFilterOptions)
                {
                    case FilterOptions.Default:
                        return Context.Devotions.OrderBy(x => x.Date).ToList();
                    case FilterOptions.Title:
                        return GetTitleFilteredDevotions(mFilterDevotionString);
                    case FilterOptions.Month:
                        return Context.Devotions
                            .Where(x => x.DevotionMonth == DevotionsCategory.DevotionMonth)
                            .OrderBy(x => x.Date)
                            .ToList();
                    default:
                        return Context.Devotions.OrderBy(x => x.Date).ToList();
                }
            }

            set => mDevotions = value;
        }


        /// <summary>
        /// The single devotion data model
        /// </summary>
        public DevotionsDataModel Devotion { get; set; } = new DevotionsDataModel();


        /// <summary>
        /// The <see cref="DevotionsViewModel"/> properties
        /// </summary>
        public DevotionsViewModel DevotionsViewModel { get; set; } = new DevotionsViewModel();


        /// <summary>
        /// The <see cref="DevotionsCategoryViewModel"/> properties
        /// </summary>
        public DevotionsCategoryViewModel DevotionsCategory { get; set; } = new DevotionsCategoryViewModel();


        #endregion


        #region Commands

        /// <summary>
        /// The command to get a specified devotion
        /// </summary>
        /// <param name="devotion">The specified devotion</param>
        /// <returns></returns>
        protected void GetDevotion(DevotionsDataModel devotion)
        {
            Devotion = Context.Devotions
                .FirstOrDefault(x => x.Id == devotion.Id);

            mShowingBlankContentPage = false;
        }


        /// <summary>
        /// Filters devotions by title
        /// </summary>
        /// <param name="searchString">The specified title string</param>
        /// <returns>The corresponding devotions</returns>
        protected List<DevotionsDataModel> GetTitleFilteredDevotions(string searchString)
        {
            // Try getting the title specified devotion
            var devotion = Context.Devotions
                .Where(x => x.Title.Contains(searchString)).ToList();

            return devotion;
        }

        /// <summary>
        /// The command to display the devotion entry dialog
        /// </summary>
        protected void ShowDevotionModifyDialog(DevotionsDataModel dataModel)
        {
            // Show dialog
            //mShowingModifyDevotionDialog = true;

            NavigationManager.NavigateTo($"/devotion-modify/{Devotion.Id}");
        }

        /// <summary>
        /// Deletes a specified devotion from the admin client
        /// </summary>
        /// <param name="dataModel">The specified devotion</param>
        protected void DeleteDevotion(DevotionsDataModel dataModel)
        {
            // Try removing the devotion...
            try
            {
                // Remove the item
                Context.Devotions.Remove(dataModel);
                // Commit changes to database
                Context.SaveChanges();
            }
            // Catch any error...
            catch (System.Exception)
            {
                // If any error occur...
                // Show error message
                mShowingDevotionDeletionFailedFlashMessage = true;
            }

            mShowingDevotionDeletionSuccessfulFlashMessage = true;
            mShowingBlankContentPage = true;

            StateHasChanged();
        }


        /// <summary>
        /// The method to modify a specified devotion
        /// </summary>
        /// <param name="dataModel">The specified devotion data model</param>
        protected void ModifyDevotion(DevotionsDataModel dataModel)
        {
            // Flag running operation
            mDevotionUpdateIsRunning = true;

            // Get the devotion to update
            var devotion = Context.Devotions.Find(dataModel.Id);

            // Assign new values
            devotion.Date = dataModel.Date;
            devotion.Title = dataModel.Title;
            devotion.AnchorScripture = dataModel.AnchorScripture;
            devotion.FurtherReading = dataModel.FurtherReading;
            devotion.TextQuote = dataModel.TextQuote;
            devotion.FirstTextParagraph = dataModel.FirstTextParagraph;
            devotion.SecondTextParagraph = dataModel.SecondTextParagraph;
            devotion.ThirdTextParagraph = dataModel.ThirdTextParagraph;
            devotion.ImageUrl = dataModel.ImageUrl;
            devotion.MemoryVerse = dataModel.MemoryVerse;
            devotion.PrayerPoint = dataModel.PrayerPoint;
            devotion.PropheticDeclaration = dataModel.PropheticDeclaration;

            // Update...
            Context.Devotions.Update(devotion);

            // Commit changes to database
            Context.SaveChanges();

            // Flag not running operation
            mDevotionUpdateIsRunning = false;

            // Drop dialog
            mShowingModifyDevotionDialog = false;
        }


        /// <summary>
        /// Handles devotions filtering based on the devotions title
        /// </summary>
        /// <param name="viewModel">The view model thats passed in</param>
        protected void FilterDevotionsByTitle(DevotionsViewModel viewModel)
        {
            mFilterDevotionString = viewModel.Title;

            // Reset the filter option
            mFilterOptions = FilterOptions.Title;
        }


        /// <summary>
        /// Handles devotions filtering based on the specified selection
        /// </summary>
        /// <param name="e">The option thats selected</param>
        protected void HandleDevotionsFilter(ChangeEventArgs e)
        {
            if (e.Value.ToString() == "All")
                mFilterOptions = FilterOptions.Default;
            else
            {
                // Set the selected month value
                DevotionsCategory.DevotionMonth = e.Value.ToString();

                // Reset the filter option
                mFilterOptions = FilterOptions.Month;
            }
        }

        /// <summary>
        /// Closes the flashed message
        /// </summary>
        protected void OnFlashMessageClosed()
        {
            mShowingDevotionDeletionSuccessfulFlashMessage = false;

            StateHasChanged();
        }

        #endregion
    }
}
