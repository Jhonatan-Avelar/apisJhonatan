using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apisJhonatan.Models;

namespace apisJhonatan.Controllers
{
    // classe responsavel por criar usuarios
    public class UserRepo
    {
        public static User Get(string username, string password)
        {
            var users = new List<User>();
            users.Add(new User { Id = 1, Username = "usuario", Password = "123456", Role = "manager" });
            return users.Where(x => x.Username.ToLower() == username.ToLower() && x.Password == password).FirstOrDefault();
        }
    }
}
