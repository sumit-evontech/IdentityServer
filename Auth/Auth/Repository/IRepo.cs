using Auth.Models;

namespace Auth.Repository
{
    public interface IRepo
    {
        public List<User> GetAllUsers();
        public User GetUserById(int id);
        public User GetUserByUserName(string username);
        public User AddUser(User user);
    }
}
