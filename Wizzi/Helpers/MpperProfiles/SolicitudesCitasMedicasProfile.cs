using AutoMapper;
using Wizzi.Dtos.SolicitudesCitasMedicas;
using Wizzi.Entities;

namespace Wizzi.Helpers.MpperProfiles
{
    public class SolicitudesCitasMedicasProfile : Profile
    {
        public SolicitudesCitasMedicasProfile()
        {

            CreateMap<RegistroSolicitudCitaMedicaDto, Solicitudcitasmedicas>()
                .ForMember(dest => dest.CodigoSoliCitaMedica,
                            opt => opt.MapFrom(o => o.codigo))
                .ForMember(dest => dest.EsPacienteSoliCitaMedica,
                            opt => opt.MapFrom(o => o.esPaciente))
                .ForMember(dest => dest.NombreClienteSoliCitaMedica,
                            opt => opt.MapFrom(o => o.nombrePaciente))
                .ForMember(dest => dest.ApellidoClienteSoliCitaMedica,
                            opt => opt.MapFrom(o => o.apellidoPaciente))
                .ForMember(dest => dest.CelularClienteSoliCitaMedica,
                            opt => opt.MapFrom(o => o.telefonoPaciente))
                .ForMember(dest => dest.EmailClienteSoliCitaMedica,
                            opt => opt.MapFrom(o => o.emailPaciente))
                .ForMember(dest => dest.GeneroClienteSoliCitaMedica,
                            opt => opt.MapFrom(o => o.generoPaciente))
                .ForMember(dest => dest.FechaNacimientoClienteSoliCitaMedica,
                            opt => opt.MapFrom(o => o.fechaNacimiento))
                .ForMember(dest => dest.NombreRepresentanteSoliCitaMedica,
                            opt => opt.MapFrom(o => o.nombreRepresentante))
                .ForMember(dest => dest.ApellidoRepresentanteSoliCitaMedica,
                            opt => opt.MapFrom(o => o.apellidoRepresentante))
                .ForMember(dest => dest.CelularRepresentanteSoliCitaMedica,
                            opt => opt.MapFrom(o => o.telefonoRepresentante))
                .ForMember(dest => dest.EmailRepresentanteSoliCitaMedica,
                            opt => opt.MapFrom(o => o.emailRepresentante))
                .ForMember(dest => dest.FechaTentativaSoliCitaMedica,
                            opt => opt.MapFrom(o => o.fechaTentativa))
                .ForMember(dest => dest.ObservacionSoliCitaMedica,
                            opt => opt.MapFrom(o => o.observacion))
                .ForMember(dest => dest.RelacionesRepresentantePacienteSoliCitaMedica,
                            opt => opt.MapFrom(o => o.relacionRepresentantePaciente))
                .ForMember(dest => dest.CiudadesSoliCitaMedica,
                            opt => opt.MapFrom(o => o.ciudad))
                .ForMember(dest => dest.SucursalesSoliCitaMedica,
                            opt => opt.MapFrom(o => o.sucursal))
                .ForMember(dest => dest.SubCampaniasOrigen,
                            opt => opt.MapFrom(o => o.subCampaniaOrigen));

            CreateMap<Solicitudcitasmedicas, VerSolicitudCitaMedicaDto>()
                .ForMember(dest => dest.codigo,
                            opt => opt.MapFrom(o => o.CodigoSoliCitaMedica))
                .ForMember(dest => dest.esPaciente,
                            opt => opt.MapFrom(o => o.EsPacienteSoliCitaMedica == 1))
                .ForMember(dest => dest.nombrePaciente,
                            opt => opt.MapFrom(o => o.NombreClienteSoliCitaMedica))
                .ForMember(dest => dest.apellidoPaciente,
                            opt => opt.MapFrom(o => o.ApellidoClienteSoliCitaMedica))
                .ForMember(dest => dest.telefonoPaciente,
                            opt => opt.MapFrom(o => o.CelularClienteSoliCitaMedica))
                .ForMember(dest => dest.emailPaciente,
                            opt => opt.MapFrom(o => o.EmailClienteSoliCitaMedica))
                .ForMember(dest => dest.generoPaciente,
                            opt => opt.MapFrom(o => o.GeneroClienteSoliCitaMedica))
                .ForMember(dest => dest.fechaNacimiento,
                            opt => opt.MapFrom(o => o.FechaNacimientoClienteSoliCitaMedica))
                .ForMember(dest => dest.nombreRepresentante,
                            opt => opt.MapFrom(o => o.NombreRepresentanteSoliCitaMedica))
                .ForMember(dest => dest.apellidoRepresentante,
                            opt => opt.MapFrom(o => o.ApellidoRepresentanteSoliCitaMedica))
                .ForMember(dest => dest.relacionRepresentantePaciente,
                            opt => opt.MapFrom(o => o.RelacionesRepresentantePacienteSoliCitaMedicaNavigation))
                .ForMember(dest => dest.telefonoRepresentante,
                            opt => opt.MapFrom(o => o.CelularRepresentanteSoliCitaMedica))
                .ForMember(dest => dest.emailRepresentante,
                            opt => opt.MapFrom(o => o.EmailRepresentanteSoliCitaMedica))
                .ForMember(dest => dest.subcampaniaOrigen,
                            opt => opt.MapFrom(o => o.SubCampaniasOrigenNavigation))
                .ForMember(dest => dest.localizacion,
                            opt => opt.MapFrom(o => o.CiudadesSoliCitaMedicaNavigation))
                .ForMember(dest => dest.sucursal,
                            opt => opt.MapFrom(o => o.SucursalesSoliCitaMedicaNavigation))
                .ForMember(dest => dest.observacion,
                            opt => opt.MapFrom(o => o.ObservacionSoliCitaMedica))
                .ForMember(dest => dest.fechaTentativa,
                            opt => opt.MapFrom(o => o.FechaTentativaSoliCitaMedica))
                .ForMember(dest => dest.fechaRegistro,
                            opt => opt.MapFrom(o => o.FechaRegistroSoliCitaMedica));

            CreateMap<Localizacionescantones, LocalizacionDto>()
                .ForMember(dest => dest.Canton,
                            opt => opt.MapFrom(o => o))
                .ForMember(dest => dest.Provincia,
                            opt => opt.MapFrom(o => o.ProvinciasLocalizacionCantonNavigation))
                .ForMember(dest => dest.Pais,
                            opt => opt.MapFrom(o => o.ProvinciasLocalizacionCantonNavigation.PaisesLocalizacionProvinciaNavigation));
        }
    }
}
