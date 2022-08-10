using Confluent.Kafka;
using Kafka.Connector.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Post.Application.Contracts.EventProcessing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Post.Infrastructure.BackgroundServices
{
    public class KafkaEventListener : IHostedService
    {
        //private readonly IEventProcessor _eventProcessor;
        private  Kafka.Connector.Contracts.IMessageBusConsumer _kafkaConsumer;
        private  IServiceProvider Services { get; }


        public KafkaEventListener(IServiceProvider services)
        {
            //IEventProcessor eventProcessor, Kafka.Connector.Contracts.IMessageBusConsumer kafkaConsumer
            //_eventProcessor = eventProcessor;
            //_kafkaConsumer = kafkaConsumer;
            Services = services;
        }


        public  Task StartAsync(CancellationToken cancellationToken)
        {

            try
            {
   
                using (var scope = Services.CreateScope())
                {
                    _kafkaConsumer = scope.ServiceProvider.GetRequiredService<IMessageBusConsumer>();
                    _kafkaConsumer.ListenForEvent(MessageReceived);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return Task.CompletedTask;
        }
        public  Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public void MessageReceived(string message)
        {
            using (var scope = Services.CreateScope())
            {
                var scopedProcessingService =scope.ServiceProvider.GetRequiredService<IEventProcessor>();
                scopedProcessingService.ProcessEvent(message);
            }
                
        }

    }
}
