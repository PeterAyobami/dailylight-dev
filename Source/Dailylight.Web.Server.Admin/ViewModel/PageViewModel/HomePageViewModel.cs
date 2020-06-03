using Dailylight.Server.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Dailylight.Web.Server.Admin
{
    /// <summary>
    /// The home page view model interaction logic
    /// </summary>
    public class HomePageViewModel : BaseViewModel
    {
        #region Protected Members

        /// <summary>
        /// Total number of dailylight users
        /// </summary>
        protected string mTotalUsersCount;

        /// <summary>
        /// Devotion edition purchases
        /// </summary>
        protected List<PurchasedEditionsDataModel> mPurchasedEditions;

        /// <summary>
        /// Edition purchases count
        /// </summary>
        protected string mEditionPurchasesCount;

        /// <summary>
        /// Bug reports user feedacks
        /// </summary>
        protected List<UserFeedbackDataModel> mBugReportsFeedbacks;

        /// <summary>
        /// Suggestion reports user feedbacks
        /// </summary>
        protected List<UserFeedbackDataModel> mSuggestionReportsFeedback;

        protected List<ApplicationUser> mTotalUsers;

        protected string mPurchasedEditionRatio;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public HomePageViewModel()
        {
            
        }

        #endregion

        /// <summary>
        /// Async methods to get total dailylight users
        /// </summary>
        /// <returns></returns>
        protected async Task<string> GetTotalUsersCountAsync()
        {
            // Get the users
            var result = await Context.Users.CountAsync();

            // Return the string value
            return result.ToString();
        }

        protected async Task<string> GetEditionPurchasesRatio()
        {
            // Instantiate users
            List<ApplicationUser> totalUsers = new List<ApplicationUser>();

            // Get current year
            var currentYear = DateTimeOffset
                .Now
                .ToString("yyyy");

            // Get all current purchases
            var purchases = await Context
                .PurchasedEditions
                .Include(x => x.Edition)
                .Where(x => x.Edition.EditionYear == currentYear)
                .ToListAsync();

            // Iterate over all purchases
            foreach (var item in purchases)
            {
                var user = await Context.Users.FindAsync(item.UserId);

                totalUsers.Add(user);
            }

            return totalUsers.Count.ToString();
        }

        /// <summary>
        /// Get the total dailylight users
        /// </summary>
        /// <returns></returns>
        protected string GetTotalUsersCount()
        {
            // Get the users
            var result = Context.Users.Count();

            // Return the string value
            return result.ToString();
        }

        /// <summary>
        /// Get total devotions purchases for all editions
        /// </summary>
        /// <returns>Edition purchases value in string format</returns>
        protected async Task<List<PurchasedEditionsDataModel>> GetEditionPurchasesAsync()
        {
            var currentYear = DateTimeOffset
                .Now
                .ToString("yyyy");

            var purchases = await Context
                .PurchasedEditions
                .Include(x => x.Edition)
                .Where(x => x.Edition.EditionYear == currentYear)
                .ToListAsync();

            return purchases;
        }
        

        /// <summary>
        /// Get total count of edition purchases
        /// </summary>
        /// <returns>String format of total edition purchases count</returns>
        protected async Task<string> GetEditionPurchasesCountAsync()
        {
            var purchases = await Context
                .PurchasedEditions
                .Where(x => x.Edition.Edition == 21)
                .Include(x => x.Edition)
                .ToListAsync();

            var count = purchases
                .Count
                .ToString();

            return count;
        }

        /// <summary>
        /// Gets all the users feedbacks based on feature suggestions
        /// </summary>
        /// <returns></returns>
        protected async Task<List<UserFeedbackDataModel>> GetUsersFeatureSuggestionsAsync()
        {
            var feedback = await Context
                .UserFeedbacks
                .Include(x => x.FeedbackCategory)
                .Where(x => x.FeedbackCategory.FeedbackCategory == "Suggestion Reports")
                .ToListAsync();

            return feedback;
        }

        /// <summary>
        /// Gets all the users feedbacks based on bug reports
        /// </summary>
        /// <returns></returns>
        protected async Task<List<UserFeedbackDataModel>> GetUsersBugReportsAsync()
        {
            var feedback = await Context
                .UserFeedbacks
                .Include(x => x.FeedbackCategory)
                .Where(x => x.FeedbackCategory.FeedbackCategory == "Bug Reports")
                .ToListAsync();

            return feedback;
        }

        /// <summary>
        /// Gets all un-completed / un-successful payment transactions
        /// </summary>
        /// <returns></returns>
        protected async Task<List<PaymentTransactionsDataModel>> GetUnverifiedPaymentTransactionsAsync()
        {
            var transactions = await Context
                .PaymentTransactions
                .Where(x => x.PaymentVerified == false)
                .ToListAsync();

            return transactions;
        }

        protected string GetCurrentEdition()
        {
            return mPurchasedEditions
                .Select(x => x.Edition.Edition)
                .First()
                .ToString();
        }

        protected string EditionsSum()
        {
            return mPurchasedEditions
                .Select(x => x.Edition.EditionPrice)
                .Sum()
                .ToString("C", new CultureInfo("en-NG"));
        }
    }
}
