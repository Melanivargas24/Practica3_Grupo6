using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotacionDAL.Repositories.Votante
{
    public interface IVotanteRepository
    {
        Task<List<VotacionObjetos.Models.Votante>> ObtenerTodosAsync();
        Task<VotacionObjetos.Models.Votante> ObtenerPorCedulaAsync(string cedula);
        Task<VotacionObjetos.Models.Votante> CrearAsync(VotacionObjetos.Models.Votante votante);
        Task<VotacionObjetos.Models.Votante> ActualizarAsync(VotacionObjetos.Models.Votante votante);
        Task<bool> EliminarAsync(string cedula);
    }
}
