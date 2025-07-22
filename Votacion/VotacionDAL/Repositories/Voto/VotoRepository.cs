using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;

namespace VotacionDAL.Repositories.Voto
{
    public class VotoRepository : IVotoRepository
    {
        private readonly VotacionContext _context;

        public VotoRepository(VotacionContext context)
        {
            _context = context;
        }

        public async Task<List<VotacionObjetos.Models.Voto>> ObtenerTodosAsync()
        {
            return await _context.Votos.ToListAsync();
        }

        public async Task<VotacionObjetos.Models.Voto> ObtenerPorIdAsync(int id)
        {
            return await _context.Votos.FindAsync(id);
        }

        public async Task<VotacionObjetos.Models.Voto> ObtenerPorCedulaAsync(string cedula)
        {
            return await _context.Votos.FirstOrDefaultAsync(v => v.CedulaVotante == cedula);
        }

        public async Task<VotacionObjetos.Models.Voto> RegistrarVotoAsync(VotacionObjetos.Models.Voto voto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {

                voto.Fecha = DateTime.Now;
                _context.Votos.Add(voto);
                await _context.SaveChangesAsync();

                var votante = await _context.Votantes.FirstOrDefaultAsync(v => v.Cedula == voto.CedulaVotante);
                if (votante != null)
                {
                    votante.SiVoto = true;
                    await _context.SaveChangesAsync();
                }

                await transaction.CommitAsync();
                return voto;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}


