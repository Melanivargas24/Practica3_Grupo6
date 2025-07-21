using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace VotacionDAL.Repositories.Votante
{
    public class VotanteRepository : IVotanteRepository
    {
        private readonly VotacionContext _context;

        public VotanteRepository(VotacionContext context)
        {
            _context = context;
        }

        public async Task<List<VotacionObjetos.Models.Votante>> ObtenerTodosAsync()
        {
            return await _context.Votantes.ToListAsync();
        }

        public async Task<VotacionObjetos.Models.Votante> ObtenerPorCedulaAsync(string cedula)
        {
            return await _context.Votantes.FirstOrDefaultAsync(v => v.Cedula == cedula);
        }

        public async Task<VotacionObjetos.Models.Votante> CrearAsync(VotacionObjetos.Models.Votante votante)
        {
            // Verificar si ya existe un votante con esa cedula
            var existente = await _context.Votantes.FindAsync(votante.Cedula);
            if (existente != null)
            {
                throw new InvalidOperationException("Ya existe un votante con esa cedula");
            }

            _context.Votantes.Add(votante);
            await _context.SaveChangesAsync();
            return votante;
        }

        public async Task<bool> EliminarAsync(string cedula)
        {
            var votante = await _context.Votantes.FindAsync(cedula);
            if (votante == null)
                return false;

            _context.Votantes.Remove(votante);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<VotacionObjetos.Models.Votante> ActualizarAsync(VotacionObjetos.Models.Votante votante)
        {
            var existente = await _context.Votantes.FindAsync(votante.Cedula);
            if (existente == null)
                return null;

            _context.Entry(existente).CurrentValues.SetValues(votante);
            await _context.SaveChangesAsync();
            return existente;
        }
    }
}
