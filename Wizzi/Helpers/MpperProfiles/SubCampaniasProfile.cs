using AutoMapper;
using Wizzi.Dtos.Subcampanias;
using Wizzi.Entities;

namespace Wizzi.Helpers.MpperProfiles
{
    public class SubCampaniasProfile : Profile
    {
        public SubCampaniasProfile()
        {
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
                            opt => opt.MapFrom(o => o.FechaFinSubCampania))
                .ReverseMap();

            CreateMap<ManejoSubCampaniasDto, Subcampanias>()
                .ForMember(dest => dest.CodigoSubCampania,
                            opt => opt.MapFrom(o => o.codigo))
                .ForMember(dest => dest.CampaniasSubCampania,
                            opt => opt.MapFrom(o => o.codigoCampania))
                .ForMember(dest => dest.DescripcionSubCampania,
                            opt => opt.MapFrom(o => o.descripcion))
                .ForMember(dest => dest.FechaInicioSubCampania,
                            opt => opt.MapFrom(o => o.fechaInicio))
                .ForMember(dest => dest.FechaFinSubCampania,
                            opt => opt.MapFrom(o => o.fechaFin));
        }
    }
}
