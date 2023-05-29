using AutoMapper;
using Wizzi.Dtos.Localizaciones;
using Wizzi.Entities;

namespace Wizzi.Helpers.MpperProfiles
{
    public class LocalizacionesProfile : Profile
    {
        public LocalizacionesProfile()
        {
            CreateMap<Localizacionespaises, VerPaisDto>()
                .ForMember(dest => dest.Codigo,
                            opt => opt.MapFrom(o => o.CodigoLocalizacionPais))
                .ForMember(dest => dest.Descripcion,
                            opt => opt.MapFrom(o => o.NombreLocalizacionPais));

            CreateMap<Localizacionesprovincias, VerProvinciaDto>()
                .ForMember(dest => dest.Codigo,
                            opt => opt.MapFrom(o => o.CodigoLocalizacionProvincia))
                .ForMember(dest => dest.Descripcion,
                            opt => opt.MapFrom(o => o.NombreLocalizacionProvincia));

            CreateMap<Localizacionescantones, VerCantonDto>()
                .ForMember(dest => dest.Codigo,
                            opt => opt.MapFrom(o => o.CodigoLocalizacionCanton))
                .ForMember(dest => dest.Descripcion,
                            opt => opt.MapFrom(o => o.NombreLocalizacionCanton));

            CreateMap<Localizacionesparroquias, VerParroquiaDto>()
                .ForMember(dest => dest.Codigo,
                            opt => opt.MapFrom(o => o.CodigoLocalizacionParroquia))
                .ForMember(dest => dest.Descripcion,
                            opt => opt.MapFrom(o => o.NombreLocalizacionParroquia));

        }
    }
}
