using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace VotacionDAL.Repositories.Voto
{
    public class VotoRepository : IVotoRepository
    {
        private readonly VotacionContext _context;
        private readonly HttpClient _httpClient;

        public VotoRepository(VotacionContext context, HttpClient httpClient)
        {
            _context = context;
            _httpClient = httpClient;
        }

        public async Task<List<VotacionObjetos.Models.Voto>> ObtenerTodosAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<VotacionObjetos.Models.Voto>>("/api/Votos");
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
            var response = await _httpClient.PostAsJsonAsync("/api/Voto", voto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<VotacionObjetos.Models.Voto>();
        }
    }
}

