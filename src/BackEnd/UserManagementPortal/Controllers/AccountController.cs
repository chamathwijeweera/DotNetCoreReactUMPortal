using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserManagementPortal.Data;
using UserManagementPortal.Infastructure;
using UserManagementPortal.Modals;

namespace UserManagementPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IJWTAuthManager _jWTAuthManager;

        public AccountController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
                                 ApplicationDbContext context, IConfiguration configuration, IJWTAuthManager jWTAuthManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _configuration = configuration;
            _jWTAuthManager = jWTAuthManager;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
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
            var userExists = await _userManager.FindByEmailAsync(model.Email);
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
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again.", Errors = result.Errors.ToList() });

            await CreateRoles();

            await _userManager.AddToRoleAsync(user, GetUserRole(model.UserRole));

            //need to refactor

            //return Ok(new Response { Status = "Success", Message = "User created successfully!" });
            var userObj = await _userManager.FindByEmailAsync(model.Email);
            if (userObj != null && await _userManager.CheckPasswordAsync(userObj, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(userObj);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userObj.UserName),
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

        [HttpGet]
        [Route("users")]
        public IActionResult GetUsers()
        {
            var users = _userManager.Users.Select(e => new { Name = e.UserName, e.Email }).ToList();
            return Ok(new { results = users });
        }

        [HttpGet]
        [Authorize]
        [Route("authuser")]
        public async Task<IActionResult> GetUser()
        {
            var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
            var userRoles = await _userManager.GetRolesAsync(currentUser);
            var modulePermissions = _context.UserModulePermissions.Where(e => e.UserId == currentUser.Id)
                                    .Select(e=> new UserPermission() 
                                    { 
                                        ModuleId= e.ModuleId,
                                        OperationId=  e.OperationId 
                                    }).ToList();

            var userObj = new User() { Email = currentUser.Email, Name = currentUser.UserName, UserRoles = userRoles.ToList(),UserPermission = modulePermissions };
            return Ok(userObj);
        }      

        private async Task<bool> CreateRoles()
        {
            if (!await _roleManager.RoleExistsAsync(UserRoles.Administrator))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Administrator));

            if (!await _roleManager.RoleExistsAsync(UserRoles.Manager))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Manager));

            if (!await _roleManager.RoleExistsAsync(UserRoles.Developer))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Developer));

            if (!await _roleManager.RoleExistsAsync(UserRoles.Customer))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Customer));

            return true;
        }

        public string GetUserRole(string role)
        {
            if (role == "Administrator")
            {
                return UserRoles.Administrator;
            }
            else if (role == "Manager")
            {
                return UserRoles.Manager;
            }
            else if (role == "Developer")
            {
                return UserRoles.Developer;
            }
            else if (role == "Customer")
            {
                return UserRoles.Customer;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
