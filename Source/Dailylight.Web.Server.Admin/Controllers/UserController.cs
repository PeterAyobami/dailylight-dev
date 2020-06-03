using Dailylight.Server.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Dailylight.Web.Server.Admin
{
    /// <summary>
    /// Manages the standard web server pages
    /// </summary>
    public class UserController : Controller
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
        public UserController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            mContext = context;
            mUserManager = userManager;
            mSignInManager = signInManager;
        }

        #endregion

        #region Action Methods

        /// <summary>
        /// The action method that loads the login page
        /// </summary>
        /// <returns></returns>
        [Route("/login")]
        public IActionResult Login()
        {
            ViewData.Add("Title", "Sign In");

            return View();
        }


        /// <summary>
        /// The action method to sign in a user 
        /// with specified user login credentials
        /// </summary>
        /// <param name="loginCredentials">The users login credentials</param>
        /// <param name="returnUrl">The return url after successful login</param>
        /// <returns></returns>
        [HttpPost()]
        [Route("/User/SignInAsync")]
        public async Task<IActionResult> SignInAsync(
            [FromForm] LoginCredentials loginCredentials, string returnUrl)
        {
            // Sign out any previous session
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);

            // Verify if the user specified an email
            var isEmail = loginCredentials.UsernameOrEmail.Contains("@");

            Task<ApplicationUser> user = null;

            bool userExist = false;

            if (await mContext.Users.AnyAsync())
            {
                // Get the user
                userExist = isEmail ?
                    await mContext.Users.AnyAsync(x => x.Email == loginCredentials.UsernameOrEmail) :
                    await mContext.Users.AnyAsync(x => x.UserName == loginCredentials.UsernameOrEmail);

                if (userExist)
                {
                    // Get the user
                    user = isEmail ?
                        mUserManager.FindByEmailAsync(loginCredentials.UsernameOrEmail) :
                        mUserManager.FindByNameAsync(loginCredentials.UsernameOrEmail);
                }
            }

            // If the user is valid...
            if (user != null)
            {
                // Sign in user
                var result = await mSignInManager.PasswordSignInAsync(
                    user.Result.UserName, loginCredentials.Password, false, false);

                // If sign in was ssuccessful...
                if (result.Succeeded)
                {
                    // If return url is null...
                    if (string.IsNullOrEmpty(returnUrl))
                        // TODO: Render home view in a more proper way
                        return Redirect("/");

                    // TODO: Redirect to return url with home page user data
                    return Redirect(returnUrl);
                }
                // Otherwise...
                else
                    // Redirect to login
                    // TODO: Flash error message - Nice UI
                    return Redirect(HttpRoutes.Login);
            }
            // Otherwise...
            else
                // Redirect to login
                // TODO: Flash error message - Nice UI
                return Redirect(HttpRoutes.Login);
        }

        /// <summary>
        /// The action method to log a user out of the application
        /// </summary>
        /// <returns>Auto redirect to the login page</returns>
        [Route(HttpRoutes.Logout)]
        public async Task<IActionResult> SignOutAsync()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);

            return Redirect(HttpRoutes.Login);
        }


        #endregion
    }
}
