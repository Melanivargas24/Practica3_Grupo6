using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotacionBLL.Services.Partido
{
    public interface IPartidoService
    {
        Task<List<VotacionObjetos.ViewModels.PartidoViewModel>> ObtenerTodosAsync();
        Task<VotacionObjetos.ViewModels.PartidoViewModel> ObtenerPorIdAsync(int id);
        Task<VotacionObjetos.ViewModels.PartidoViewModel> CrearAsync(VotacionObjetos.ViewModels.PartidoViewModel partido);
        Task<VotacionObjetos.ViewModels.PartidoViewModel> ActualizarAsync(VotacionObjetos.ViewModels.PartidoViewModel partido);
        Task<bool> EliminarAsync(int id);
    }
}
