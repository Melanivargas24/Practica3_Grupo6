using Microsoft.AspNetCore.Mvc;

namespace API_Votacion.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
