using AppGham.Shared.Models;
using SQLite;
using System;
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
            try
            {
                if (_service != null)
                    return;

                string databaseName = Path.Combine(FileSystem.AppDataDirectory, "database.db");
                _service = new SQLiteAsyncConnection(databaseName);
                await CreateTables();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Init error: {ex.Message}");
            }
        }

        protected async Task CreateTables()
        {
            await _service.CreateTableAsync<User>();
        }
    }
}
