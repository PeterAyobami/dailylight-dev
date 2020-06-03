using Dailylight.Server.Core;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;

namespace Dailylight.Web.Server.Admin
{
    /// <summary>
    /// The base view model for all blazor pages intearction logic
    /// </summary>
    public class BaseViewModel : ComponentBase
    {
        #region Injected Services
        
        /// <summary>
        /// The scoped instance of the <see cref="ApplicationDbContext"/>
        /// </summary>
        [Inject]
        public ApplicationDbContext Context { get; set; }

        /// <summary>
        /// The user manager for handling user creation, deletion,
        /// searching, etc...
        /// </summary>
        //[Inject]
        //public UserManager<ApplicationUser> UserManager { get; set; }

        /// <summary>
        /// The injected <see cref="NavigationManager"/> for uri navigation
        /// </summary>
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        #endregion

        public void CloseFlashMessages()
        {

        }
    }
}
