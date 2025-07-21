using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using VotacionBLL.Services.Partido;
using VotacionObjetos.ViewModels;

namespace API_Votacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartidoController : ControllerBase
    {
        private readonly IPartidoService _partidoService;

        public PartidoController(IPartidoService partidoService)
        {
            _partidoService = partidoService;
        }

        // GET: api/Partido
        [HttpGet]
        public async Task<ActionResult<List<PartidoViewModel>>> GetTodos()
        {
            var partidos = await _partidoService.ObtenerTodosAsync();
            return Ok(partidos);
        }

        // GET: api/Partido/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PartidoViewModel>> GetPorId(int id)
        {
            var partido = await _partidoService.ObtenerPorIdAsync(id);
            if (partido == null)
                return NotFound();

            return Ok(partido);
        }

        // POST: api/Partido
        [HttpPost]
        public async Task<ActionResult<PartidoViewModel>> Crear(PartidoViewModel partidoVm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var partidoCreado = await _partidoService.CrearAsync(partidoVm);
            return CreatedAtAction(nameof(GetPorId), new { id = partidoCreado.Id }, partidoCreado);
        }

        // PUT: api/Partido/5
        [HttpPut("{id}")]
        public async Task<ActionResult<PartidoViewModel>> Actualizar(int id, PartidoViewModel partidoVm)
        {
            if (id != partidoVm.Id)
                return BadRequest("El id del partido no coincide.");

            var partidoActualizado = await _partidoService.ActualizarAsync(partidoVm);
            if (partidoActualizado == null)
                return NotFound();

            return Ok(partidoActualizado);
        }

        // DELETE: api/Partido/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Eliminar(int id)
        {
            var eliminado = await _partidoService.EliminarAsync(id);
            if (!eliminado)
                return NotFound();

            return Ok(true);
        }
    }
}
