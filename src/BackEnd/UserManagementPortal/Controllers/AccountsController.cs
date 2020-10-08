using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserManagementPortal.Infastructure;
using UserManagementPortal.Modals;

namespace UserManagementPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IJWTAuthManager _jWTAuthManager;

        public AccountsController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IJWTAuthManager jWTAuthManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _jWTAuthManager = jWTAuthManager;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                return Ok(_jWTAuthManager.GenerateTokens(authClaims));
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            await CreateRoles();

            await _userManager.AddToRoleAsync(user, GetUserRole(model.UserRole));

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        private async Task<bool> CreateRoles()
        {
            try
            {
                if (!await _roleManager.RoleExistsAsync(UserRoles.Administrator))
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.Administrator));
                if (!await _roleManager.RoleExistsAsync(UserRoles.StandardUser))
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.StandardUser));

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string GetUserRole (string role)
        {
            if(role == "admin")
            {
                return UserRoles.Administrator;
            }
            else if(role == "suser")
            {
                return UserRoles.StandardUser;
            }
            else
            {
                return UserRoles.StandardUser;
            }
        }
    }
}
