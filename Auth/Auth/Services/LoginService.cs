using Auth.Models;
using Auth.Repository;
using Auth.Utils.CustomErrorHandling;
using Auth.Utils.JWT;
using Auth.Utils.PasswordHashing;
using Auth.ViewModel;

namespace Auth.Services
{
    public class LoginService : ILoginService
    {
        private readonly IRepo _repo;
        private readonly IJWTToken _jwt;
        private readonly IPasswordHash _passwordHash;

        public LoginService(IRepo repo, IJWTToken jwt, IPasswordHash passwordHash)
        {
            _repo = repo;
            _jwt = jwt;
            _passwordHash = passwordHash;
        }
        public bool Login(LoginDTO Credentials)
        {
            if (string.IsNullOrEmpty(Credentials.username) || string.IsNullOrEmpty(Credentials.password))
            {
                throw new HttpStatusCodeException(System.Net.HttpStatusCode.BadRequest, "Please enter username and password");
            }

            User? user = _repo.GetUserByUserName(Credentials.username);

            if (user == null)
            {
                throw new HttpStatusCodeException(System.Net.HttpStatusCode.NotFound, "User not exists");
            }

            if(!_passwordHash.ComparePassword(user.UserPassword, Credentials.password))
            {
                throw new HttpStatusCodeException(System.Net.HttpStatusCode.Unauthorized, "Wrong Password");
            }

            return true;
        }
    }
}
