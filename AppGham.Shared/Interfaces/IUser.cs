using System;

namespace AppGham.Shared
{
    public interface IUser
    {
        int ID { get; set; }
        string Name { get; set; }
        string Email { get; set; }
        string Photo { get; set; }
        string Password { get; set; }
        DateTime Date { get; set; }
    }
}