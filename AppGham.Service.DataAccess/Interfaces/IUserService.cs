using AppGham.Services.Tables;
using AppGham.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppGham.Services.Interfaces
{
    public interface IUserService
    {
        Task<IList<IUser>> GetUsersAsync();
        Task<IUser> GetUserAsync(int id);
        Task<int> DeleteUserAsync(int id);
        Task<int> AddUserAsync(IUser user);
    }
}