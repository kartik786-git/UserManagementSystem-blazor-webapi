using idenitywebapiauthenitcation.Model;
using idenitywebapiauthenitcation.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace idenitywebapiauthenitcation.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IUserService _userService;

        public UserController(SignInManager<IdentityUser> signInManager, IUserService userService)
        {
            _signInManager = signInManager;
            _userService = userService;
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userList = await _userService.GetAllUsers();
            return Ok(userList);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("{emailId}")]
        public async Task<IActionResult> Get(string emailId)
        {
            var userList = await _userService.GetUserByEmail(emailId);
            return Ok(userList);
        }

        [Authorize(Roles = "admin")]
        [HttpPut("{emailId}")]
        public async Task<IActionResult> UpdateUser(string emailId, [FromBody] UserModel userModel)
        {
            var result = await _userService.UpdateUser(emailId, userModel);
            if (!result)
            {
                return BadRequest();
            }
            return NoContent();
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{emailId}")]
        public async Task<IActionResult> Delete(string emailId)
        {
            var result = await _userService.DeleteUserByEmail(emailId);
            if (!result)
            {
                return BadRequest();
            }
            return NoContent();
        }
        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] object empty)
        {
            //{}
            if (empty is not null)
            {
                await _signInManager.SignOutAsync();
            }
            return Ok();
        }

    }
}
