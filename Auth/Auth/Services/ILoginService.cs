using Auth.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Services
{
    public interface ILoginService
    {
        public bool Login(LoginDTO Credentials);
    }
}
