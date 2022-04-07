using Microsoft.AspNetCore.Mvc;

namespace LIVESTOCK.Areas.website.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}