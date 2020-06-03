using Dailylight.Server.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Dailylight.Web.Server.Admin
{
    /// <summary>
    /// Manages the standard web server pages
    /// </summary>
    public class HomeController : Controller
    {
        #region Protected Members

        /// <summary>
        /// The swcoped instance of the <see cref="ApplicationDbContext"/>
        /// </summary>
        protected ApplicationDbContext mContext;

        /// <summary>
        /// The manager for handling creation, deletion, searching, etc... of users
        /// </summary>
        protected UserManager<ApplicationUser> mUserManager;

        /// <summary>
        /// The manager for handling signing in and out of users
        /// </summary>
        protected SignInManager<ApplicationUser> mSignInManager;

        #endregion


        #region Constructor

        /// <summary>
        /// Default constructor
        /// <paramref name="context">The injected context</paramref>
        /// <paramref name="signInManager">The injected sign in manager</paramref>
        /// <paramref name="userManager">The injected user manager</paramref>
        /// </summary>
        public HomeController(ApplicationDbContext context, 
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager)
        {
            mContext = context;
            mUserManager = userManager;
            mSignInManager = signInManager;
        }

        #endregion


        #region Action Methods



        #endregion
    }
}
