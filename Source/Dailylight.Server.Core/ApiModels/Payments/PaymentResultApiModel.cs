using Microsoft.AspNetCore.Mvc;

namespace Dailylight.Server.Core
{
    /// <summary>
    /// The api model for payment transactions
    /// </summary>
    public class PaymentResultApiModel : ApiResponse
    {
        /// <summary>
        /// The authorised url passed back on a 
        /// successful payment initialization
        /// </summary>
        public string AuthorisationUrl { get; set; }
    }
}
