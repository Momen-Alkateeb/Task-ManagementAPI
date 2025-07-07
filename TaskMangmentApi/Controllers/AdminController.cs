using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TaskMangmentApi.Models;
using TaskMangmentApi.Models.Dbinfo;
using TaskMangmentApi.Models.DTO;

namespace TaskMangmentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class AdminController : ControllerBase
    {
        
        private readonly UserManager<ApplicationUser> _userManager;

       
        public AdminController( UserManager<ApplicationUser> userManager)
        {
            
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> DiplayAllUser()
        {
            var Users =  await _userManager.Users.ToListAsync();
            List<DisplayUserDTO>usersDto = new List<DisplayUserDTO>();
            foreach(var item in Users)
            {
                DisplayUserDTO user = new DisplayUserDTO();
                user.UserName = item.UserName;
                user.Email = item.Email;
                usersDto.Add(user);
            }
            return Ok(usersDto);
        }
        [HttpGet("{username:alpha}")]
        public async Task<IActionResult> GetUserByName(string username)
        {
            var User =  await _userManager.FindByNameAsync(username);
            if (User == null)
            {
                return BadRequest("User Not Found!");
            }
            var userDto = new DisplayUserDTO();
            userDto.UserName = User.UserName;
            userDto.Email = User.Email;
            return Ok(userDto);
        }
        [HttpPut("{username:alpha}")]
        public async Task<IActionResult> UpdateUserAsync(string username, RegisterDTO newUser)
        {
            var olduser = await _userManager.FindByNameAsync(username);

            if (olduser == null)
            {
                return BadRequest("User Not Found!");
            }

            olduser.UserName = newUser.Name;
            olduser.Email = newUser.Email;

            var OldRole = await _userManager.GetRolesAsync(olduser);
            await _userManager.RemoveFromRolesAsync(olduser, OldRole);
           IdentityResult result =  await _userManager.AddToRoleAsync(olduser, newUser.Status);
            if (!result.Succeeded)
            {
                return BadRequest("update Failed");
            }

            await _userManager.UpdateAsync(olduser);
            return Ok("Updated successfully");
        }

        [HttpDelete("{username:alpha}")]
        public async Task<IActionResult> DeleteUserAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return BadRequest("User Not Found!");
            }
            await _userManager.DeleteAsync(user);
            return Ok("deleted successfully  ");
        }
        [HttpPatch("{username:alpha}")]
        public async Task<IActionResult> ChangePassword(string username , [FromBody]string newpass)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                return BadRequest("User not Found!");

          IdentityResult RemoveResult =  await _userManager.RemovePasswordAsync(user);
          IdentityResult Result = await _userManager.AddPasswordAsync(user, newpass);
            if (!Result.Succeeded)
            {
                foreach(var item in Result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return BadRequest(ModelState);
            }
            return Ok("Change Password success");

            

        }

       
       

        

    }
}
