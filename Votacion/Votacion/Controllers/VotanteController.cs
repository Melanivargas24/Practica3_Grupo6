using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using VotacionObjetos.ViewModels;

namespace Votacion.Controllers
{
    public class VotanteController : Controller
    {
        private readonly HttpClient _httpClient;

        public VotanteController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("API");
        }

        // GET: Votante
        public async Task<IActionResult> Index()
        {
            try
            {
                var votantes = await _httpClient.GetFromJsonAsync<List<VotanteViewModel>>("votante");
                return View(votantes ?? new List<VotanteViewModel>());
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar los votantes: " + ex.Message;
                return View(new List<VotanteViewModel>());
            }
        }

        // GET: Votante/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var votante = await _httpClient.GetFromJsonAsync<VotanteViewModel>($"votante/cedula/{id}");
                if (votante == null)
                {
                    return NotFound();
                }
                return View(votante);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar el votante: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
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
                    var response = await _httpClient.PostAsJsonAsync("votante", votante);
                    if (response.IsSuccessStatusCode)
                    {
                        TempData["Mensaje"] = "Votante agregado correctamente";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        ModelState.AddModelError("Cedula", $"Error al crear votante: {errorContent}");
                    }
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

            try
            {
                var votante = await _httpClient.GetFromJsonAsync<VotanteViewModel>($"votante/cedula/{id}");
                if (votante == null)
                {
                    return NotFound();
                }
                return View(votante);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar el votante: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
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
                try
                {
                    var response = await _httpClient.PutAsJsonAsync($"votante/{id}", votante);
                    if (response.IsSuccessStatusCode)
                    {
                        TempData["Mensaje"] = "Votante actualizado correctamente";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        ModelState.AddModelError("", $"Error al actualizar votante: {errorContent}");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error al actualizar votante: " + ex.Message);
                }
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

            try
            {
                var votante = await _httpClient.GetFromJsonAsync<VotanteViewModel>($"votante/cedula/{id}");
                if (votante == null)
                {
                    return NotFound();
                }
                return View(votante);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar el votante: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Votante/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"votante/{id}");
                if (response.IsSuccessStatusCode)
                {
                    TempData["Mensaje"] = "Votante eliminado correctamente";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Error"] = "Error al eliminar el votante";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al eliminar el votante: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }
    }
} 