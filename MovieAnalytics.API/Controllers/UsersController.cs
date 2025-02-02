using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieAnalytics.API.Entities;

namespace MovieAnalytics.API.Controllers;


public class UsersController(UserManager<AppUser> userManager) : BaseApiController
{
    // Useses MemberDto
    // GetUsers, GetUser, UpdateUser,
    // Later: AddPhoto, SetMainPhoto, DeletePhoto
    // UsersController  ->
    
    [HttpGet("test")]
    public IActionResult Test()
    {
        return Ok("Route is working!");
    }
    
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
        if (User.Identity?.IsAuthenticated == false)
        {
            return Unauthorized("User is not authenticated.");
        }


        var users = await userManager.Users.ToListAsync();

        return Ok(users);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<AppUser>> GetUserById(string id)
    {
        var user = await userManager.FindByIdAsync(id);
        if (user == null) return NotFound();
        return Ok(user);
    }
}