using Auth.Models;
using Auth.Utils.CustomErrorHandling;

namespace Auth.Repository
{
    public class Repo : IRepo
    {
        private readonly loginContext _dbContext;

        public Repo(loginContext dbContext)
        {
            _dbContext = dbContext;
        }
        public User AddUser(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
            return user;
        }

        public List<User> GetAllUsers()
        {
            return _dbContext.Users.ToList();
        }

        public User GetUserById(int id)
        {
            User? user = _dbContext.Users.FirstOrDefault(x=> x.UserId == id);
            if(user == null)
            {
                throw new HttpStatusCodeException(System.Net.HttpStatusCode.NotFound, "User not found");
            }
            return user;
        }

        public User GetUserByUserName(string username)
        {
            User? user = _dbContext.Users.FirstOrDefault(x=> x.UserName == username);

            if(user == null)
            {
                throw new HttpStatusCodeException(System.Net.HttpStatusCode.NotFound, "User not found");
            }
            return user;
        }
    }
}
