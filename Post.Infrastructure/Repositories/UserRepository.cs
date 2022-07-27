using Post.Application.Contracts.Repositories;
using Post.Domain.Entities;
using Post.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<Domain.Entities.User>, IUserRepository
    {
        private readonly AppDbContext _dbContext;
        public UserRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public bool SyncMissingUsers(IEnumerable<User> users)
        {
            try
            {
                foreach (var user in users)
                {
                    //if user not found on post database then add them
                    if (!_dbContext.Users.Where(x => x.ExternalId == user.ExternalId).Any())
                    {
                        _dbContext.Users.Add(new User
                        {
                            ExternalId = user.ExternalId,
                            CreatedDate = DateTime.Now,
                            Name = user.Name,
                            ProfilePicLocation = user.ProfilePicLocation,
                        });
                    }
                }
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {

                Console.WriteLine($"--> Unable to sync missing users {e.Message}");
                return false;
            }

        }
    }
}
