using Identity.Application.Dtos.UserManagement;
using Identity.Domain.Models;

namespace Identity.Application.Contracts.Repositories
{
    public interface IUserRepository:IGenericRepository<User>
    {
        bool SaveChanges();
        bool IsUserExist(string email);
        void AddUser(User user);
        User Authenticate(string email,string password);
        IEnumerable<User> GetAll();
        IEnumerable<Dtos.UserManagement.UserReadDto> SearchUser(string query);

        User GetUserById(int id);
    }
}
