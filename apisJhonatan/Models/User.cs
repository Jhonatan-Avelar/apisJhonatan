using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apisJhonatan.Models
{
    // model user caso seja a autenticacao fique com usuarios inseridos no banco
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
