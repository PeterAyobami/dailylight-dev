using Dna;
using Dailylight.Core;

namespace Dailylight.Droid
{
    /// <summary>
    /// A shorthand access class to get DI services with nice clean short code
    /// </summary>
    public static class DI
    {
        /// <summary>
        /// A shortcut to access toe <see cref="IClientDataStore"/> service
        /// </summary>
        public static IClientDataStore ClientDataStore => Framework.Service<IClientDataStore>();
    }
}