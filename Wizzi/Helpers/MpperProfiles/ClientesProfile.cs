using AutoMapper;
using Wizzi.Dtos.Clientes;
using Wizzi.Entities;

namespace Wizzi.Helpers.MpperProfiles
{
    public class ClientesProfile : Profile
    {
        public ClientesProfile()
        {
            CreateMap<RegistrarClienteDto, Clientes>()
                .ForMember(dest => dest.CodigoCliente,
                            opt => opt.MapFrom(o => o.Codigo))
                .ForMember(dest => dest.TiposIdentificacionCliente,
                            opt => opt.MapFrom(o => o.TipoIdentificacion))
                .ForMember(dest => dest.NumeroIdentificacionCliente,
                            opt => opt.MapFrom(o => o.Identificacion))
                .ForMember(dest => dest.NombreComercialCliente,
                            opt => opt.MapFrom(o => o.NombreComercial))
                .ForMember(dest => dest.PrioridadNombreComercialCliente,
                            opt => opt.MapFrom(o => o.PrioridadNombreComercial ? "1" : "0"))
                .ForMember(dest => dest.NombreCliente,
                            opt => opt.MapFrom(o => o.Nombre))
                .ForMember(dest => dest.ApellidoCliente,
                            opt => opt.MapFrom(o => o.Apellido))
                .ForMember(dest => dest.DireccionUnoCliente,
                            opt => opt.MapFrom(o => o.Direccion))
                .ForMember(dest => dest.TelefonoUnoCliente,
                            opt => opt.MapFrom(o => o.Telefono))
                .ForMember(dest => dest.MailCliente,
                            opt => opt.MapFrom(o => o.Email))
                .ForMember(dest => dest.SexoCliente,
                            opt => opt.MapFrom(o => o.Genero))
                .ForMember(dest => dest.FechaNacimientoCliente,
                            opt => opt.MapFrom(o => o.FechaNacimiento));

            CreateMap<Clientes, VerClienteDto>()
                .ForMember(dest => dest.Codigo,
                            opt => opt.MapFrom(o => o.CodigoCliente))
                .ForMember(dest => dest.tipoIdentificacion,
                            opt => opt.MapFrom(o => o.TiposIdentificacionClienteNavigation))
                .ForMember(dest => dest.Identificacion,
                            opt => opt.MapFrom(o => o.NumeroIdentificacionCliente))
                .ForMember(dest => dest.NombreComercial,
                            opt => opt.MapFrom(o => o.NombreComercialCliente))
                .ForMember(dest => dest.Nombre,
                            opt => opt.MapFrom(o => o.NombreCliente))
                .ForMember(dest => dest.Apellido,
                            opt => opt.MapFrom(o => o.ApellidoCliente))
                .ForMember(dest => dest.Direccion,
                            opt => opt.MapFrom(o => o.DireccionUnoCliente))
                .ForMember(dest => dest.Telefono,
                            opt => opt.MapFrom(o => o.TelefonoUnoCliente))
                .ForMember(dest => dest.Email,
                            opt => opt.MapFrom(o => o.MailCliente))
                .ForMember(dest => dest.Genero,
                            opt => opt.MapFrom(o => o.SexoCliente))
                .ForMember(dest => dest.FechaNacimiento,
                            opt => opt.MapFrom(o => o.FechaNacimientoCliente))
                .ForMember(dest => dest.localizacion,
                            opt => opt.MapFrom(o => o.Clienteslocalizaciones));

            CreateMap<Clienteslocalizaciones, LocalizacionDto>()
                .ForMember(dest => dest.Pais,
                            opt => opt.MapFrom(o => o.PaisesClienteLocalizacionNavigation))
                .ForMember(dest => dest.Provincia,
                            opt => opt.MapFrom(o => o.ProvinciasClienteLocalizacionNavigation))
                .ForMember(dest => dest.Canton,
                            opt => opt.MapFrom(o => o.CantonesClienteLocalizacionNavigation))
                .ForMember(dest => dest.Parroquia,
                            opt => opt.MapFrom(o => o.ParroquiasClienteLocalizacionNavigation))
                .ReverseMap();
        }
    }
}
