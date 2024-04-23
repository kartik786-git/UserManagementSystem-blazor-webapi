using idenitywebapiauthenitcation.Model;
using idenitywebapiauthenitcation.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace idenitywebapiauthenitcation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [Authorize(Roles = "admin")]
        [HttpGet("GetRoles")]
        public async Task<IActionResult> GetRoles()
        {

            var list = await _roleService.GetRolesAsync();
            return Ok(list);
        }

        [Authorize]
        [HttpGet("GetUserRole")]
        public async Task<IActionResult> GetuserRole(string userEmail)
        {
            var userClaims = await _roleService.GetUserRolesAsync(userEmail);
            return Ok(userClaims);

        }

        [Authorize(Roles = "admin")]
        [HttpPost("addRoles")]
        public async Task<ActionResult> AddRole(string[] roles)
        {
            var userrole = await _roleService.AddRolesAsync(roles);
            if (userrole.Count == 0)
            {
                return BadRequest();
            }
            return Ok(userrole);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("addUserRoles")]
        public async Task<ActionResult> AddUserRole([FromBody] AddUserModel addUser)
        {
            var result = await _roleService.AddUserRoleAsync(addUser.UserEmail, addUser.Roles);

            if (!result)
            {
                return BadRequest();
            }

            return StatusCode((int)HttpStatusCode.Created, result);
        }

    }
}
