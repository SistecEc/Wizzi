using AutoMapper;
using Wizzi.Dtos.Sucursales;
using Wizzi.Entities;

namespace Wizzi.Helpers.MpperProfiles
{
    public class SucursalesProfile : Profile
    {
        public SucursalesProfile()
        {
            CreateMap<Sucursales, VerSucursalDto>()
                .ForMember(dest => dest.Codigo,
                            opt => opt.MapFrom(o => o.CodigoSucursal))
                .ForMember(dest => dest.Nombre,
                            opt => opt.MapFrom(o => o.NombreSucursal))
                .ForMember(dest => dest.Direccion,
                            opt => opt.MapFrom(o => o.DireccionSucursal))
                .ForMember(dest => dest.localizacion,
                            opt => opt.MapFrom(o => o));

            CreateMap<Sucursales, Localizacion>()
                .ForMember(dest => dest.Pais,
                            opt => opt.MapFrom(o => o.PaisSucursalNavigation))
                .ForMember(dest => dest.Provincia,
                            opt => opt.MapFrom(o => o.ProvinciaSucursalNavigation))
                .ForMember(dest => dest.Canton,
                            opt => opt.MapFrom(o => o.CiudadesSucursalNavigation))
                .ForMember(dest => dest.Parroquia,
                            opt => opt.MapFrom(o => o.ParroquiaSucursalNavigation));

        }
    }
}
