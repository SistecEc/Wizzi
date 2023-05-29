using AutoMapper;
using Wizzi.Dtos.Agendas;
using Wizzi.Entities;

namespace Wizzi.Helpers.MpperProfiles
{
    public class AgendasProfile : Profile
    {
        public AgendasProfile()
        {
            CreateMap<Agendas, VerAgendaDto>()
                .ForMember(dest => dest.Codigo,
                            opt => opt.MapFrom(o => o.CodigoAgenda))
                .ForMember(dest => dest.Titulo,
                            opt => opt.MapFrom(o => o.TituloAgenda))
                .ForMember(dest => dest.Descripcion,
                            opt => opt.MapFrom(o => o.DescripcionAgenda))
                .ForMember(dest => dest.FechaInicio,
                            opt => opt.MapFrom(o => o.FechaInicioAgenda))
                .ForMember(dest => dest.FechaFin,
                            opt => opt.MapFrom(o => o.FechaFinAgenda))
                .ForMember(dest => dest.TodoElDia,
                            opt => opt.MapFrom(o => o.EsTodoElDiaAgenda == 1))
                .ForMember(dest => dest.ReglaRecurrencia,
                            opt => opt.MapFrom(o => o.ReglaRecurrenciaAgenda))
                .ForMember(dest => dest.FechasExcluidasRecurrencia,
                            opt => opt.MapFrom(o => o.FechasExluidasRecurrencia))
                .ForMember(dest => dest.Empleado,
                            opt => opt.MapFrom(o => o.EmpleadosAgendaNavigation))
                .ForMember(dest => dest.FechaRegistro,
                            opt => opt.MapFrom(o => o.FechaRegistroAgenda))
                .ForMember(dest => dest.fechaUltimaModificacion,
                            opt => opt.MapFrom(o => o.FechaUltimaModificacionAgenda))
                .ForMember(dest => dest.TipoAgenda,
                            opt => opt.MapFrom(o => o.TiposAgendasAgendaNavigation))
                .ForMember(dest => dest.Estado,
                            opt => opt.MapFrom(o => o.EstadoAgenda))
                ;

            CreateMap<Empleados, VerEmpleadoDto>()
                .ForMember(dest => dest.Codigo,
                            opt => opt.MapFrom(o => o.CodigoEmpleado))
                .ForMember(dest => dest.Nombre,
                            opt => opt.MapFrom(o => o.NombreEmpleado))
                .ForMember(dest => dest.Apellido,
                            opt => opt.MapFrom(o => o.ApellidoEmpleado))
                ;

            CreateMap<Tiposagendas, VerTipoAgendaDto>()
                .ForMember(dest => dest.Codigo,
                            opt => opt.MapFrom(o => o.CodigoTipoAgenda))
                .ForMember(dest => dest.Descripcion,
                            opt => opt.MapFrom(o => o.DescripcionTipoAgenda))
                ;

            CreateMap<Agendas, VerAgendaCitaMedicaDto>()
                .ForMember(dest => dest.Codigo,
                            opt => opt.MapFrom(o => o.CodigoAgenda))
                .ForMember(dest => dest.Titulo,
                            opt => opt.MapFrom(o => o.TituloAgenda))
                .ForMember(dest => dest.Descripcion,
                            opt => opt.MapFrom(o => o.DescripcionAgenda))
                .ForMember(dest => dest.FechaInicio,
                            opt => opt.MapFrom(o => o.FechaInicioAgenda))
                .ForMember(dest => dest.FechaFin,
                            opt => opt.MapFrom(o => o.FechaFinAgenda))
                .ForMember(dest => dest.TodoElDia,
                            opt => opt.MapFrom(o => o.EsTodoElDiaAgenda == 1))
                .ForMember(dest => dest.ReglaRecurrencia,
                            opt => opt.MapFrom(o => o.ReglaRecurrenciaAgenda))
                .ForMember(dest => dest.FechasExcluidasRecurrencia,
                            opt => opt.MapFrom(o => o.FechasExluidasRecurrencia))
                .ForMember(dest => dest.Empleado,
                            opt => opt.MapFrom(o => o.EmpleadosAgendaNavigation))
                .ForMember(dest => dest.FechaRegistro,
                            opt => opt.MapFrom(o => o.FechaRegistroAgenda))
                .ForMember(dest => dest.fechaUltimaModificacion,
                            opt => opt.MapFrom(o => o.FechaUltimaModificacionAgenda))
                .ForMember(dest => dest.TipoAgenda,
                            opt => opt.MapFrom(o => o.TiposAgendasAgendaNavigation))
                .ForMember(dest => dest.Estado,
                            opt => opt.MapFrom(o => o.EstadoAgenda))
                .ForMember(dest => dest.cita,
                            opt => opt.MapFrom(o => o.Citasmedicas))
                ;

            CreateMap<Citasmedicas, CitaMedicaAgendaDto>()
                .ForMember(dest => dest.Codigo,
                            opt => opt.MapFrom(o => o.CodigoCitaMedica))
                .ForMember(dest => dest.tipoCitaMedica,
                            opt => opt.MapFrom(o => o.TipoCitaMedica))
                .ForMember(dest => dest.fuenteRemision,
                            opt => opt.MapFrom(o => o.FuentesRemisionCitaMedica))
                .ForMember(dest => dest.solicitud,
                            opt => opt.MapFrom(o => o.SolicitudesCitaMedica))
                .ForMember(dest => dest.cliente,
                            opt => opt.MapFrom(o => o.ClientesCitaMedicaNavigation))
                ;

            CreateMap<Clientes, ClienteCitaMedicaAgendaDto>()
                .ForMember(dest => dest.Codigo,
                            opt => opt.MapFrom(o => o.CodigoCliente))
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
                            opt => opt.MapFrom(o => o.SexoCliente));

        }
    }
}
