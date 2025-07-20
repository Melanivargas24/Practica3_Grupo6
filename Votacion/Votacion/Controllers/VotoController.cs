using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using VotacionObjetos.ViewModelos;
using VotacionObjetos.ViewModels;

namespace VotacionMVC.Controllers
{
    public class VotoController : Controller
    {
        private readonly HttpClient _httpClient;

        public VotoController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("API");
        }

        // GET: Voto - Formulario de votación
        public async Task<IActionResult> Index()
        {
            var partidos = await _httpClient.GetFromJsonAsync<List<PartidoViewModel>>("partido");
            ViewBag.Partidos = partidos ?? new List<PartidoViewModel>();
            return View();
        }

        // POST: Voto - Procesar votación
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string cedulaVotante, int partidoId)
        {
            if (string.IsNullOrEmpty(cedulaVotante) || partidoId == 0)
            {
                ModelState.AddModelError(string.Empty, "Debe ingresar la cédula y seleccionar un partido");
                var partidos = await _httpClient.GetFromJsonAsync<List<PartidoViewModel>>("partido");
                ViewBag.Partidos = partidos ?? new List<PartidoViewModel>();
                return View();
            }

            try
            {
                var votanteResponse = await _httpClient.GetAsync($"votante/cedula/{cedulaVotante}");
                if (!votanteResponse.IsSuccessStatusCode)
                {
                    ModelState.AddModelError(string.Empty, "La cédula ingresada no está registrada en el sistema");
                    var partidos = await _httpClient.GetFromJsonAsync<List<PartidoViewModel>>("partido");
                    ViewBag.Partidos = partidos ?? new List<PartidoViewModel>();
                    return View();
                }

                var votoExistenteResponse = await _httpClient.GetAsync($"voto/cedula/{cedulaVotante}");
                if (votoExistenteResponse.IsSuccessStatusCode)
                {
                    ModelState.AddModelError(string.Empty, "Esta cédula ya ha votado");
                    var partidos = await _httpClient.GetFromJsonAsync<List<PartidoViewModel>>("partido");
                    ViewBag.Partidos = partidos ?? new List<PartidoViewModel>();
                    return View();
                }

                var votante = await votanteResponse.Content.ReadFromJsonAsync<VotanteViewModel>();
                var partido = await _httpClient.GetFromJsonAsync<PartidoViewModel>($"partido/{partidoId}");

                var voto = new VotoViewModel
                {
                    CedulaVotante = cedulaVotante,
                    PartidoId = partidoId,
                    Fecha = DateTime.Now,
                    NombreVotante = votante?.Nombre ?? "",
                    NombrePartido = partido?.Nombre ?? ""
                };

                var response = await _httpClient.PostAsJsonAsync("voto", voto);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Confirmacion");
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, $"Error al registrar el voto: {errorContent}");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error del sistema: " + ex.Message);
            }

            var partidosError = await _httpClient.GetFromJsonAsync<List<PartidoViewModel>>("partido");
            ViewBag.Partidos = partidosError ?? new List<PartidoViewModel>();
            return View();
        }

        // GET: Voto/Confirmacion
        public IActionResult Confirmacion()
        {
            return View();
        }

        // GET: Voto/Resultados
        public async Task<IActionResult> Resultados()
        {
            try
            {
                var votos = await _httpClient.GetFromJsonAsync<List<VotoViewModel>>("voto");
                var partidos = await _httpClient.GetFromJsonAsync<List<PartidoViewModel>>("partido");

                var resultados = partidos.Select(p => new ResultadoViewModel
                {
                    NombrePartido = p.Nombre,
                    CantidadVotos = votos?.Count(v => v.PartidoId == p.Id) ?? 0
                }).ToList();

                return View(resultados);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al cargar los resultados: " + ex.Message;
                return View(new List<ResultadoViewModel>());
            }
        }
    }
}
