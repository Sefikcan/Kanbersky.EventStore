using GreenPipes;
using Kanbersky.EventStore.Core.Settings.Concrete;
using MassTransit;
using MassTransit.RabbitMqTransport;
using System;

namespace Kanbersky.EventStore.Core.Messaging.Concrete.RabbitMQ
{
    public class BusConfigurator
    {
        private static readonly Lazy<BusConfigurator> configurator = new Lazy<BusConfigurator>(() => new BusConfigurator());

        public static BusConfigurator Instance => configurator.Value;

        public IBusControl ConfigureBus(MassTransitSettings massTransitSettings,
           Action<IRabbitMqBusFactoryConfigurator> registrationAction = null)
        {
            return Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri(massTransitSettings.MassTransitUri), hst =>
                {
                    hst.Username(massTransitSettings.UserName);
                    hst.Password(massTransitSettings.Password);
                });

                //cfg.PublishTopology.BrokerTopologyOptions = PublishBrokerTopologyOptions.MaintainHierarchy;

                if (massTransitSettings.MessageRetryCount != null)
                {
                    cfg.UseMessageRetry(r => r.Immediate(massTransitSettings.MessageRetryCount.Value)); // Retry pattern!!.İlgili işlemi yapmak için belirlenen kadar deneyecektir. Hata devam ederise mesajı error queue’a atıp bir sonrakine devam edecektir.
                }

                if (massTransitSettings.TripThreshold != null && massTransitSettings.ActiveThreshold != null && massTransitSettings.ResetInterval != null)
                {
                    cfg.UseCircuitBreaker(cb =>
                    {
                        cb.TrackingPeriod = TimeSpan.FromMinutes(massTransitSettings.TrackingPeriod.Value);//TrackingPeriod ise ResetInterval süresinden sonra belirlediğimiz süre daha tetikte beklemesi söylüyor.Tekrar hata alınması durumunda ActiveThreshold ve TripThreshold limitlerini beklemeden yine resetinterval'da belirlenen süre ile beklemeye geçecektir.
                        cb.TripThreshold = massTransitSettings.TripThreshold.Value; // Alınan taleplerimizin belirlediğimiz % oranında hata olması durumunda restart olmasını sağlar.
                        cb.ActiveThreshold = massTransitSettings.ActiveThreshold.Value; // Üst üste belirlediğimiz miktarda hata aldığımızda restart olmasını sağlar
                        cb.ResetInterval = TimeSpan.FromMinutes(massTransitSettings.ResetInterval.Value); // Hata oluşması durumlarında sistemin belirlediğimiz sürede beklemesi gerektiğini belirtiyoruz.
                    });
                }

                if (massTransitSettings.RateLimit != null && massTransitSettings.RateLimiterInterval != null)
                {
                    cfg.UseRateLimit(massTransitSettings.RateLimit.Value, TimeSpan.FromSeconds(massTransitSettings.RateLimiterInterval.Value)); // servise belirlediğimiz süre içerisinde belirlenen request adeti kadar istek yapabilecek şekilde yapıyorum.
                }

                registrationAction.Invoke(cfg);
            });
        }
    }
}
