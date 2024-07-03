using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyRecipes.Data;
using MyRecipes.Models;
using MyRecipes.ViewModels;
using Newtonsoft.Json;

namespace MyRecipes.Controllers
{
    [Authorize(Roles = "Admin, Manager")]
    public class ManagementController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ManagementController(AppDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Users()
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var currentUserRoles = await _userManager.GetRolesAsync(currentUser);
            ViewBag.currentUserRoles = currentUserRoles;
            var allRoles = await _roleManager.Roles.ToListAsync();
            ViewBag.Roles = allRoles;

            var users = await _userManager.Users.ToListAsync();

            var userList = new List<User>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
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
               

                var addResult = await _userManager.AddToRoleAsync(user, "User");
                if (!addResult.Succeeded)
                {
                    return BadRequest("Failed to add User role.");
                }

                return RedirectToAction(nameof(Users));
            }

            return BadRequest("User not found.");
        }
        [Authorize(Roles = "Admin")]

        [HttpPost]
        public async Task<IActionResult> ModifyUserRoles()
        {
            var id = Request.Form["UserId"];
            var rolesJson = Request.Form["roles"];
            var roles = JsonConvert.DeserializeObject<List<string>>(rolesJson);
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var currentRoles = await _userManager.GetRolesAsync(user);

                var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
                if (!removeResult.Succeeded)
                {
                    return BadRequest("Failed to remove user roles.");
                }
                var addResult = await _userManager.AddToRolesAsync(user, roles);
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
