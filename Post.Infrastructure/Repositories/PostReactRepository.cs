using Post.Application.Contracts.Repositories;
using Post.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Infrastructure.Repositories
{
    public class PostReactRepository : GenericRepository<Domain.Entities.PostReact>, IPostReactRepository
    {
        private readonly AppDbContext _appDbContext;
        public PostReactRepository(AppDbContext context) : base(context)
        {
            _appDbContext = context;
        }

        public bool AddReactToPost(int userId, Application.Dtos.React.AddReactDto addReact)
        {
            var currentReact = _appDbContext.PostReactes.Where(x => x.PostId == addReact.PostId && x.UserId == userId).FirstOrDefault();

            if (currentReact == null)
            {
                _appDbContext.PostReactes.Add(new Domain.Entities.PostReact
                {
                    CreatedBy = userId.ToString(),
                    CreatedDate = DateTime.Now,
                    PostId = addReact.PostId,
                    ReactTypeId = addReact.ReactTypeId,
                    UserId = userId,
                    IsDeleted = false
                });
            }
            else
            {
                //delete case
                if (addReact.ReactTypeId == 0)
                {
                    currentReact.DeleteDate = DateTime.Now;
                    currentReact.IsDeleted = true;
                }
                else
                {
                    currentReact.ReactTypeId = addReact.ReactTypeId;
                    currentReact.LastModifiedBy = userId.ToString();
                    currentReact.LastModifiedDate = DateTime.Now;
                    currentReact.IsDeleted = false;
                    currentReact.DeleteDate = null;
                }
                _appDbContext.PostReactes.Update(currentReact);
            }
            return true;
        }
    }
}
