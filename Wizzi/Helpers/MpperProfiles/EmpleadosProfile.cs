using AutoMapper;
using Wizzi.Dtos.Empleados;
using Wizzi.Dtos.Users;
using Wizzi.Entities;

namespace Wizzi.Helpers.MpperProfiles
{
    public class EmpleadosProfile : Profile
    {
        public EmpleadosProfile()
        {
            CreateMap<Empleados, UserDto>()
                .ForMember(dest => dest.Id,
                            opt => opt.MapFrom(o => o.CodigoEmpleado))
                .ForMember(dest => dest.FirstName,
                            opt => opt.MapFrom(o => o.NombreEmpleado))
                .ForMember(dest => dest.LastName,
                            opt => opt.MapFrom(o => o.ApellidoEmpleado))
                .ForMember(dest => dest.Username,
                            opt => opt.MapFrom(o => o.NombreUsuarioEmpleado))
                .ForMember(dest => dest.Password,
                            opt => opt.MapFrom(o => o.ClaveUsuarioEmpleado))
                .ReverseMap();

            CreateMap<Empleados, VerEmpleadoDto>()
                .ForMember(dest => dest.Codigo,
                            opt => opt.MapFrom(o => o.CodigoEmpleado))
                .ForMember(dest => dest.Nombre,
                            opt => opt.MapFrom(o => o.NombreEmpleado))
                .ForMember(dest => dest.Apellido,
                            opt => opt.MapFrom(o => o.ApellidoEmpleado))
                .ReverseMap();
        }
    }
}
