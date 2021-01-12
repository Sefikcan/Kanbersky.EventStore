using Kanbersky.EventStore.Core.Settings.Abstract;

namespace Kanbersky.EventStore.Core.Settings.Concrete
{
    public class MassTransitSettings : ISettings
    {
        /// <summary>
        /// 
        /// </summary>
        public string MassTransitUri { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? TripThreshold { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? ActiveThreshold { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? ResetInterval { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? RateLimit { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? RateLimiterInterval { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? TrackingPeriod { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? MessageRetryCount { get; set; }
    }
}
