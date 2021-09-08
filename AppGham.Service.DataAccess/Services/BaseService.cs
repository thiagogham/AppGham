using AppGham.Services.Tables;
using SQLite;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace AppGham.Services
{
    public abstract class BaseService
    {
        protected SQLiteAsyncConnection _service;

        protected async Task Init()
        {
            if (_service != null)
                return;

            string databaseName = Path.Combine(FileSystem.AppDataDirectory, "database.db");
            _service = new SQLiteAsyncConnection(databaseName);
            await CreateTables();
        }

        protected async Task CreateTables()
        {
            await _service.CreateTableAsync<User>();
        }
    }
}
