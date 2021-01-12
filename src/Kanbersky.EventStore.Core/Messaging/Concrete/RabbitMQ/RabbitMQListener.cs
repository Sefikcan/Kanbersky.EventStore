using Kanbersky.EventStore.Core.Messaging.Abstract;
using Kanbersky.EventStore.Core.Messaging.Models;
using Kanbersky.EventStore.Core.Results.Exceptions.Concrete;
using Kanbersky.EventStore.Core.Settings.Concrete;
using MassTransit;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Kanbersky.EventStore.Core.Messaging.Concrete.RabbitMQ
{
    /// <summary>
    /// 
    /// </summary>
    public class RabbitMQListener : IEventListener
    {
        private readonly MassTransitSettings _massTransitSettings;
        private readonly IServiceProvider _serviceProvider;
        private IBusControl _bus;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="massTransitSettings"></param>
        /// <param name="serviceProvider"></param>
        public RabbitMQListener(MassTransitSettings massTransitSettings,
            IServiceProvider serviceProvider)
        {
            _massTransitSettings = massTransitSettings;
            _serviceProvider = serviceProvider;
            _bus = BusConfigurator.Instance.ConfigureBus(_massTransitSettings);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task Publish<T>(T model) where T : BaseMessagingModel
        {
            return _bus.Publish(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="THandler"></typeparam>
        public void Subscribe<T, THandler>()
            where T : BaseMessagingModel
            where THandler : IEventHandler<T>
        {
            if (_bus == null)
                throw new NotFoundException("Mastransit could not initialized");

            var queueName = typeof(T).Name;
            _bus = BusConfigurator.Instance.ConfigureBus(_massTransitSettings,
              (cfg) =>
              {
                  cfg.ReceiveEndpoint(queueName,
                      e =>
                      {
                          //Farklı exchange tipleri ile çalışmak için kullanılır
                          //e.AutoDelete = true;
                          //e.Durable = false;
                          //e.ExchangeType = ExchangeType.Direct;
                          //e.Bind("exchange-name");
                          //e.Bind<T>();
                          e.Consumer(() =>
                          {
                              var handler = _serviceProvider.GetService<IEventHandler<T>>();
                              return new MassTransitConsumerAdapter<T>(handler);
                          });
                      });
              });

            _bus.Start();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="THandler"></typeparam>
        public void UnSubscribe<T, THandler>()
            where T : BaseMessagingModel
            where THandler : IEventHandler<T>
        {
            if (_bus == null)
                throw BaseException.NotFoundException("Mastransit could not initialized");

            _bus.Stop();
            _bus = null;
        }
    }
}
