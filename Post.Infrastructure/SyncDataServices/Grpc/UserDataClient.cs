using AutoMapper;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using Post.Application.Contracts.SyncDataServices.Grpc;
using Post.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Post.Infrastructure;
using Grpc.Protos.Identity;

namespace Post.Infrastructure.SyncDataServices.Grpc
{
    public class UserDataClient : IUserDataClient
    {

        private IConfiguration _configuration;
        private IMapper _mapper;

        public UserDataClient(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;

        }


        public IEnumerable<User> ReturnAllUsers()
        {
            Console.WriteLine($"-->Calling GRPC Service {_configuration["GrpcPlatform"]}");

            var channel = GrpcChannel.ForAddress(_configuration["GrpcUsers"]);
            var client = new GrpcUser.GrpcUserClient(channel);
            var request = new GetAllRequest();

            try
            {
                var reply = client.GetAllUsers(request);
                return _mapper.Map<IEnumerable<Domain.Entities.User>>(reply.User);

            }
            catch (Exception e)
            {
                Console.WriteLine($"--> Could not call GRPC Server {e.Message}");
                return null;
            }

            return null;
        }
    }
}
