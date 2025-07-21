using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using VotacionObjetos.ViewModels;
using VotacionDAL;

namespace Votacion.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly VotacionContext _context;

        public HomeController(ILogger<HomeController> logger, VotacionContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            try
            {
                // Verificar conexión a la base de datos
                var votantesCount = _context.Votantes.Count();
                ViewBag.VotantesCount = votantesCount;
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error de conexion a la base de datos: " + ex.Message;
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
