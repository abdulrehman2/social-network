using Identity.Application.Contracts.Repositories;
using Identity.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Repositories
{
    public class UserPrivacyRepository: GenericRepository<Domain.Models.UserPrivacy>, IUserPrivacyRepository
    {
        private readonly AppDbContext _appDbContext;

        public UserPrivacyRepository(Data.AppDbContext appDbContext) :base(appDbContext)
        {
            _appDbContext = appDbContext;
        }


    }
}
