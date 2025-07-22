using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VotacionObjetos.ViewModels;

namespace VotacionBLL.Services.Voto
{
    public interface IVotoService
    {
        Task<List<VotoViewModel>> ObtenerTodosAsync();
        Task<VotoViewModel> ObtenerPorIdAsync(int id);
        Task<VotoViewModel> ObtenerPorCedulaAsync(string cedula);
        Task<VotoViewModel> RegistrarVotoAsync(VotoViewModel votoVm);
    }
}
