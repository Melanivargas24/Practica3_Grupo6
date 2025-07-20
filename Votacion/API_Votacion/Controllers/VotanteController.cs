using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using VotacionBLL.Services.Votante;
using VotacionObjetos.ViewModels;

namespace API_Votacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VotanteController : ControllerBase
    {
        private readonly IVotanteService _votanteService;

        public VotanteController(IVotanteService votanteService)
        {
            _votanteService = votanteService;
        }

        // GET: api/votante
        [HttpGet]
        public async Task<ActionResult<List<VotanteViewModel>>> GetTodos()
        {
            var votantes = await _votanteService.ObtenerTodosAsync();
            return Ok(votantes);
        }

        // GET: api/votante/cedula/123456789
        [HttpGet("cedula/{cedula}")]
        public async Task<ActionResult<VotanteViewModel>> GetPorCedula(string cedula)
        {
            var votante = await _votanteService.ObtenerPorCedulaAsync(cedula);
            if (votante == null)
                return NotFound();

            return Ok(votante);
        }

        // POST: api/votante
        [HttpPost]
        public async Task<ActionResult<VotanteViewModel>> Crear([FromBody] VotanteViewModel votante)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var creado = await _votanteService.CrearAsync(votante);
                return CreatedAtAction(nameof(GetPorCedula), new { cedula = creado.Cedula }, creado);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { mensaje = ex.Message });
            }
        }

        // PUT: api/votante/123456789
        [HttpPut("{cedula}")]
        public async Task<ActionResult<VotanteViewModel>> Actualizar(string cedula, [FromBody] VotanteViewModel votante)
        {
            if (cedula != votante.Cedula)
                return BadRequest("La cédula no coincide.");

            var actualizado = await _votanteService.ActualizarAsync(votante);
            if (actualizado == null)
                return NotFound();

            return Ok(actualizado);
        }

        // DELETE: api/votante/123456789
        [HttpDelete("{cedula}")]
        public async Task<IActionResult> Eliminar(string cedula)
        {
            var eliminado = await _votanteService.EliminarAsync(cedula);
            if (!eliminado)
                return NotFound();

            return NoContent();
        }
    }
}
