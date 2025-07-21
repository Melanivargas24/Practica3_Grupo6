using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using VotacionDAL.Repositories.Voto;
using VotacionObjetos.ViewModelos;

namespace VotacionBLL.Services.Voto
{
    public class VotoService : IVotoService
    {
        private readonly IVotoRepository _votoRepositorio;
        private readonly IMapper _mapper;

        public VotoService(IVotoRepository votoRepositorio, IMapper mapper)
        {
            _votoRepositorio = votoRepositorio;
            _mapper = mapper;
        }

        public async Task<List<VotoViewModel>> ObtenerTodosAsync()
        {
            var votos = await _votoRepositorio.ObtenerTodosAsync();
            return _mapper.Map<List<VotoViewModel>>(votos);
        }

        public async Task<VotoViewModel> ObtenerPorIdAsync(int id)
        {
            var voto = await _votoRepositorio.ObtenerPorIdAsync(id);
            return _mapper.Map<VotoViewModel>(voto);
        }

        public async Task<VotoViewModel> ObtenerPorCedulaAsync(string cedula)
        {
            var voto = await _votoRepositorio.ObtenerPorCedulaAsync(cedula);
            return _mapper.Map<VotoViewModel>(voto);
        }

        public async Task<VotoViewModel> RegistrarVotoAsync(VotoViewModel votoVm)
        {
            var voto = _mapper.Map<VotacionObjetos.Models.Voto>(votoVm);
            var resultado = await _votoRepositorio.RegistrarVotoAsync(voto);
            return _mapper.Map<VotoViewModel>(resultado);
        }
    }
}
