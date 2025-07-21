using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using VotacionDAL.Repositories.Votante;
using VotacionObjetos.ViewModels;

namespace VotacionBLL.Services.Votante
{
    public class VotanteService : IVotanteService
    {
        private readonly IVotanteRepository _votanteRepositorio;
        private readonly IMapper _mapper;

        public VotanteService(IVotanteRepository votanteRepositorio, IMapper mapper)
        {
            _votanteRepositorio = votanteRepositorio;
            _mapper = mapper;
        }

        public async Task<List<VotanteViewModel>> ObtenerTodosAsync()
        {
            var votantes = await _votanteRepositorio.ObtenerTodosAsync();
            return _mapper.Map<List<VotanteViewModel>>(votantes);
        }

        public async Task<VotanteViewModel> ObtenerPorCedulaAsync(string cedula)
        {
            var votante = await _votanteRepositorio.ObtenerPorCedulaAsync(cedula);
            return _mapper.Map<VotanteViewModel>(votante);
        }

        public async Task<VotanteViewModel> CrearAsync(VotanteViewModel votanteVm)
        {
            var votante = _mapper.Map<VotacionObjetos.Models.Votante>(votanteVm);
            var resultado = await _votanteRepositorio.CrearAsync(votante);
            return _mapper.Map<VotanteViewModel>(resultado);
        }

        public async Task<VotanteViewModel> ActualizarAsync(VotanteViewModel votanteVm)
        {
            var votante = _mapper.Map<VotacionObjetos.Models.Votante>(votanteVm);
            var resultado = await _votanteRepositorio.ActualizarAsync(votante);
            return _mapper.Map<VotanteViewModel>(resultado);
        }

        public async Task<bool> EliminarAsync(string cedula)
        {
            return await _votanteRepositorio.EliminarAsync(cedula);
        }
    }
}
