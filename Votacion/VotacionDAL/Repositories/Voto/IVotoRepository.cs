using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotacionDAL.Repositories.Voto
{
    public interface IVotoRepository
    {
        Task<List<VotacionObjetos.Models.Voto>> ObtenerTodosAsync();
        Task<VotacionObjetos.Models.Voto> ObtenerPorIdAsync(int id);
        Task<VotacionObjetos.Models.Voto> ObtenerPorCedulaAsync(string cedula);
        Task<VotacionObjetos.Models.Voto> RegistrarVotoAsync(VotacionObjetos.Models.Voto voto);
    }
}
