using AppGham.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppGham.Services.Interfaces
{
    public interface IUserService
    {
        Task<int> AddUserAsync(IUser user);
        Task<int> DeleteUserAsync(int id);
        Task<IList<IUser>> GetUsersAsync();
        Task<IUser> GetUserAsync(int id);
        Task<IUser> GetUserAsync(string email);
        Task<bool> LoginUserAsync(string email, string password);
        Task<int> UpdateUserAsync(IUser user);
    }
}