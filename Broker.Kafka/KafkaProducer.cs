using Confluent.Kafka;
using Kafka.Connector.Contracts;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Kafka.Connector
{
    public class KafkaProducer: IMessageBusProducer
    {

        private readonly string bootstrapServers;
        private readonly string topic = "test";
        private readonly ProducerConfig _config;

        public KafkaProducer(IOptions<Settings.KafkaConnectionSettings> settings)
        {
            bootstrapServers = $"{settings.Value.HostName}:{settings.Value.Port}";
            _config = new ProducerConfig
            {
                BootstrapServers = bootstrapServers,
                ClientId = Dns.GetHostName()
            };
        }

        
        public async Task<bool> SendMessage(string message)
        {
            try
            {
                using (var producer = new ProducerBuilder<Null, string>(_config).Build())
                {
                    var result = await producer.ProduceAsync
                    (topic, new Message<Null, string>
                    {
                        Value = message
                    });

                    Debug.WriteLine($"Delivery Timestamp: { result.Timestamp.UtcDateTime}");
                    return await Task.FromResult(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured: {ex.Message}");
                return false;
            }
        }
    }
}
