using Auth.Models;
using Auth.Services;
using Auth.Utils.CustomErrorHandling;
using Auth.Utils.JWT;
using Auth.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace Auth.Api_Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly IConfiguration _config;
        private readonly IJWTToken _jWTToken;

        public LoginController(ILoginService loginService, IConfiguration configuration, IJWTToken jWTToken)
        {
            _loginService = loginService;
            _config = configuration;
            _jWTToken = jWTToken;
        }
        [HttpPost]
        public IActionResult Post(LoginDTO Credentials)
        {
            try
            {
                if (!_loginService.Login(Credentials))
                {
                    throw new HttpStatusCodeException(System.Net.HttpStatusCode.BadRequest, "Unable to login");
                }

                string loginToken = _jWTToken.GenerateToken(Credentials.username, _config["Jwt:Key"], _config["Jwt:Issuer"], _config["Jwt:Audience"], Convert.ToInt32(_config["Jwt:ExpiryTime"]));
                return Ok(new {message = "Login Successfull", token = loginToken});
            }
            catch (HttpStatusCodeException ex)
            {
                return StatusCode((int)ex.StatusCode, new { error = ex.Message });
            }
        }
    }
}
