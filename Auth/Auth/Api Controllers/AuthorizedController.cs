using Auth.Utils.CustomErrorHandling;
using Auth.Utils.JWT;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Api_Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizedController : ControllerBase
    {
        private readonly IJWTToken _jWTToken;
        private readonly IConfiguration _config;

        public AuthorizedController(IJWTToken jWTToken, IConfiguration config)
        {
            _jWTToken = jWTToken;
            _config = config;
        }
        //[Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                if (String.IsNullOrEmpty(Request.Headers["Authorization"]))
                {
                    throw new HttpStatusCodeException(System.Net.HttpStatusCode.BadRequest, "Please provide an Auth Token");
                }
                string username = _jWTToken.ExtractUsernameFromToken(Request.Headers["Authorization"], _config["Jwt:Key"], _config["Jwt:Issuer"], _config["Jwt:Audience"]);
                return Ok(new
                {
                    message = $"This is an authorized route. Welcome to the site, '{username}'"
                });
            }
            catch (HttpStatusCodeException ex)
            {
                return StatusCode((int)ex.StatusCode, new { error = ex.Message });
            }
        }
    }
}
