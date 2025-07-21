using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using VotacionObjetos.Models;
using VotacionObjetos.ViewModelos;
using VotacionObjetos.ViewModels;

namespace VotacionObjetos
{
    public class MapeoClases : Profile
    {
        public MapeoClases()
        {
            
            CreateMap<Votante, VotanteViewModel>().ReverseMap();

            CreateMap<Partido, PartidoViewModel>().ReverseMap();

            CreateMap<Voto, VotoViewModel>()
                .ForMember(dest => dest.NombreVotante, opt => opt.MapFrom(src => src.Votante.Nombre))
                .ForMember(dest => dest.NombrePartido, opt => opt.MapFrom(src => src.Partido.Nombre));

            CreateMap<VotoViewModel, Voto>()
                .ForMember(dest => dest.Votante, opt => opt.Ignore())
                .ForMember(dest => dest.Partido, opt => opt.Ignore());
        }
    }

}
