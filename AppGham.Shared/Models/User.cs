using System;

namespace AppGham.Shared.Models
{
    public class User : BaseModel, IUser
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public string Photo { get; set; }

        public DateTime Date { get; set; }
    }
}
