using Microsoft.AspNetCore.Mvc;

namespace MyRecipes.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index","Dishes");
        }
    }
}
