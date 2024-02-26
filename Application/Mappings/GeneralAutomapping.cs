using Domain.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class GeneralAutomapping : Profile
    {
        public GeneralAutomapping()
        {
            CreateMap<CircunscripcionDto,Circunscripcion>().ReverseMap();
            CreateMap<Localidad, LocalidadDto>().ReverseMap();
            CreateMap<UsuarioBasedeDatosPoderJudicial, UsuarioBBDDPoderJudicialDTO>().ReverseMap();
        }

        
    }
}
