using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using VotacionDAL.Repositories.Partido;
using VotacionObjetos.ViewModels;
using VotacionObjetos.Models;

namespace VotacionBLL.Services.Partido
{
    public class PartidoService : IPartidoService
    {
        private readonly IPartidoRepository _partidoRepositorio;
        private readonly IMapper _mapper;

        public PartidoService(IPartidoRepository partidoRepositorio, IMapper mapper)
        {
            _partidoRepositorio = partidoRepositorio;
            _mapper = mapper;
        }

        public async Task<List<PartidoViewModel>> ObtenerTodosAsync()
        {
            var partidos = await _partidoRepositorio.ObtenerTodosAsync();
            return _mapper.Map<List<PartidoViewModel>>(partidos);
        }

        public async Task<PartidoViewModel> ObtenerPorIdAsync(int id)
        {
            var partido = await _partidoRepositorio.ObtenerPorIdAsync(id);
            return _mapper.Map<PartidoViewModel>(partido);
        }

        public async Task<PartidoViewModel> CrearAsync(PartidoViewModel partidoVm)
        {
            var partido = _mapper.Map<VotacionObjetos.Models.Partido>(partidoVm);
            var resultado = await _partidoRepositorio.CrearAsync(partido);
            return _mapper.Map<PartidoViewModel>(resultado);
        }

        public async Task<PartidoViewModel> ActualizarAsync(PartidoViewModel partidoVm)
        {
            var partido = _mapper.Map<VotacionObjetos.Models.Partido>(partidoVm); 
            var resultado = await _partidoRepositorio.ActualizarAsync(partido);
            return _mapper.Map<PartidoViewModel>(resultado);
        }

        public async Task<bool> EliminarAsync(int id)
        {
            return await _partidoRepositorio.EliminarAsync(id);
        }
    }
}
