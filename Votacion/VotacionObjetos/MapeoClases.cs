using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using VotacionObjetos.Models;
using VotacionObjetos.ViewModels;

namespace VotacionObjetos
{
    public class MapeoClases : Profile
    {
        public MapeoClases()
        {
            // Mapeo de Partido
            CreateMap<Partido, PartidoViewModel>().ReverseMap();

            // Mapeo de Votante
            CreateMap<Votante, VotanteViewModel>().ReverseMap();

            // Mapeo de Voto
            CreateMap<Voto, VotoViewModel>().ReverseMap();
        }
    }
}
