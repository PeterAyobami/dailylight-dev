using Dailylight.Server.Core;
using Dna;
using System;
using System.Security.Claims;

namespace Dailylight.Web.Server.Admin
{
    /// <summary>
    /// A short hand access class to get DI services with nice clean short code
    /// </summary>
    public static class DI
    {
        /// <summary>
        /// The identity user claims proncipal
        /// </summary>
        public static ClaimsPrincipal ClaimsPrincipal { get; set; } = new ClaimsPrincipal { };

        /// <summary>
        /// The service provider for this application
        /// </summary>
        public static IServiceProvider Provider { get; set; }

        /// <summary>
        /// The scoped instance of the <see cref="ApplicationDbContext"/>
        /// </summary>
        public static ApplicationDbContext Context => Framework.Service<ApplicationDbContext>();
    }
}
