using Dailylight.Server.Core;
using Dna;
using Microsoft.Extensions.DependencyInjection;

namespace Dailylight.Web.Server
{
    public static class DI
    {
        /// <summary>
        /// The scoped instance of the <see cref="ApplicationDbContext"/>
        /// </summary>
        public static ApplicationDbContext ApplicationDbContext => Framework.Provider.GetService<ApplicationDbContext>();

        /// <summary>
        /// The transient instance of the <see cref="IEmailSender"/>
        /// </summary>
        public static IEmailSender EmailSender => Framework.Provider.GetService<IEmailSender>();

        /// <summary>
        /// The transient instance of the <see cref="IEmailTemplateSender"/>
        /// </summary>
        public static IEmailTemplateSender EmailTemplateSender => Framework.Provider.GetService<IEmailTemplateSender>();
    }
}
