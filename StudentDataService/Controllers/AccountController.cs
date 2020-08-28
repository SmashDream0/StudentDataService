using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StudentDataService.Attributes;
using StudentDataService.Config;
using StudentDataService.Contracts.Request;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StudentDataService.Controllers
{
    [Route("api/[controller]")]
    [ValidateModel]
    public class AccountController : Controller
    {
        // тестовые данные вместо использования базы данных
        public AccountController(IOptions<AuthConfig> config, Entity.Repository.User.IUserRepository userRepository)
        {
            _config = config.Value;
            _userRepository = userRepository;

            //userRepository.Add(new Entity.POCO.User() { Login = "admin", Password = "123", Role = "admin" });
            //userRepository.Add(new Entity.POCO.User() { Login = "selector", Password = "123", Role = "user" });

            //userRepository.SaveChanges();
        }

        private readonly Entity.Repository.User.IUserRepository _userRepository;
        private readonly AuthConfig _config;

        [HttpGet(nameof(Token))]
        [AllowAnonymous]
        public IActionResult Token([FromQuery] AuthRequest authRequest)
        {
            var user = GetUser(authRequest.Login, authRequest.Password);
            if (user == null)
            { return BadRequest(new { errorText = "Invalid username or password." }); }

            var response = GenerateJwtToken(user);

            return Json(response);
        }

        private string GenerateJwtToken(Entity.POCO.User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, user.Key.ToString()), new Claim(ClaimTypes.Role, user.Role) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private Entity.POCO.User GetUser(string login, string password)
        {
            var user = _userRepository.FindByLogin(login);

            if (!String.Equals(user.Password, password))
            { user = null; }

            return user;
        }
    }
}
