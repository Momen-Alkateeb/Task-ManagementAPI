using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TaskMangmentApi.Models;
using TaskMangmentApi.Models.DTO;

namespace TaskMangmentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
       
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public AccountController(UserManager<ApplicationUser> user , RoleManager<IdentityRole> role)
        {
            userManager = user;
            roleManager = role;
        }
        [HttpPost("Register")]
        [Authorize(Roles ="Admin")]
       
        public async Task <IActionResult> Register([FromForm]RegisterDTO UserDTO)
        {
            string rolename = UserDTO.Status;
            if(!await roleManager.RoleExistsAsync(rolename))
            {
                await roleManager.CreateAsync(new IdentityRole(rolename)); 
            }
            if (ModelState.IsValid)
            {
                ApplicationUser newuser = new ApplicationUser();
                newuser.UserName = UserDTO.Name;
                newuser.Email = UserDTO.Email;
               IdentityResult result=  await userManager.CreateAsync(newuser , UserDTO.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newuser, rolename);
                    return Created("","Create Suceefuly");
                }
                else
                {
                    foreach(var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }

            }

            return BadRequest(ModelState);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            if (ModelState.IsValid)
            {
                var User = await userManager.FindByNameAsync(dto.UserNameOrEmail);

                if (User == null)
                {
                    User = await userManager.FindByEmailAsync(dto.UserNameOrEmail);
                }
                if (User != null)
                {
                    bool find = await userManager.CheckPasswordAsync(User, dto.Password);
                    if (find)
                    {
                        List<Claim> Claims = new List<Claim>();
                        Claims.Add(new Claim(ClaimTypes.NameIdentifier, User.Id));
                        Claims.Add(new Claim(ClaimTypes.Name, User.UserName!));
                        var Roles = await userManager.GetRolesAsync(User);
                        foreach (var item in Roles)
                        {
                            Claims.Add(new Claim(ClaimTypes.Role, item));
                        }
                        Claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                        SymmetricSecurityKey Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("daskfksaij@@@dsakifkafkm#l;dksaflas;lk!!;lakflksfdasfas"));

                        SigningCredentials sginin = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

                        JwtSecurityToken mytoken = new JwtSecurityToken
                       (
                       issuer: "http://localhost:5157/",
                       expires: DateTime.Now.AddHours(5),
                       claims: Claims,
                       signingCredentials: sginin
                       );
                        var Token = new JwtSecurityTokenHandler().WriteToken(mytoken);
                        return Ok(Token);

                    }

                }


                return BadRequest("The User Name Or Passowrd Is not Corecct");

            }
            return BadRequest(ModelState);
        }
        
           
    }
}
