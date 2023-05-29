using AutoMapper;
using CSharpVitamins;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Options;
using MoreLinq.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Wizzi.Constants;
using Wizzi.Dtos.Agendas;
using Wizzi.Dtos.CitasMedicas;
using Wizzi.Dtos.Llamadas;
using Wizzi.Dtos.SolicitudesCitasMedicas;
using Wizzi.Entities;
using Wizzi.Enums;
using Wizzi.Extensions;
using Wizzi.Helpers;
using Wizzi.Interfaces;
using Wizzi.Models;
using Wizzi.Services;

namespace Wizzi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SolicitudesCitasController : ControllerBase
    {
        private DataContext _wiseContext;
        private IMapper _mapper;
        private ICallCenterService _callCenter;
        private readonly UserResolverService _currentUserService;
        private readonly AppSettings _appSettings;

        public SolicitudesCitasController(
            DataContext wiseContext,
            IMapper mapper,
            ICallCenterService callCenter,
            UserResolverService currentUserService,
            IOptions<AppSettings> appSettings
            )
        {
            _wiseContext = wiseContext;
            _mapper = mapper;
            _callCenter = callCenter;
            _currentUserService = currentUserService;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("")]
        public IActionResult registrarSolicitud([FromBody] RegistroSolicitudCitaMedicaDto solicitudRecibida)
        {
            Solicitudcitasmedicas solicitud = _mapper.Map<Solicitudcitasmedicas>(solicitudRecibida);
            solicitud.CodigoSoliCitaMedica = ShortGuid.NewGuid().Value;
            if (solicitud.SucursalesSoliCitaMedica == "")
            {
                solicitud.SucursalesSoliCitaMedica = null;
            }
            Subcampanias subcampania = _wiseContext.Subcampanias.Find(solicitud.SubCampaniasOrigen);
            solicitud.SubCampaniasOrigen = subcampania?.CodigoSubCampania;
            solicitud.FechaRegistroSoliCitaMedica = DateTime.Now.ToTimeZoneTime();

            _wiseContext.Solicitudcitasmedicas.Add(solicitud);
            if (_wiseContext.SaveChanges() > 0)
            {
                VerSolicitudCitaMedicaDto solicitudDto = _mapper.Map<VerSolicitudCitaMedicaDto>(solicitud);
                return Ok(solicitudDto);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult GetAll(int p = 1, string ciudad = "", string sucursal = "", DateTime? fechaRegistro = null, DateTime? fechaRegistroInicio = null, DateTime? fechaRegistroFinal = null, bool aplicarfiltrofecha = false)
        {
            Expression<Func<Solicitudcitasmedicas, bool>> condicionSolicitud = s => s.EstadoSoliCitaMedica == (int)EstadoSolicitudCita.PENDIENTE;

            if (!string.IsNullOrEmpty(sucursal))
            {
                condicionSolicitud = condicionSolicitud.And(s => s.SucursalesSoliCitaMedica == sucursal);
            }

            if (!string.IsNullOrEmpty(ciudad))
            {
                condicionSolicitud = condicionSolicitud.And(s => s.CiudadesSoliCitaMedica == ciudad);
            }

            if (fechaRegistro != null)
            {
                condicionSolicitud = condicionSolicitud.And(s => s.FechaRegistroSoliCitaMedica.Date == fechaRegistro.Value.Date);
            }
            if (aplicarfiltrofecha)
            {
                condicionSolicitud = condicionSolicitud.And(s => (s.FechaRegistroSoliCitaMedica.Date > fechaRegistroInicio.Value.Date) && (s.FechaRegistroSoliCitaMedica.Date < fechaRegistroFinal.Value.Date));
            }

            string empleadoActual = _currentUserService.GetCode();

            ResultadoPaginado<VerSolicitudCitaMedicaDto> solicitudesDtos = _wiseContext
                                    .Solicitudcitasmedicas
                                    .Include(s => s.CiudadesSoliCitaMedicaNavigation)
                                        .ThenInclude(ciudad => ciudad.ProvinciasLocalizacionCantonNavigation)
                                            .ThenInclude(provincia => provincia.PaisesLocalizacionProvinciaNavigation)
                                    .Include(s => s.Citasmedicas)
                                        .ThenInclude(c => c.ClientesCitaMedicaNavigation)
                                    .Include(s => s.Citasmedicas)
                                        .ThenInclude(c => c.AgendasCitaMedicaNavigation)
                                                .ThenInclude(a => a.EmpleadosAgendaNavigation)
                                    .Include(s => s.Citasmedicas)
                                        .ThenInclude(c => c.AgendasCitaMedicaNavigation)
                                            .ThenInclude(a => a.TiposAgendasAgendaNavigation)
                                    .Include(s => s.Citasmedicas)
                                        .ThenInclude(c => c.FuentesRemisionCitaMedicaNavigation)
                                    .Include(s => s.RelacionesRepresentantePacienteSoliCitaMedicaNavigation)
                                    .Include(s => s.SucursalesSoliCitaMedicaNavigation)
                                    .Include(s => s.SubCampaniasOrigenNavigation)
                                    .Where(condicionSolicitud)
                                    .Where(s => string.IsNullOrEmpty(s.SucursalesSoliCitaMedica) ||
                                                s.SucursalesSoliCitaMedicaNavigation
                                                .Permisossucursalagendar
                                                .Any(ps => ps.EmpleadosPermisoSucursalAgendar == empleadoActual))
                                    .OrderBy(s => s.Citasmedicas.Count)
                                    .ThenBy(s => s.FechaRegistroSoliCitaMedica)
                                    .GetPaged<Solicitudcitasmedicas, VerSolicitudCitaMedicaDto>(p, 10, _mapper, AgregarExtrasSolicitudCitaDto);

            ResultadoPaginado<VerSolicitudCitaMedicaDto> solicitudesDtosTotal = _wiseContext
                                   .Solicitudcitasmedicas
                                   .Include(s => s.CiudadesSoliCitaMedicaNavigation)
                                       .ThenInclude(ciudad => ciudad.ProvinciasLocalizacionCantonNavigation)
                                           .ThenInclude(provincia => provincia.PaisesLocalizacionProvinciaNavigation)
                                   .Include(s => s.Citasmedicas)
                                       .ThenInclude(c => c.ClientesCitaMedicaNavigation)
                                  
                                   .Include(s => s.Citasmedicas)
                                       .ThenInclude(c => c.AgendasCitaMedicaNavigation)
                                               .ThenInclude(a => a.EmpleadosAgendaNavigation)
                                   .Include(s => s.Citasmedicas)
                                       .ThenInclude(c => c.AgendasCitaMedicaNavigation)
                                           .ThenInclude(a => a.TiposAgendasAgendaNavigation)
                                   .Include(s => s.Citasmedicas)
                                       .ThenInclude(c => c.FuentesRemisionCitaMedicaNavigation)
                                   .Include(s => s.RelacionesRepresentantePacienteSoliCitaMedicaNavigation)
                                   .Include(s => s.SucursalesSoliCitaMedicaNavigation)
                                   .Include(s => s.SubCampaniasOrigenNavigation)
                                   .Where(condicionSolicitud)
                                   .Where(s => string.IsNullOrEmpty(s.SucursalesSoliCitaMedica) ||
                                               s.SucursalesSoliCitaMedicaNavigation
                                               .Permisossucursalagendar
                                               .Any(ps => ps.EmpleadosPermisoSucursalAgendar == empleadoActual))
                                   .OrderBy(s => s.Citasmedicas.Count)
                                   .ThenBy(s => s.FechaRegistroSoliCitaMedica)
                                   .GetPaged<Solicitudcitasmedicas, VerSolicitudCitaMedicaDto>(p, solicitudesDtos.CantidadPaginas* 10, _mapper, AgregarExtrasSolicitudCitaDto);
            string cadena = citasAtendidas();
            List<String>  listaCitasAtendidas= _wiseContext.Citasmedicas.FromSqlRaw(cadena).Select(x => x.SolicitudesCitaMedica).ToList();
            solicitudesDtosTotal.Resultados = solicitudesDtosTotal.Resultados.Where(item => listaCitasAtendidas.All(id => id != (item.cita != null ? item.cita.codigoSolicitudCitaMedica:"") ))
                                                        .ToList();
            return Ok(solicitudesDtosTotal);
        }

        public string citasAtendidas()
        {
            string cadena = "SELECT citasmedicas.CodigoCitaMedica,citasmedicas.FechaRegistroCitaMedica ,citasmedicas.SolicitudesCitaMedica, agendas.CodigoAgenda, Max( citasmedicas.FechaRegistroCitaMedica ), citasmedicas.ActivaCitaMedica, citasmedicas.ClientesCitaMedica, citasmedicas.DiagnosticoCitaMedica, citasmedicas.PacienteLLegoCitaMedica, citasmedicas.TipoCitaMedica, citasmedicas.SubCampaniasOrigen, citasmedicas.codigoGrupoCitaMedica, citasmedicas.AgendasCitaMedica, citasmedicas.FuentesRemisionCitaMedica ";
            cadena += "FROM citasmedicas ";
            cadena += "INNER JOIN agendas ON citasmedicas.AgendasCitaMedica = agendas.CodigoAgenda ";
            cadena += "where EstadoAgenda = '1' and SolicitudesCitaMedica ";
            cadena += "GROUP BY codigoGrupoCitaMedica ";
            return cadena;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            Solicitudcitasmedicas solicitud = _wiseContext.Solicitudcitasmedicas
                                                        .Where(sc => sc.CodigoSoliCitaMedica == id)
                                                        .Include(s => s.CiudadesSoliCitaMedicaNavigation)
                                                            .ThenInclude(ciudad => ciudad.ProvinciasLocalizacionCantonNavigation)
                                                                .ThenInclude(provincia => provincia.PaisesLocalizacionProvinciaNavigation)
                                                        .Include(s => s.Citasmedicas)
                                                            .ThenInclude(c => c.ClientesCitaMedicaNavigation)
                                                        .Include(s => s.Citasmedicas)
                                                            .ThenInclude(c => c.AgendasCitaMedicaNavigation)
                                                                    .ThenInclude(a => a.EmpleadosAgendaNavigation)
                                                        .Include(s => s.Citasmedicas)
                                                            .ThenInclude(c => c.AgendasCitaMedicaNavigation)
                                                                .ThenInclude(a => a.TiposAgendasAgendaNavigation)
                                                        .Include(s => s.Citasmedicas)
                                                            .ThenInclude(c => c.FuentesRemisionCitaMedicaNavigation)
                                                        .Include(s => s.RelacionesRepresentantePacienteSoliCitaMedicaNavigation)
                                                        .Include(s => s.SucursalesSoliCitaMedicaNavigation)
                                                        .Include(s => s.SubCampaniasOrigenNavigation)
                                                        .FirstOrDefault();
            if (solicitud != null)
            {
                VerSolicitudCitaMedicaDto solicitudDto = AgregarExtrasSolicitudCitaDto(solicitud);
                return Ok(solicitudDto);
            }
            else
            {
                return NoContent();
            }
        }

        private VerSolicitudCitaMedicaDto AgregarExtrasSolicitudCitaDto(Solicitudcitasmedicas solicitud)
        {
            string codigoEmpresa = solicitud.SucursalesSoliCitaMedicaNavigation?.EmpresasSucursal ?? _appSettings.EmpresaDefecto;

            Parametros parametros = _wiseContext.Parametros
                                                .Include(p => p.CategoriaDocumentoLlamadaCallCenterParametroNavigation)
                                                .Where(p => p.EmpresasParametro == codigoEmpresa)
                                                .FirstOrDefault();

            IEnumerable<Ordeninstalacion> ordenes = _wiseContext.Ordeninstalacion
                                                                .Include(o => o.Instalacionescabecera)
                                                                .Where(o => o.ContratoCabeceraOrdenInstalacion == solicitud.CodigoSoliCitaMedica)
                                                                .OrderBy(o => o.FechaRegistroOrdenInstalacion);

            VerSolicitudCitaMedicaDto solicitudDto = _mapper.Map<VerSolicitudCitaMedicaDto>(solicitud);
            Citasmedicas cita = solicitud.Citasmedicas
                                .OrderByDescending(c => c.FechaRegistroCitaMedica)
                                .FirstOrDefault();


            solicitudDto.cita = _mapper.Map<VerCitaMedicaDto>(cita);
            if (cita != null)
            {
                solicitudDto.cantidadAgendas = solicitud.Citasmedicas.Count();
                solicitudDto.cita.cantidadReagendados = solicitudDto.cantidadAgendas > 0 ? solicitudDto.cantidadAgendas - 1 : 0;
                solicitudDto.cita.activa = cita.ActivaCitaMedica == 1;
            }
            else
            {
                solicitudDto.cantidadAgendas = 0;
            }

            int cantidadMovimientos = 0;

            Ordeninstalacion ultimaOrden = ordenes.LastOrDefault();
            if (ultimaOrden != null)
            {
                Instalacionescabecera ultimaInstalacion = ultimaOrden.Instalacionescabecera
                                                                    .OrderBy(i => i.FechaRegistroAsignacionInstalacionesCabecera)
                                                                    .LastOrDefault();
                if (ultimaInstalacion != null)
                {
                    if (ultimaInstalacion.CategoriasTiposDocumentosInstalacionesCabecera == parametros.CategoriaDocumentoLlamadaCallCenterParametro)
                    {
                        while (ultimaInstalacion != null)
                        {
                            solicitudDto.cantidadLlamadasUltimoProceso += 1;
                            ultimaInstalacion = ultimaOrden.Instalacionescabecera
                                                            .Where(i => i.CodigoInstalacionesCabecera == ultimaInstalacion.CodigoPadreInstalacionesCabecera
                                                                    && i.CategoriasTiposDocumentosInstalacionesCabecera == parametros.CategoriaDocumentoLlamadaCallCenterParametro
                                                                    )
                                                            .FirstOrDefault();
                        }
                    }
                }


                foreach (Ordeninstalacion orden in ordenes)
                {
                    cantidadMovimientos += orden.Instalacionescabecera.Count(i => i.CategoriasTiposDocumentosInstalacionesCabecera != parametros.CategoriaDocumentoLlamadaCallCenterParametro);
                }
            }

            solicitudDto.cantidadMovimientos = cantidadMovimientos;
            return solicitudDto;
        }

        [HttpPost("{idSolicitud}/agendar")]
        public IActionResult PostAgendar(string idSolicitud, AgendarCitaDto datosAgenda)
        {
            Solicitudcitasmedicas solicitudCitaMedica = _wiseContext.Solicitudcitasmedicas.Find(idSolicitud);
            if (solicitudCitaMedica != null)
            {
                Empleados empleadoAgendar = _wiseContext.Empleados.Find(datosAgenda.agenda.doctorAtiende);
                if (empleadoAgendar != null)
                {
                    Agendas agendaNueva = new Agendas()
                    {
                        CodigoAgenda = ShortGuid.NewGuid().Value,
                        EmpleadosAgendaNavigation = empleadoAgendar,
                        TituloAgenda = datosAgenda.agenda.titulo.ToUpper(),
                        DescripcionAgenda = datosAgenda.agenda.descripcion.ToUpper(),
                        FechaInicioAgenda = datosAgenda.agenda.fechaInicio.ToTimeZoneTime(),
                        FechaFinAgenda = datosAgenda.agenda.fechaFin.ToTimeZoneTime(),
                        FechaRegistroAgenda = DateTime.Now.ToTimeZoneTime(),
                        FechaUltimaModificacionAgenda = DateTime.Now.ToTimeZoneTime(),
                        TiposAgendasAgenda = (int)TipoAgenda.CITA_MEDICA,
                        EstadoAgenda = (int)EstadoAgenda.AGENDADO,
                        EsTodoElDiaAgenda = datosAgenda.agenda.esTodoElDia ? 1 : 0,
                        ReglaRecurrenciaAgenda = datosAgenda.agenda.reglaRecurrencia,
                        FechasExluidasRecurrencia = datosAgenda.agenda.fechasExluidasRecurrencia,
                    };

                    if (agendaNueva.FechaInicioAgenda.Date == DateTime.Now.Date)
                    {
                        agendaNueva.EstadoAgenda = (int)EstadoAgenda.AGENDADO_CONFIRMADO;
                    }

                    Citasmedicas citaNueva = new Citasmedicas()
                    {
                        CodigoCitaMedica = ShortGuid.NewGuid().Value,
                        ClientesCitaMedica = datosAgenda.cliente,
                        DiagnosticoCitaMedica = "",
                        PacienteLlegoCitaMedica = 0,
                        SolicitudesCitaMedica = idSolicitud,
                        TipoCitaMedica = datosAgenda.agenda.tipoCitaMedica,
                        ActivaCitaMedica = 1,
                        SubCampaniasOrigen = solicitudCitaMedica.SubCampaniasOrigen,
                        CodigoGrupoCitaMedica = solicitudCitaMedica.CodigoSoliCitaMedica,
                        FechaRegistroCitaMedica = DateTime.Now.ToTimeZoneTime(),
                        AgendasCitaMedicaNavigation = agendaNueva,
                    };

                    if (datosAgenda.agenda.fuenteRemision != "")
                    {
                        citaNueva.FuentesRemisionCitaMedica = datosAgenda.agenda.fuenteRemision;
                    }

                    _wiseContext.Agendas.Add(agendaNueva);
                    _wiseContext.Citasmedicas.Add(citaNueva);

                    try
                    {
                        _callCenter.grabarCallCenterCitaMedica(citaNueva, idSolicitud);
                    }
                    catch (AppException ex)
                    {
                        return BadRequest(new msjRespuesta { codigo = codigosMensajes.ERROR_AL_GRABAR, detalle = $"No se ha podido grabar el call center de la cita médica {ex.Message}" });
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(new msjRespuesta { codigo = codigosMensajes.ERROR_AL_GRABAR, detalle = $"No se ha podido grabar el call center de la cita médica {ex.Message}" });
                    }

                    try
                    {
                        _wiseContext.SaveChanges();
                        Agendas agendaCreada = _wiseContext.Agendas
                                                .Include(a => a.EmpleadosAgendaNavigation)
                                                .Include(a => a.TiposAgendasAgendaNavigation)
                                                .Include(a => a.Citasmedicas)
                                                .Where(a => a.CodigoAgenda == agendaNueva.CodigoAgenda)
                                                .FirstOrDefault();
                        return Created("", _mapper.Map<VerAgendaCitaMedicaDto>(agendaCreada));
                    }
                    catch (Exception)
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    return BadRequest(new msjRespuesta { codigo = codigosMensajes.NO_SE_PUEDE_ENCONTRAR, detalle = "No se ha podido recuperar la información del empleado asignado" });
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("{idSolicitud}/registrarLlamada")]
        public IActionResult PostRegistrarLlamada(string idSolicitud, RegistrarLlamadaDto datosLlamada)
        {
            Solicitudcitasmedicas solicitudCitaMedica = _wiseContext.Solicitudcitasmedicas
                                                                    .Include(s => s.SucursalesSoliCitaMedicaNavigation)
                                                                    .ThenInclude(su => su.EmpresasSucursalNavigation)
                                                                    .Where(s => s.CodigoSoliCitaMedica == idSolicitud)
                                                                    .FirstOrDefault();
            if (solicitudCitaMedica != null)
            {
                try
                {
                    _callCenter.grabarLlamadaSolicitudCita(solicitudCitaMedica, datosLlamada);
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest();
                }
            }
            else
            {
                return NotFound();
            }
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteById(string id)
        {
            Solicitudcitasmedicas solicitud = _wiseContext
                .Solicitudcitasmedicas
                .Find(id);

            if (solicitud != null)
            {
                solicitud.EstadoSoliCitaMedica = (int)EstadoSolicitudCita.CANCELADA;
                _wiseContext.Solicitudcitasmedicas.Update(solicitud);
                try
                {
                    _wiseContext.SaveChanges();
                    return Ok(new msjRespuesta());
                }
                catch (Exception ex)
                {
                    return BadRequest(new msjRespuesta { codigo = codigosMensajes.ERROR_AL_GRABAR, detalle = $"No se ha podido dar de baja la solicitud de cita médica {ex.Message}" });
                }
            }
            else
            {
                return NotFound();
            }
        }

    }
}