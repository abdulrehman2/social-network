using AutoMapper;
using Identity.Application.Contracts.AsyncDataServices;
using Kafka.Connector.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Identity.Infrastructure.AsyncDataServices
{
    public class MessageBusClient : IMessageBusClient
    {
        private readonly IMessageBusProducer messageBusProducer;
        private readonly IMapper _mapper;
        public MessageBusClient(IMessageBusProducer messageProducer, IMapper mapper)
        {
            messageBusProducer = messageProducer;
            _mapper = mapper;

        }
        public void PublishNewUser(Domain.Models.User user)
        {
            var userToPublish = _mapper.Map<Application.Dtos.UserManagement.UserPublishDto>(user);
            userToPublish.Event = "User_Published";
            var message = JsonSerializer.Serialize(userToPublish);
            messageBusProducer.SendMessage(message);
        }
    }
}
