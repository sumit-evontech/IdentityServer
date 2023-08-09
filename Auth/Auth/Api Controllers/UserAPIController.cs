using Auth.Models;
using Auth.Services;
using Auth.Utils.CustomErrorHandling;
using Auth.ViewModel;
using Microsoft.AspNetCore.Mvc;


namespace Auth.Api_Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAPIController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserAPIController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<User> users = _userService.GetUsers();
                return Ok(users);
            }
            catch (HttpStatusCodeException ex)
            {
                return StatusCode((int)ex.StatusCode, new {error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                User user = _userService.GetUserById(id);
                return Ok(user);
            }
            catch (HttpStatusCodeException ex)
            {
                return StatusCode((int)ex.StatusCode, new { error = ex.Message });
            }

        }

        [HttpPost]
        public IActionResult Post(UserViewModel userData)
        {
            try
            {
                User user = _userService.AddUser(userData);
                return Ok(user);
            }
            catch (HttpStatusCodeException ex)
            {
                return StatusCode((int)ex.StatusCode, new { error = ex.Message });
            }
        }
    }
}
