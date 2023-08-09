using Auth.Models;
using Auth.ViewModel;

namespace Auth.Services
{
    public interface IUserService
    {
        public List<User> GetUsers();
        public User GetUserById(int id);
        public User AddUser(UserViewModel userData);
    }
}
