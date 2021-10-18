using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using apisJhonatan.Models;
using apisJhonatan.Settings;

namespace apisJhonatan.Controllers
{
    // classe responsavel pelo fluxo de validacao do usuario, criacao do token e validacao do token
    public class AuthController : Controller
    {
        private static readonly HttpClient client = new HttpClient();

        public static string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.Settings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        // endpoint de autenticacao
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] dynamic dados)
        {
            JObject objeto = JObject.Parse(dados.ToString());
            var username = objeto.SelectToken("username").ToString();
            var password = objeto.SelectToken("password").ToString();

            // Verifica se o usuário existe
            var user = UserRepo.Get(username, password);
            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });
            else
            {
                var token = GenerateToken(user);

                user.Password = "";

                return new
                {
                    user = user,
                    token = token
                };
            }
        }
    }
}
