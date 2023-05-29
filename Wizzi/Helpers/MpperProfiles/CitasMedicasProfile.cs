using AutoMapper;
using Wizzi.Dtos.CitasMedicas;
using Wizzi.Entities;

namespace Wizzi.Helpers.MpperProfiles
{
    public class CitasMedicasProfile : Profile
    {
        public CitasMedicasProfile()
        {
            CreateMap<RegistrarCitaMedicaDto, Citasmedicas>()
                .ForMember(dest => dest.CodigoCitaMedica,
                            opt => opt.MapFrom(o => o.codigo))
                .ForMember(dest => dest.ClientesCitaMedica,
                            opt => opt.MapFrom(o => o.codigoCliente))
                .ForMember(dest => dest.DiagnosticoCitaMedica,
                            opt => opt.MapFrom(o => o.diagnostico))
                .ForMember(dest => dest.PacienteLlegoCitaMedica,
                            opt => opt.MapFrom(o => o.pacienteLlego))
                .ForMember(dest => dest.SolicitudesCitaMedica,
                            opt => opt.MapFrom(o => o.codigoSolicitudCitaMedica))
                .ForMember(dest => dest.TipoCitaMedica,
                            opt => opt.MapFrom(o => o.tipoCita))
                .ForMember(dest => dest.ActivaCitaMedica,
                            opt => opt.MapFrom(o => o.activa))
                .ForMember(dest => dest.SubCampaniasOrigen,
                            opt => opt.MapFrom(o => o.codigoSubCampaniaOrigen));

            CreateMap<Citasmedicas, VerCitaMedicaDto>()
                .ForMember(dest => dest.codigo,
                            opt => opt.MapFrom(o => o.CodigoCitaMedica))
                .ForMember(dest => dest.cliente,
                            opt => opt.MapFrom(o => o.ClientesCitaMedicaNavigation))
                .ForMember(dest => dest.diagnostico,
                            opt => opt.MapFrom(o => o.DiagnosticoCitaMedica))
                .ForMember(dest => dest.pacienteLlego,
                            opt => opt.MapFrom(o => o.PacienteLlegoCitaMedica))
                .ForMember(dest => dest.codigoSolicitudCitaMedica,
                            opt => opt.MapFrom(o => o.SolicitudesCitaMedica))
                .ForMember(dest => dest.tipoCita,
                            opt => opt.MapFrom(o => o.TipoCitaMedica))
                .ForMember(dest => dest.codigoSubCampaniaOrigen,
                            opt => opt.MapFrom(o => o.SubCampaniasOrigen))
                .ForMember(dest => dest.fechaRegistro,
                            opt => opt.MapFrom(o => o.FechaRegistroCitaMedica))
                .ForMember(dest => dest.agenda,
                            opt => opt.MapFrom(o => o.AgendasCitaMedicaNavigation))
                .ForMember(dest => dest.grupoCita,
                            opt => opt.MapFrom(o => o.CodigoGrupoCitaMedica))
                .ForMember(dest => dest.fuenteRemision,
                            opt => opt.MapFrom(o => o.FuentesRemisionCitaMedicaNavigation));

            CreateMap<Clientes, VerClienteDto>()
                .ForMember(dest => dest.Codigo,
                            opt => opt.MapFrom(o => o.CodigoCliente))
                .ForMember(dest => dest.NumeroIdentificacion,
                            opt => opt.MapFrom(o => o.NumeroIdentificacionCliente))
                .ForMember(dest => dest.NombreComercial,
                            opt => opt.MapFrom(o => o.NombreComercialCliente))
                .ForMember(dest => dest.PrioridadNombreComercial,
                            opt => opt.MapFrom(o => o.PrioridadNombreComercialCliente == "1"))
                .ForMember(dest => dest.Nombre,
                            opt => opt.MapFrom(o => o.NombreCliente))
                .ForMember(dest => dest.Apellido,
                            opt => opt.MapFrom(o => o.ApellidoCliente))
                .ForMember(dest => dest.Direccion,
                            opt => opt.MapFrom(o => o.DireccionUnoCliente))
                .ForMember(dest => dest.Telefono,
                            opt => opt.MapFrom(o => o.TelefonoUnoCliente))
                .ForMember(dest => dest.Mail,
                            opt => opt.MapFrom(o => o.MailCliente))
                .ForMember(dest => dest.Sexo,
                            opt => opt.MapFrom(o => o.SexoCliente));
        }
    }
}
