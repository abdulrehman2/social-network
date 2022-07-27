using AutoMapper;
using Identity.Infrastructure.Data;
using Identity.Application.Dtos.Common;
using Identity.Application.Dtos.Friend;
using Identity.Application.Dtos.FriendRequest;
using Identity.Application.Contracts.Repositories;

namespace Identity.Infrastructure.Repositories
{
    public class FriendshipRepository :GenericRepository<Domain.Models.FriendShipRequest>, IFriendshipRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public FriendshipRepository(AppDbContext appDbContext, IMapper mapper):base(appDbContext)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public GenericResponse AcceptFriendRequest(int userId, AcceptFriendRequestDto acceptFriendRequest)
        {
            var response = new GenericResponse();
            try
            {
                var request = _appDbContext.FriendShipRequests.Where(x => x.Id == acceptFriendRequest.FriendRequestId && (x.SenderId == userId || x.ReceiverId == userId)).First();

                request.IsAccepted = true;
                request.AccpetedDate = DateTime.Now;

                _appDbContext.FriendShipRequests.Update(request);
                _appDbContext.SaveChanges();
                response.Message = "Request Accepted Successfully";
            }
            catch (Exception e)
            {
                response.Status = System.Net.HttpStatusCode.InternalServerError;
                Console.WriteLine($"--> unable to get accept friend request {e.Message}");
            }
            return response;
        }

        public GenericResponse AddFriendRequest(int userId, AddFriendRequestDto addFriendRequest)
        {
            var response = new GenericResponse();
            try
            {
                if (_appDbContext.FriendShipRequests.Where(x => (x.SenderId == userId && x.ReceiverId == addFriendRequest.RequestedUserId) ||
                 (x.SenderId == addFriendRequest.RequestedUserId && x.ReceiverId == userId)
                ).Any())
                {
                    response.Message = "Request already sent to user";
                    response.Status = System.Net.HttpStatusCode.BadRequest;
                }
                _appDbContext.FriendShipRequests.Add(new Domain.Models.FriendShipRequest
                {
                    CreatedDate = DateTime.Now,
                    SenderId = userId,
                    ReceiverId = addFriendRequest.RequestedUserId,
                });
                _appDbContext.SaveChanges();

                response.Message = "Request Send to user";
            }
            catch (Exception e)
            {
                response.Status = System.Net.HttpStatusCode.InternalServerError;
                Console.WriteLine($"--> unable to add friend request {e.Message}");
            }
            return response;
        }

        public IEnumerable<FriendSuggestionDto> GetFriendSuggestions(int userId)
        {
            List<FriendSuggestionDto> friendSuggestions = new List<FriendSuggestionDto>();
            try
            {
                //friendSuggestions = (from friends in _appDbContext.FriendShipRequests.Where(x => (x.SenderId == userId || x.ReceiverId == userId))
                //                     from user in _appDbContext.Users
                //                     where (friends.SenderId != user.Id && friends.ReceiverId != user.Id)
                //                     select new FriendSuggestionDto
                //                     {
                //                         Name = user.FirstName + " " + user.LastName,
                //                         ProfilePicture = user.ProfilePicLocation,
                //                         UserId = user.Id
                //                     }).Where(x => x.UserId != userId).ToList();

                //all users except himself

                var requestsSends = _appDbContext.FriendShipRequests.Where(x => x.SenderId == userId).Select(x => x.ReceiverId);
                var requestsReceived = _appDbContext.FriendShipRequests.Where(x => x.ReceiverId == userId).Select(x => x.SenderId);


                friendSuggestions = _appDbContext.Users.
                    Where(x => !requestsSends.Contains(x.Id) && !requestsReceived.Contains(x.Id)).
                    Where(x => x.Id != userId).Select(x => new FriendSuggestionDto
                    {
                        Name = x.FirstName + ' ' + x.LastName,
                        ProfilePicture = x.ProfilePicLocation,
                        UserId = x.Id
                    }).Take(5).ToList();
            }

            catch (Exception e)
            {
                Console.WriteLine($"--> unable to get friend suggestions {e.Message}");
            }
            return friendSuggestions;
        }

        public IEnumerable<FriendRequesReadtDto> GetPendingFriendRequests(int userId)
        {
            var friendRequests = new List<FriendRequesReadtDto>();

            try
            {
                friendRequests = (from request in _appDbContext.FriendShipRequests.Where(x => x.ReceiverId == userId && x.IsAccepted == false)
                                  from user in _appDbContext.Users
                                  where (request.SenderId == user.Id)
                                  select new FriendRequesReadtDto
                                  {
                                      IsAccepted = request.IsAccepted,
                                      RequestDate = request.CreatedDate,
                                      RequestId = request.Id,
                                      FirstName = user.FirstName,
                                      LastName = user.LastName,
                                      ProfiePicture = user.ProfilePicLocation,
                                      UserId = user.Id
                                  }).ToList();
                
            }
            catch (Exception e)
            {
                Console.WriteLine($"--> unable to get pending friend requests {e.Message}");
            }
            return friendRequests;
        }

        public IEnumerable<UserFriendDto> GetUserFriends(int userId)
        {
            List<UserFriendDto> userfriends = new List<UserFriendDto>();
            try
            {
                userfriends = (from friends in _appDbContext.FriendShipRequests.Where(x => (x.SenderId == userId || x.ReceiverId == userId) && x.IsAccepted == true)
                               from user in _appDbContext.Users
                               where (friends.SenderId == user.Id || friends.ReceiverId == user.Id)
                               select new UserFriendDto
                               {
                                   Name = user.FirstName + " " + user.LastName,
                                   ProfilePicture = user.ProfilePicLocation,
                                   UserId = user.Id
                               }).Where(x => x.UserId != userId).ToList();


            }

            catch (Exception e)
            {
                Console.WriteLine($"--> unable to get friend suggestions {e.Message}");
            }
            return userfriends;
        }
    }
}
