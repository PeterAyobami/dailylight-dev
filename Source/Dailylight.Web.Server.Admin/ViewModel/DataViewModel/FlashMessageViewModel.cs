namespace Dailylight.Web.Server.Admin
{
    /// <summary>
    /// The view model binding for the application flash message
    /// </summary>
    public class FlashMessageViewModel
    {
        #region Private Members


        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public FlashMessageViewModel()
        {

        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The type of message to flash
        /// </summary>
        public string MessageType { get; set; }

        /// <summary>
        /// The message body
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The link to route to another page
        /// </summary>
        public string Route { get; set; }

        #endregion
    }
}
