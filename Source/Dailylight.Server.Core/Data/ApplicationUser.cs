using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Dailylight.Server.Core
{
    /// <summary>
    /// The <see cref="IdentityUser"/> model for our application
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// The users first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The users last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The devotion editions that a user hass
        /// </summary>
        public List<DevotionEditionsDataModel> DevotionEditions { get; set; }
    }
}
