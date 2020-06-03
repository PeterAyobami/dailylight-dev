namespace Dailylight.Web.Server.Admin
{
    /// <summary>
    /// The theme info for the data table component
    /// </summary>
    public class DataTableCascadingInfo : BaseCascadingInfo
    {
        /// <summary>
        /// The cascaded zero or empty level of stock
        /// </summary>
        public const string ZeroStock = "c-table__row--danger";

        /// <summary>
        /// The cascaded low level of stock
        /// </summary>
        public const string LowStock = "c-table__row--warning";

        /// <summary>
        /// The cascaded moderate or 'beyond low' level of stock
        /// </summary>
        public const string ModerateStock = "c-table__row--success";

        /// <summary>
        /// The cascaded low rate of gross ratio
        /// </summary>
        public const string LowGrossRate = "u-color-warning";

        /// <summary>
        /// The cascaded moderate rate of gross ratio
        /// </summary>
        public const string ModerateGrossRate = "u-color-primary";

        /// <summary>
        /// The cascaded high rate of gross ratio
        /// </summary>
        public const string HighGrossRate = "u-color-info";

        /// <summary>
        /// The cascaded 'very high' rate of gross ratio
        /// </summary>
        public const string VeryHighGrossRate = "u-color-success";
    }
}
