using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DrivingSchoolAPIModels;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json.Linq;
using System.Data;

namespace DrivingSchoolWebAPI.Controllers
{
    /// <summary>
    /// Контроллер для неавторизованного пользователя
    /// </summary>
    [Route("api")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public AuthenticateController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }
        /// <summary>
        /// Вход в систему
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(Login))]
        public async Task<ActionResult<Response<LoginResponse>>> Login([FromBody] LoginModel model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                // Если пользователь не найден или пароль не совпадает, то отмена авторизации 
                if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
                    return Unauthorized(new Response
                    {
                        Status = "Login denied",
                        Message = user == null ? "Пользователь не найден" : "Неверный пароль"
                    });
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                // Создать токен
                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(12),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );
                var role = userRoles.First();
                return Ok( new Response<LoginResponse>
                {
                    Message = "OK",
                    Status = "OK",
                    Package = new LoginResponse
                    {
                        Token = new JwtSecurityTokenHandler().WriteToken(token),
                        Expiration = token.ValidTo,
                        Role = role,
                        Id = user.Id
                    }
                });
            }
            catch (Exception e)
            {
                return BadRequest(new Response
                {
                    Status = "Failure",
                    Message = e.Message
                });
            }

        }
        /// <summary>
        /// Проверить, работает ли сервер и подходит ли токен аутентификации.
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("Ping")]
        public ActionResult<Response<bool>> Ping()
        {
            return Ok(new Response<bool> { Status = "Success", Message = "Pong", Package=true });
        }
    }
}
