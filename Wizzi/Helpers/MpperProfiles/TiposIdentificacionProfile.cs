using AutoMapper;
using Wizzi.Dtos.Clientes;
using Wizzi.Entities;

namespace Wizzi.Helpers.MpperProfiles
{
    public class TiposIdentificacionProfile : Profile
    {
        public TiposIdentificacionProfile()
        {
            CreateMap<Tiposidentificacion, tipoIdentificacionDto>()
                .ForMember(dest => dest.Codigo,
                            opt => opt.MapFrom(o => o.CodigoTipoIdentificacion))
                .ForMember(dest => dest.Descripcion,
                            opt => opt.MapFrom(o => o.NombreTipoIdentificacion))
                .ReverseMap();

        }
    }
}
