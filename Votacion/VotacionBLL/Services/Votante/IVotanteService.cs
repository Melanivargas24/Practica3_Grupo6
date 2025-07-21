using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotacionObjetos.ViewModels;

namespace VotacionBLL.Services.Votante
{
    public interface IVotanteService
    {
        Task<List<VotanteViewModel>> ObtenerTodosAsync();
        Task<VotanteViewModel> ObtenerPorCedulaAsync(string cedula);
        Task<VotanteViewModel> CrearAsync(VotanteViewModel votante);
        Task<VotanteViewModel> ActualizarAsync(VotanteViewModel votante);
        Task<bool> EliminarAsync(string cedula);
    }
}
