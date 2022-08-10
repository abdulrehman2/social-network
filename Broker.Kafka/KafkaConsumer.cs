using Confluent.Kafka;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafka.Connector
{
    public class KafkaConsumer : Contracts.IMessageBusConsumer
    {
        private readonly string bootstrapServers;
        private readonly string topic = "test";
        private readonly ConsumerConfig _config;
        private readonly string groupId = "test_group";
       

        public KafkaConsumer(IOptions<Settings.KafkaConnectionSettings> options)
        {
            bootstrapServers = $"{options.Value.HostName}:{options.Value.Port}";
            _config = new ConsumerConfig
            {
                GroupId = groupId,
                BootstrapServers = bootstrapServers,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
        }
        

        public void ListenForEvent(Action<string> callbackFunction)
        {
            using (var consumerBuilder = new ConsumerBuilder<Ignore, string>(_config).Build())
            {
                consumerBuilder.Subscribe(topic);
                var cancelToken = new CancellationTokenSource();

                try
                {
                    while (true)
                    {
                        var consumer = consumerBuilder.Consume(cancelToken.Token);
                        Debug.WriteLine($"Message Received on Kafka: {consumer.Message.Value}");
                        callbackFunction(consumer.Message.Value);
                    }
                }
                catch (OperationCanceledException)
                {
                    consumerBuilder.Close();
                }
            }
        }




      
    }
}
