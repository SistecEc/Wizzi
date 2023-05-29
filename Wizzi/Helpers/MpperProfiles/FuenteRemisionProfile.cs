using AutoMapper;
using Wizzi.Dtos.FuentesRemision;
using Wizzi.Entities;

namespace Wizzi.Helpers.MpperProfiles
{
    public class FuenteRemisionProfile : Profile
    {
        public FuenteRemisionProfile()
        {
            CreateMap<Fuentesremision, VerFuentesRemision>()
                .ForMember(dest => dest.codigo,
                            opt => opt.MapFrom(o => o.CodigoFuenteRemision))
                .ForMember(dest => dest.descripcion,
                            opt => opt.MapFrom(o => o.DescripcionFuenteRemision));

        }
    }
}
