using idenitywebapiauthenitcation.Model;

namespace idenitywebapiauthenitcation.Services
{
    public interface IUserService
    {
        Task<List<UserModel>> GetAllUsers();

        Task<UserModel> GetUserByEmail(string emailId);

        Task<bool> UpdateUser(string emailId, UserModel user);

        Task<bool> DeleteUserByEmail(string emailId);
    }
}
