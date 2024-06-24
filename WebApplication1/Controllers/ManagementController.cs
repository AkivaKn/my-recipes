using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyRecipes.Data;
using MyRecipes.Models;

namespace MyRecipes.Controllers
{

    public class ManagementController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ManagementController(AppDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Users()
        {
            var users = await _userManager.Users.ToListAsync();

            var userList = new List<User>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                Console.WriteLine($"User: {user.UserName}, Roles: {string.Join(", ", roles)}");
                userList.Add(new User
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Roles = roles.ToList()
                });
            }

            return View(userList);
        }
        public async Task<IActionResult> ApproveUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                if (!await _userManager.IsInRoleAsync(user, "UnapprovedUser"))
                {
                    return BadRequest("User is not already approved.");
                }

                var currentRoles = await _userManager.GetRolesAsync(user);

                var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
                if (!removeResult.Succeeded)
                {
                    return BadRequest("Failed to remove user roles.");
                }

                var addResult = await _userManager.AddToRoleAsync(user, "User");
                if (!addResult.Succeeded)
                {
                    return BadRequest("Failed to add User role.");
                }

                return RedirectToAction(nameof(Users));
            }

            return BadRequest("User not found.");
        }
    }

}
