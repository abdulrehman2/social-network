using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafka.Connector
{
    public static class KafkaServiceRegistration
    {
        public static IServiceCollection AddKafkaServices(this IServiceCollection services, IConfiguration configuration,bool isProduction)
        {
            services.Configure<Settings.KafkaConnectionSettings>(configuration.GetSection("Kafka"));
            services.AddTransient<Contracts.IMessageBusProducer, KafkaProducer>();
            services.AddTransient<Contracts.IMessageBusConsumer, KafkaConsumer>();
            return services;
        }
    }
}
