

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using VotacionDAL.Repositories.Partido;
using VotacionObjetos.Models;

namespace VotacionDAL.Repositories.Partido
{
    public class PartidoRepository : IPartidoRepository
    {
        private readonly VotacionContext _context;

        public PartidoRepository(VotacionContext context)
        {
            _context = context;
        }

        public async Task<List<VotacionObjetos.Models.Partido>> ObtenerTodosAsync()
        {
            return await _context.Partidos
                .Include(p => p.Votos)
                .ToListAsync();
        }

        public async Task<VotacionObjetos.Models.Partido> ObtenerPorIdAsync(int id)
        {
            return await _context.Partidos
                .Include(p => p.Votos)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<VotacionObjetos.Models.Partido> CrearAsync(VotacionObjetos.Models.Partido partido)
        {
            _context.Partidos.Add(partido);
            await _context.SaveChangesAsync();
            return partido;
        }

        public async Task<VotacionObjetos.Models.Partido> ActualizarAsync(VotacionObjetos.Models.Partido partido)
        {
            var existente = await _context.Partidos.FindAsync(partido.Id);
            if (existente == null)
                return null;

            _context.Entry(existente).CurrentValues.SetValues(partido);
            await _context.SaveChangesAsync();
            return existente;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var partido = await _context.Partidos.FindAsync(id);
            if (partido == null)
                return false;

            _context.Partidos.Remove(partido);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
