using AutoMapper;
using Grpc.Core;
using Grpc.Protos.Identity;
using Identity.Infrastructure.Repositories;
using Identity.Application.Contracts.Repositories;

namespace Identity.Infrastructure.SyncDataServices.Grpc
{
    public class GrpcUserService : GrpcUser.GrpcUserBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GrpcUserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public override Task<UserResponse> GetAllUsers(GetAllRequest request, ServerCallContext context)
        {
            var response = new UserResponse();

            var users = _userRepository.GetAll();

            foreach (var plat in users)
            {
                response.User.Add(_mapper.Map<GrpcUserModel>(plat));
            }

            return Task.FromResult(response);
        }
    }
}
