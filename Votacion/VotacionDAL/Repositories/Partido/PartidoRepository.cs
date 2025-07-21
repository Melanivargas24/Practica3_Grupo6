using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace VotacionDAL.Repositories.Partido
{
    public class PartidoRepository : IPartidoRepository
    {
        private readonly VotacionContext _context;
        private readonly HttpClient _httpClient;

        public PartidoRepository(VotacionContext context, HttpClient httpClient)
        {
            _context = context;
            _httpClient = httpClient;
        }

        public async Task<List<VotacionObjetos.Models.Partido>> ObtenerTodosAsync()
        {
            var partidos = await _httpClient.GetFromJsonAsync<List<VotacionObjetos.Models.Partido>>("/api/Partidos");
            return partidos;
        }

        public async Task<VotacionObjetos.Models.Partido> ObtenerPorIdAsync(int id)
        {
            return await _context.Partidos.FindAsync(id);
        }

        public async Task<VotacionObjetos.Models.Partido> CrearAsync(VotacionObjetos.Models.Partido partido)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/Partido", partido);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<VotacionObjetos.Models.Partido>();
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/Partido/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task<VotacionObjetos.Models.Partido> ActualizarAsync(VotacionObjetos.Models.Partido partido)
        {
            var existente = await _context.Partidos.FindAsync(partido.Id);
            if (existente == null) return null;

            _context.Entry(existente).CurrentValues.SetValues(partido);
            await _context.SaveChangesAsync();
            return existente;
        }
    }
}