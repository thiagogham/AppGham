using SQLite;
using System;

namespace AppGham.Shared.Models
{
    public class User : BaseModel, IUser
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string Name { get; set; }

        [Unique]
        public string Email { get; set; }

        public string Photo { get; set; }
        
        public string Password { get; set; }

        public DateTime Date { get; set; }
    }
}
