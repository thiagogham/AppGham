using AppGham.Services.Interfaces;
using AppGham.Services.Tables;
using AppGham.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppGham.Services
{
    public class UserService : BaseService, IUserService
    {
        public async Task<int> AddUserAsync(IUser user)
        {
            await Init();
            return await _service.InsertAsync(user);
        }

        public async Task<int> DeleteUserAsync(int id)
        {
            await Init();
            return await _service.DeleteAsync<IUser>(id);
        }

        public async Task<IUser> GetUserAsync(int id)
        {
            await Init();
            return await _service.GetAsync<User>(id);
        }

        public async Task<IList<IUser>> GetUsersAsync()
        {
            await Init();
            return await _service.Table<User>().ToArrayAsync();
        }
    }
}
