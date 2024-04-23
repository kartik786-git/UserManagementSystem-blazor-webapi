using idenitywebapiauthenitcation.Model;
using Microsoft.AspNetCore.Identity;

namespace idenitywebapiauthenitcation.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IRoleService _roleService;

        public UserService(UserManager<IdentityUser> userManager, IRoleService roleService)
        {
            _userManager = userManager;
            _roleService = roleService;
        }

        public async Task<List<UserModel>> GetAllUsers()
        {
            var response = new List<UserModel>();
            var users = _userManager.Users.ToList();
            foreach (var x in users)
            {
                var userRoles = await _userManager.GetRolesAsync(x);
                var user = new UserModel
                {
                    Id = Guid.Parse(x.Id),
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    UserName = x.UserName,
                    Roles = userRoles.ToList(),
                };

                response.Add(user);
            }
            return response;
        }

        public async Task<UserModel> GetUserByEmail(string emailId)
        {
            var user = await _userManager.FindByEmailAsync(emailId);
            var userRoles = await _userManager.GetRolesAsync(user);
            var userModel = new UserModel
            {
                Id = Guid.Parse(user.Id),
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName,
                Roles = userRoles.ToList(),
            };
            return userModel;

        }
        public async Task<bool> DeleteUserByEmail(string emailId)
        {

            var user = await _userManager.FindByEmailAsync(emailId);
            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }



        public async Task<bool> UpdateUser(string emailId, UserModel user)
        {
            //
            // user role - admin,hr
            var userIdentity = await _userManager.FindByEmailAsync(emailId);
            if (userIdentity == null)
            {
                return false;
            }

            userIdentity.UserName = user.UserName;
            userIdentity.Email = user.Email;
            userIdentity.PhoneNumber = user.PhoneNumber;

            var updateReponse = await _userManager.UpdateAsync(userIdentity);
            if (updateReponse.Succeeded)
            {
                // admin,user
                var currentUserRole = await _userManager.GetRolesAsync(userIdentity);
                // user role - admin,hr
                var removeUserRole = currentUserRole.Except(user.Roles);
                // user
                var removeRoleResult = await _userManager.RemoveFromRolesAsync(userIdentity, removeUserRole);
                if (removeRoleResult.Succeeded)
                {
                    // user role - admin,hr
                    // current user -  admin
                    var uniqueRole = user.Roles.Except(currentUserRole);
                    // hr
                    var assginRoleResult = await _roleService.AddUserRoleAsync(userIdentity.Email, uniqueRole.ToArray());
                    return assginRoleResult;
                }
            }
            return false;

        }
    }
}
