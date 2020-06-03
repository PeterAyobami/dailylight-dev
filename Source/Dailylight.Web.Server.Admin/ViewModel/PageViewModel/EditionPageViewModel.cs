using Dailylight.Server.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dailylight.Web.Server.Admin
{
    /// <summary>
    /// The editions page view model interaction logic
    /// </summary>
    public class EditionPageViewModel : BaseViewModel
    {
        #region Private Members

        /// <summary>
        /// Editions data model backing store
        /// </summary>
        private List<DevotionEditionsDataModel> mEditions = new List<DevotionEditionsDataModel>();

        /// <summary>
        /// Editions view model backing store
        /// </summary>
        private EditionsViewModel mEditionsViewModel = new EditionsViewModel();

        #endregion


        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public EditionPageViewModel()
        {
            Editions = mEditions;
            EditionsViewModel = mEditionsViewModel;
        }

        #endregion


        #region Public Properties

        /// <summary>
        /// The flag to control the state of edition flash UI
        /// </summary>
        public bool FlashEditionAddedSuccess { get; set; }

        /// <summary>
        /// The editions view model data
        /// </summary>
        public EditionsViewModel EditionsViewModel { get; set; }

        /// <summary>
        /// The editions data model
        /// </summary>
        public List<DevotionEditionsDataModel> Editions { get; set; }

        #endregion


        #region Methods

        /// <summary>
        /// Gets the devotion editions
        /// </summary>
        /// <returns></returns>
        public async Task<List<DevotionEditionsDataModel>> GetEditionsAsync()
        {
            return await Context.DevotionEditions.ToListAsync();
        }

        /// <summary>
        /// Adds devotion edition
        /// </summary>
        /// <returns></returns>
        public async Task AddEditionAsync(EditionsViewModel viewModel)
        {
            // Set the view model data
            var edition = new DevotionEditionsDataModel
            {
                Id = Guid.NewGuid().ToString(),
                Edition = viewModel.Edition,
                EditionYear = viewModel.EditionYear,
                EditionPrice = viewModel.EditionPrice,
                ImageUrl = viewModel.ImageUrl
            };

            // Add the edition
            await Context.DevotionEditions.AddAsync(edition);

            // Commit changes to data store
            await Context.SaveChangesAsync();

            Editions = await GetEditionsAsync();

            StateHasChanged();

            // Flag success flash data
            FlashEditionAddedSuccess = true;
        }

        /// <summary>
        /// Closes flash messages by unflaging control flags
        /// </summary>
        public void CloseFlashMessages()
        {
            FlashEditionAddedSuccess = false;
        }

        #endregion
    }
}
