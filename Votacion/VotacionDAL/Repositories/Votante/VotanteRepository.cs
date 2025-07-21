using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace VotacionDAL.Repositories.Votante
{
    public class VotanteRepository : IVotanteRepository
    {
        private readonly VotacionContext _context;
        private readonly HttpClient _httpClient;

        public VotanteRepository(VotacionContext context, HttpClient httpClient)
        {
            _context = context;
            _httpClient = httpClient;
        }

        public async Task<List<VotacionObjetos.Models.Votante>> ObtenerTodosAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<VotacionObjetos.Models.Votante>>("/api/Votantes");
        }

        public async Task<VotacionObjetos.Models.Votante> ObtenerPorCedulaAsync(string cedula)
        {
            return await _context.Votantes.FindAsync(cedula);
        }

        public async Task<VotacionObjetos.Models.Votante> CrearAsync(VotacionObjetos.Models.Votante votante)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/Votante", votante);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<VotacionObjetos.Models.Votante>();
        }

        public async Task<bool> EliminarAsync(string cedula)
        {
            var response = await _httpClient.DeleteAsync($"/api/Votante/{cedula}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task<VotacionObjetos.Models.Votante> ActualizarAsync(VotacionObjetos.Models.Votante votante)
        {
            var existente = await _context.Votantes.FindAsync(votante.Cedula);
            if (existente == null) return null;

            _context.Entry(existente).CurrentValues.SetValues(votante);
            await _context.SaveChangesAsync();
            return existente;
        }
    }
}
