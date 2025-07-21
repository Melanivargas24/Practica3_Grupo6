using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotacionDAL.Repositories.Partido
{
    public interface IPartidoRepository
    {
        Task<List<VotacionObjetos.Models.Partido>> ObtenerTodosAsync();
        Task<VotacionObjetos.Models.Partido> ObtenerPorIdAsync(int id);
        Task<VotacionObjetos.Models.Partido> CrearAsync(VotacionObjetos.Models.Partido partido);
        Task<VotacionObjetos.Models.Partido> ActualizarAsync(VotacionObjetos.Models.Partido partido);
        Task<bool> EliminarAsync(int id);
    }
}
