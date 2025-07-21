using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using VotacionObjetos.ViewModels;

namespace VotacionMVC.Controllers
{
    public class PartidoController : Controller
    {
        private readonly HttpClient _httpClient;

        public PartidoController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("API");
        }


        // GET: Partido
        public async Task<IActionResult> Index()
        {
            var partidos = await _httpClient.GetFromJsonAsync<List<PartidoViewModel>>("partido");
            return View(partidos);
        }

        // GET: Partido/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var partido = await _httpClient.GetFromJsonAsync<PartidoViewModel>($"partido/{id}");
            if (partido == null) return NotFound();
            return View(partido);
        }

        // GET: Partido/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Partido/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PartidoViewModel partido)
        {
            if (!ModelState.IsValid) return View(partido);

            var response = await _httpClient.PostAsJsonAsync("partido", partido);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, "Error al crear el partido");
            return View(partido);
        }

        // GET: Partido/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var partido = await _httpClient.GetFromJsonAsync<PartidoViewModel>($"partido/{id}");
            if (partido == null) return NotFound();
            return View(partido);
        }

        // POST: Partido/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PartidoViewModel partido)
        {
            if (id != partido.Id) return BadRequest();

            if (!ModelState.IsValid) return View(partido);

            var response = await _httpClient.PutAsJsonAsync($"partido/{id}", partido);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, "Error al actualizar el partido");
            return View(partido);
        }

        // GET: Partido/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var partido = await _httpClient.GetFromJsonAsync<PartidoViewModel>($"partido/{id}");
            if (partido == null) return NotFound();
            return View(partido);
        }

        // POST: Partido/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"partido/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, "Error al eliminar el partido");
            // Para mostrar el detalle nuevamente si falla la eliminación
            var partido = await _httpClient.GetFromJsonAsync<PartidoViewModel>($"partido/{id}");
            return View("Delete", partido);
        }
    }
}
