using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dailylight.Server.Core
{
    /// <summary>
    /// The database representational model for our application
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="options"></param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        #endregion


        #region DbSets

        /// <summary>
        /// The devotions for the application
        /// </summary>
        public DbSet<DevotionsDataModel> Devotions { get; set; }

        /// <summary>
        /// The devotions edition for the application
        /// </summary>
        public DbSet<DevotionEditionsDataModel> DevotionEditions { get; set; }

        /// <summary>
        /// The reviews of a particular devotion
        /// </summary>
        public DbSet<DevotionReviewsDataModel> DevotionReviews { get; set; }

        /// <summary>
        /// The feedbacks from the users
        /// </summary>
        public DbSet<UserFeedbackDataModel> UserFeedbacks { get; set; }

        /// <summary>
        /// The users purchased editions
        /// </summary>
        public DbSet<PurchasedEditionsDataModel> PurchasedEditions { get; set; }

        /// <summary>
        /// The feedback categories of users feedbacks
        /// </summary>
        public DbSet<FeedbackCategoryDataModel> FeedbackCategories { get; set; }

        /// <summary>
        /// The payment transactions of user
        /// </summary>
        public DbSet<PaymentTransactionsDataModel> PaymentTransactions { get; set; }

        #endregion
    }
}
