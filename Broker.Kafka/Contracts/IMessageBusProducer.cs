using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafka.Connector.Contracts
{
    public interface IMessageBusProducer
    {
        Task<bool> SendMessage(string message);
    }
}
