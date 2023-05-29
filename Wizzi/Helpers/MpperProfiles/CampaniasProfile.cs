using AutoMapper;
using Wizzi.Dtos.Campanias;
using Wizzi.Entities;

namespace Wizzi.Helpers.MpperProfiles
{
    public class CampaniasProfile : Profile
    {
        public CampaniasProfile()
        {
            CreateMap<Campanias, CampaniaDto>()
                .ForMember(dest => dest.codigo,
                            opt => opt.MapFrom(o => o.CodigoCampania))
                .ForMember(dest => dest.titulo,
                            opt => opt.MapFrom(o => o.TituloCampania))
                .ForMember(dest => dest.descripcion,
                            opt => opt.MapFrom(o => o.DescripcionCampania))
                .ForMember(dest => dest.presupuesto,
                            opt => opt.MapFrom(o => o.PresupuestoCampania))
                .ForMember(dest => dest.fechaInicio,
                            opt => opt.MapFrom(o => o.FechaInicioCampania))
                .ForMember(dest => dest.fechaFin,
                            opt => opt.MapFrom(o => o.FechaFinCampania))
                .ReverseMap();

            CreateMap<Subcampanias, SubCampaniasDto>()
                .ForMember(dest => dest.codigo,
                            opt => opt.MapFrom(o => o.CodigoSubCampania))
                .ForMember(dest => dest.descripcion,
                            opt => opt.MapFrom(o => o.DescripcionSubCampania))
                .ForMember(dest => dest.imagen,
                            opt => opt.MapFrom(o => o.ImagenSubCampania))
                .ForMember(dest => dest.fechaInicio,
                            opt => opt.MapFrom(o => o.FechaInicioSubCampania))
                .ForMember(dest => dest.fechaFin,
                            opt => opt.MapFrom(o => o.FechaFinSubCampania));

        }
    }
}
