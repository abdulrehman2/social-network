using AutoMapper;
using Grpc.Net.Client;
using Grpc.Protos.Identity;
using Microsoft.Extensions.Configuration;
using Post.Application.Contracts.SyncDataServices.Grpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Infrastructure.SyncDataServices.Grpc
{
    internal class FriendDataClient: IFriendDataClient
    {
        private IConfiguration _configuration;
        private IMapper _mapper;

        public FriendDataClient(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;

        }


        public IEnumerable<Application.Dtos.Friend.UserFriendDto> ReturnAllUserFriends(int userId)
        {
            Console.WriteLine($"-->Calling GRPC Service {_configuration["GrpcPlatform"]}");

            var channel = GrpcChannel.ForAddress(_configuration["GrpcUsers"]);
            var client = new GrpcFriend.GrpcFriendClient(channel);
            var request = new GetAllFriendRequest();
            request.UserId = userId;
            try
            {
                var reply = client.GetAllFriends(request);
                return _mapper.Map<IEnumerable<Application.Dtos.Friend.UserFriendDto>>(reply.Friend);

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
