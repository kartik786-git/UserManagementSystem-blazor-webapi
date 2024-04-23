using idenitywebapiauthenitcation.Model;
using Microsoft.AspNetCore.Identity;

namespace idenitywebapiauthenitcation.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public RoleService(RoleManager<IdentityRole> roleManager, 
            UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<List<RoleModel>> GetRolesAsync()
        {
            var roleList = _roleManager.Roles.Select(x => 
            new RoleModel { Id = Guid.Parse(x.Id), Name = x.Name }).ToList();
            return roleList;
        }

        public async Task<List<string>> GetUserRolesAsync(string emailId)
        {
           var user = await _userManager.FindByEmailAsync(emailId);

           var userRoles = await _userManager.GetRolesAsync(user);
            return userRoles.ToList();
        }
        public async Task<List<string>> AddRolesAsync(string[] roles)
        {
            var rolesList = new List<string>();
            foreach (var role in roles)
            {
               if(!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                    rolesList.Add(role);
                }
            }
            return rolesList;
        }

        public async Task<bool> AddUserRoleAsync(string userEmail, string[] roles)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);

            var exitsRoles = await ExistsRolesAsync(roles);

            if(user != null && exitsRoles.Count == roles.Length)
            {
                var assginRoles = await _userManager.AddToRolesAsync(user, exitsRoles);
                return assginRoles.Succeeded;
            }

            return false;

        }

        private async Task<List<string>> ExistsRolesAsync(string[] roles)
        {
            var rolesList = new List<string>();
            foreach (var role in roles)
            {
                var roleExist = await _roleManager.RoleExistsAsync(role);
                if (roleExist)
                {
                    rolesList.Add(role);
                }
            }
            return rolesList;

        }


        }
}
