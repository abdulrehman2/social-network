using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Post.Application.Contracts.EventProcessing;
using Post.Application.Contracts.Repositories;
using Post.Application.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Post.Infrastructure.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private IServiceScopeFactory _serviceScopeFactory;
        private IMapper _mapper;

        public EventProcessor(IServiceScopeFactory serviceScopeFactory, AutoMapper.IMapper mapper)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _mapper = mapper;
        }

        public void ProcessEvent(string message)
        {
            var eventType = DetermineEvent(message);

            switch (eventType)
            {
                case EventType.UserPublished:
                    AddUser(message);
                    break;


                case EventType.Undetermined:
                    break;
            }
        }
        private EventType DetermineEvent(string notificationMessage)
        {
            Console.WriteLine("--> Determining Event");

            var eventType = JsonSerializer.Deserialize<Application.Dtos.Common.GenericEventDto>(notificationMessage);

            switch (eventType.Event)
            {

                case "User_Published":
                    Console.WriteLine("user published event detected");
                    return EventType.UserPublished;
                default:
                    Console.WriteLine("Could not determine event type");
                    return EventType.Undetermined;
            }
        }


        private void AddUser(string userPublishMessage)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {

                try
                {
                    var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                    var publishUserDto = JsonSerializer.Deserialize<Application.Dtos.User.UserPublishDto>(userPublishMessage);
                    var plat = _mapper.Map<Domain.Entities.User>(publishUserDto);
                    if (unitOfWork.Users.GetById(plat.ExternalId) == null)
                    {
                        unitOfWork.Users.Add(plat);
                        unitOfWork.Complete();
                        Console.WriteLine($"--> User added....");
                    }
                    else
                    {
                        Console.WriteLine($"--> User already exist....");
                    }

                }
                catch (Exception e)
                {

                    Console.WriteLine($"Could not add User to DB {e.Message}");
                }
            }
        }
    }
}
