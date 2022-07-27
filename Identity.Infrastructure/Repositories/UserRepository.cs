using AutoMapper;
using Identity.Infrastructure.Data;
using Identity.Application.Dtos.UserManagement;
using Identity.Application.Contracts.Repositories;
using Identity.Domain.Models;

namespace Identity.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserRepository(Infrastructure.Data.AppDbContext dbContext, IMapper mapper) : base(dbContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public void AddUser(User user)
        {
            try
            {
                user.CreatedDate = DateTime.Now;
                _dbContext.Users.Add(user);
            }
            catch (Exception e)
            {

            }
        }

        public User Authenticate(string email, string password)
        {
            var user = _dbContext.Users.Where(x => x.Email == email && x.Password == password).FirstOrDefault();
            if (user != null)
            {
                return user;
            }
            else
                return null;
        }

        public IEnumerable<User> GetAll()
        {
            return _dbContext.Users.ToList();
        }

        public bool IsUserExist(string email)
        {
            return _dbContext.Users.Any(x => x.Email == email);
        }

        public bool SaveChanges()
        {
            return _dbContext.SaveChanges() > 0;
        }


        public IEnumerable<Application.Dtos.UserManagement.UserReadDto> SearchUser(string query)
        {

            var users = _dbContext.Users.Where(x => x.FirstName.Contains(query)).ToList();

            return _mapper.Map<IEnumerable<Application.Dtos.UserManagement.UserReadDto>>(users);
        }


        public User GetUserById(int id)
        {
            return _dbContext.Users.Where(x => x.Id == id).FirstOrDefault();

        }

    }
}
