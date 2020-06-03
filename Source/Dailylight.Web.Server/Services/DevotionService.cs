using Dailylight.Server.Core;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace Dailylight.Web.Server
{
    public class DevotionService : DevotionServiceHandler.DevotionServiceHandlerBase
    {
        #region Private Members

        /// <summary>
        /// Generic interface used to activate ILogger from DI
        /// </summary>
        private readonly ILogger<DevotionService> mLogger;

        /// <summary>
        /// The scoped instance of the <see cref="ApplicationDbContext"/>
        /// </summary>
        private ApplicationDbContext mContext;

        #endregion


        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public DevotionService(ILogger<DevotionService> logger, ApplicationDbContext context)
        {
            mLogger = logger;
            mContext = context;
        }

        #endregion


        #region Overrides

        public override Task<DevotionsReply> RequestDevotion(DevotionsRequest request, ServerCallContext context)
        {
            // Get the data
            var devotion = mContext.Devotions.First();

            DevotionsReply result = new DevotionsReply();

            try
            {
                result = new DevotionsReply
                {
                    Id = devotion?.Id,
                    Title = devotion?.Title,
                    AnchorScripture = devotion?.AnchorScripture,
                    TextQuote = devotion?.TextQuote,
                    FirstTextParagraph = devotion?.FirstTextParagraph,
                    SecondTextParagraph = devotion?.SecondTextParagraph,
                    ThirdTextParagraph = devotion?.ThirdTextParagraph,
                    FurtherReading = devotion?.FurtherReading,
                    PrayerPoint = devotion?.PrayerPoint,
                    ImageUrl = devotion?.ImageUrl,
                    Date = devotion?.Date.ToString()
                };
            }
            catch (System.ArgumentNullException ex)
            {
                mLogger.LogError(ex.Message);
            }

            return Task.FromResult(result);
        }

        #endregion
    }
}
