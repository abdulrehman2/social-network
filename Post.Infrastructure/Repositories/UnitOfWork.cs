using AutoMapper;
using Post.Application.Contracts.Repositories;
using Post.Application.Contracts.SyncDataServices.Grpc;
using Post.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Infrastructure.Repositories
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly AppDbContext _appDbContext;
        public IPostCommentRepository PostComments { get; private set; }
        public IPostRepository Posts { get; private set; }
        public IPostReactRepository PostReacts { get; private set; }
        public IUserRepository Users { get; private set; }

        private readonly IFriendDataClient _friendDataClient;

        public UnitOfWork(AppDbContext appDbContext, IMapper mapper, IFriendDataClient friendDataClient)
        {
            _appDbContext = appDbContext;
            _friendDataClient = friendDataClient;
            PostComments = new PostCommentRepository(_appDbContext);
            PostReacts = new PostReactRepository(_appDbContext);
            Posts = new PostRepository(_appDbContext, mapper, _friendDataClient);
            Users=new UserRepository(_appDbContext);
        }

       

        public int Complete()
        {
            return _appDbContext.SaveChanges();
        }

        public void Dispose()
        {
            _appDbContext.Dispose();
        }
    }
}
