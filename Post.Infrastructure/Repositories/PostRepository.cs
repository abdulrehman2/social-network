using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Post.Application.Contracts.Repositories;
using Post.Application.Contracts.SyncDataServices.Grpc;
using Post.Application.Dtos.Post;
using Post.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Infrastructure.Repositories
{
    public class PostRepository : GenericRepository<Domain.Entities.Post>, IPostRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFriendDataClient _friendDataClient;
        public PostRepository(AppDbContext context, IMapper mapper, IFriendDataClient friendDataClient) : base(context)
        {
            _context = context;
            _mapper = mapper;
            _friendDataClient = friendDataClient;
        }

        public IEnumerable<ReadPostDto> GetMyPosts(int userId)
        {
            //var posts = _context.Posts.Where(x => x.UserId == userId).OrderByDescending(x => x.CreatedDate).ToList();

            var posts = _context.Posts.Join(_context.Users,
                post => post.UserId,
                user => user.ExternalId,
                (post, user) => new ReadPostDto
                {
                    Id = post.Id,
                    CreatedDate = post.CreatedDate,
                    PostCreator = user.Name,
                    PostCreatorId = post.UserId,
                    PostCreatorProfilePicutre = user.ProfilePicLocation,
                    MediaLocation = post.MediaLocation,
                    WrittenText = post.WrittenText,

                }) // selection
                .Where(postAndUser => postAndUser.PostCreatorId == userId).OrderByDescending(x => x.CreatedDate).ToList();



            //var postsToReturn = _mapper.Map<IEnumerable<ReadPostDto>>(posts);

            foreach (var post in posts)
            {
                post.ReactCount = _context.PostReactes.Where(x => x.PostId == post.Id).Count();
                post.CommentCount = _context.PostComments.Where(x => x.PostId == post.Id).Count();
                //if (!string.IsNullOrEmpty(post.MediaLocation))
                //{
                //    post.MediaLocation = "uploads/" + post.MediaLocation;
                //}
            }

            return posts;
        }



        public IEnumerable<ReadPostDto> GetUserWall(int userId)
        {
            var userFriends = _friendDataClient.ReturnAllUserFriends(userId);
            var userFriendsIds = userFriends.Select(x => x.UserId).ToArray();

            var posts = _context.Posts.Join(_context.Users,
          post => post.UserId,
          user => user.ExternalId,
          (post, user) => new ReadPostDto
          {
              Id = post.Id,
              CreatedDate = post.CreatedDate,
              PostCreator = user.Name,
              PostCreatorId = post.UserId,
              PostCreatorProfilePicutre = user.ProfilePicLocation,
              MediaLocation = post.MediaLocation,
              WrittenText = post.WrittenText,

          }) // selection
          .Where(postAndUser => userFriendsIds.Contains(postAndUser.PostCreatorId) || postAndUser.PostCreatorId== userId).OrderByDescending(x => x.CreatedDate).ToList();



            //var postsToReturn = _mapper.Map<IEnumerable<ReadPostDto>>(posts);

            foreach (var post in posts)
            {
                var userReact=_context.PostReactes.Where(x => x.PostId == post.Id && x.IsDeleted == false && x.UserId == userId).FirstOrDefault();
                if (userReact != null)
                {
                    post.IsReacted = true;
                    post.ReactTypeId = userReact.ReactTypeId;
                }
                
                post.ReactCount = _context.PostReactes.Where(x => x.PostId == post.Id && x.IsDeleted==false).Count();
                post.CommentCount = _context.PostComments.Where(x => x.PostId == post.Id).Count();
                //if (!string.IsNullOrEmpty(post.MediaLocation))
                //{
                //    post.MediaLocation = "uploads/" + post.MediaLocation;
                //}
            }

            return posts;
        }

    }
}
