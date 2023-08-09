using Auth.Models;
using Auth.Repository;
using Auth.Utils.CustomErrorHandling;
using Auth.Utils.PasswordHashing;
using Auth.ViewModel;

namespace Auth.Services
{
    public class UserService : IUserService
    {
        private readonly IRepo _repo;
        private readonly IPasswordHash _passwordHash;

        public UserService(IRepo repo, IPasswordHash passwordHash)
        {
            _repo = repo;
            _passwordHash = passwordHash;
        }
        public User AddUser(UserViewModel userData)
        {
            if (string.IsNullOrEmpty(userData.UserName) || string.IsNullOrEmpty(userData.UserPassword) || string.IsNullOrEmpty(userData.FullName) || string.IsNullOrEmpty(userData.Gender))
            {
                throw new HttpStatusCodeException(System.Net.HttpStatusCode.BadRequest, "Some fields are still empty");
            }

            if(_repo.GetUserByUserName(userData.UserName) != null)
            {
                throw new HttpStatusCodeException(System.Net.HttpStatusCode.Conflict, $"User '{userData.UserName}' already exists.");
            }

            User user = new()
            {
                UserName = userData.UserName,
                UserPassword = _passwordHash.HashedPassword(userData.UserPassword),
                FullName = userData.FullName,
                Gender = userData.Gender,
            };

            return _repo.AddUser(user);
        }

        public User GetUserById(int id)
        {
            User user = _repo.GetUserById(id);
            
            if(user == null)
            {
                throw new HttpStatusCodeException(System.Net.HttpStatusCode.NotFound, "User not found");
            }

            return user;
        }

        public List<User> GetUsers()
        {
            return _repo.GetAllUsers();
        }

        public User GetUserByUserName(string username)
        {
            User user = _repo.GetUserByUserName(username);

            if (user == null)
            {
                throw new HttpStatusCodeException(System.Net.HttpStatusCode.NotFound, "User not found");
            }

            return user;
        }
    }
}
