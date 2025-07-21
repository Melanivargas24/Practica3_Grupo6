using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotacionObjetos.ViewModelos;

namespace VotacionBLL.Services.Voto
{
    public interface IVotoService
    {
        Task<List<VotoViewModel>> ObtenerTodosAsync();
        Task<VotoViewModel> ObtenerPorIdAsync(int id);
        Task<VotoViewModel> ObtenerPorCedulaAsync(string cedula);
        Task<VotoViewModel> RegistrarVotoAsync(VotoViewModel voto);
    }
}
