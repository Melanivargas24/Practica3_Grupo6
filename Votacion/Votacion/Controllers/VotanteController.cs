using Microsoft.AspNetCore.Mvc;
using VotacionBLL.Services.Votante;
using VotacionObjetos.ViewModels;

namespace Votacion.Controllers
{
    public class VotanteController : Controller
    {
        private readonly IVotanteService _votanteService;

        public VotanteController(IVotanteService votanteService)
        {
            _votanteService = votanteService;
        }

        // GET: Votante
        public async Task<IActionResult> Index()
        {
            var votantes = await _votanteService.ObtenerTodosAsync();
            return View(votantes);
        }

        // GET: Votante/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var votante = await _votanteService.ObtenerPorCedulaAsync(id);
            if (votante == null)
            {
                return NotFound();
            }

            return View(votante);
        }

        // GET: Votante/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Votante/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Cedula,Nombre,Apellido,FechaNacimiento,SiVoto")] VotanteViewModel votante)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _votanteService.CrearAsync(votante);
                    TempData["Mensaje"] = "Votante agregado correctamente";
                    return RedirectToAction(nameof(Index));
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError("Cedula", "Ya existe un votante con esa cedula");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error al crear votante: " + ex.Message);
                }
            }
            return View(votante);
        }

        // GET: Votante/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var votante = await _votanteService.ObtenerPorCedulaAsync(id);
            if (votante == null)
            {
                return NotFound();
            }
            return View(votante);
        }

        // POST: Votante/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Cedula,Nombre,Apellido,FechaNacimiento,SiVoto")] VotanteViewModel votante)
        {
            if (id != votante.Cedula)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var resultado = await _votanteService.ActualizarAsync(votante);
                if (resultado == null)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(votante);
        }

        // GET: Votante/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var votante = await _votanteService.ObtenerPorCedulaAsync(id);
            if (votante == null)
            {
                return NotFound();
            }

            return View(votante);
        }

        // POST: Votante/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var resultado = await _votanteService.EliminarAsync(id);
            if (!resultado)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }
    }
} 