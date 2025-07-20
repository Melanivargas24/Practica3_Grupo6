using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using VotacionBLL.Services.Voto;
using VotacionObjetos.ViewModelos;

namespace API_Votacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VotoController : ControllerBase
    {
        private readonly IVotoService _votoService;

        public VotoController(IVotoService votoService)
        {
            _votoService = votoService;
        }

        // GET: api/Voto
        [HttpGet]
        public async Task<ActionResult<List<VotoViewModel>>> GetTodos()
        {
            var votos = await _votoService.ObtenerTodosAsync();
            return Ok(votos);
        }

        // GET: api/Voto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VotoViewModel>> GetPorId(int id)
        {
            var voto = await _votoService.ObtenerPorIdAsync(id);
            if (voto == null)
                return NotFound();
            return Ok(voto);
        }

        // GET: api/Voto/cedula/123456789
        [HttpGet("cedula/{cedula}")]
        public async Task<ActionResult<VotoViewModel>> GetPorCedula(string cedula)
        {
            var voto = await _votoService.ObtenerPorCedulaAsync(cedula);
            if (voto == null)
                return NotFound();
            return Ok(voto);
        }

        // POST: api/Voto
        [HttpPost]
        public async Task<ActionResult<VotoViewModel>> RegistrarVoto(VotoViewModel votoVm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var votoRegistrado = await _votoService.RegistrarVotoAsync(votoVm);
            return CreatedAtAction(nameof(GetPorId), new { id = votoRegistrado.Id }, votoRegistrado);
        }
    }
}