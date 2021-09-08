using AppGham.Shared;
using SQLite;
using System;

namespace AppGham.Services.Tables
{
    public class User : IUser
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string Name { get; set; }

        [Unique]
        public string Email { get; set; }

        public string Photo { get; set; }

        public DateTime Date { get; set; }
    }
}
