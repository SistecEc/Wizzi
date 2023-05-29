using AutoMapper;
using CSharpVitamins;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Wizzi.Constants;
using Wizzi.Dtos.Agendas;
using Wizzi.Dtos.Movimientos;
using Wizzi.Entities;
using Wizzi.Enums;
using Wizzi.Extensions;
using Wizzi.Helpers;
using Wizzi.Interfaces;
using Wizzi.Models;
using Z.EntityFramework.Plus;

namespace Wizzi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class GruposCitasMedicasController : ControllerBase
    {
        private DataContext _wiseContext;
        private IMapper _mapper;
        private ICallCenterService _callCenter;

        public GruposCitasMedicasController(
            DataContext wiseContext,
            IMapper mapper,
            ICallCenterService callCenter
            )
        {
            _wiseContext = wiseContext;
            _mapper = mapper;
            _callCenter = callCenter;
        }

        [HttpGet("{idGrupoCitas}/agendas")]
        public IActionResult GetReagendas(string idGrupoCitas)
        {
            IEnumerable<Agendas> agendasXcita = _wiseContext.Citasmedicas
                .Where(c => c.CodigoGrupoCitaMedica == idGrupoCitas)
                .Include(c => c.AgendasCitaMedicaNavigation)
                    .ThenInclude(a => a.EmpleadosAgendaNavigation)
                .OrderBy(a => a.FechaRegistroCitaMedica)
                .Select(c => c.AgendasCitaMedicaNavigation);
            if (agendasXcita != null)
            {
                return Ok(_mapper.Map<List<VerAgendaDto>>(agendasXcita));
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("{idGrupoCitas}/movimientos")]
        public IActionResult GetMovimientos(string idGrupoCitas)
        {

            string codigoDocumentoPadreOrden = _wiseContext
                                                .Instalacionescabecera
                                                .Include(i => i.OrdenInstalacionInstalacionesCabeceraNavigation)
                                                .Where(i => i.DocumentoOrigenInstalacionesCabecera == idGrupoCitas)
                                                .OrderByDescending(i => i.FechaRegistroAsignacionInstalacionesCabecera)
                                                .Select(i => i.OrdenInstalacionInstalacionesCabeceraNavigation.ContratoCabeceraOrdenInstalacion)
                                                .FirstOrDefault();

            codigoDocumentoPadreOrden = codigoDocumentoPadreOrden ?? idGrupoCitas;

            List<Ordeninstalacion> ordenes = _wiseContext
                                                .Ordeninstalacion
                                                .Include(o => o.Instalacionescabecera)
                                                    .ThenInclude(i => i.EmpleadoInstalacionesCabeceraNavigation)
                                                .Where(o => o.ContratoCabeceraOrdenInstalacion == codigoDocumentoPadreOrden)
                                                .OrderBy(o => o.FechaRegistroOrdenInstalacion)
                                                .ToList();

            List<VerMovimientosDto> movimientos = new List<VerMovimientosDto>();

            if (ordenes.Count > 0)
            {
                List<string> codigosEmpleadosGeneranInstalaciones = ordenes
                                                                    .SelectMany(o => o.Instalacionescabecera)
                                                                    .Select(i => i.UsuarioAsignaInstalacionesCabecera)
                                                                    .Distinct()
                                                                    .ToList();
                List<Empleados> empleadosAgendan = _wiseContext
                                                    .Empleados
                                                    .Where(e => codigosEmpleadosGeneranInstalaciones.Contains(e.NombreUsuarioEmpleado))
                                                    .ToList();

                foreach (Ordeninstalacion orden in ordenes)
                {

                    Parametros parametros = _wiseContext.Parametros
                                                        .Include(p => p.CategoriaDocumentoLlamadaCallCenterParametroNavigation)
                                                            .ThenInclude(c => c.TiposDocumentosInstalacionesCategoriaTiposDocumentoInstalacionesNavigation)
                                                        .Where(p => p.EmpresasParametro == orden.EmpresaOrdenInstalacion)
                                                        .FirstOrDefault();

                    Categoriastiposdocumentosinstalaciones categoriaTipoDocumentoLlamada = parametros.CategoriaDocumentoLlamadaCallCenterParametroNavigation;

                    foreach (Instalacionescabecera instalacion in orden.Instalacionescabecera.OrderBy(i => i.FechaRegistroAsignacionInstalacionesCabecera))
                    {
                        Empleados empleadoAsignaInstalacion = empleadosAgendan.FirstOrDefault(e => e.NombreUsuarioEmpleado == instalacion.UsuarioAsignaInstalacionesCabecera);
                        if (instalacion.CategoriasTiposDocumentosInstalacionesCabecera == categoriaTipoDocumentoLlamada.CodigoCategoriasTiposDocumentosInstalaciones)
                        {
                            movimientos.Add(new VerMovimientosDto
                            {
                                codigo = instalacion.CodigoInstalacionesCabecera,
                                titulo = instalacion.ObservacionInstalacionesCabecera,
                                descripcion = instalacion.DescripcionInstalacionesCabecera,
                                fechaInicio = instalacion.FechaRegistroAsignacionInstalacionesCabecera,
                                fechaFin = instalacion.FechaRegistroAsignacionInstalacionesCabecera,
                                fechaRegistro = instalacion.FechaRegistroAsignacionInstalacionesCabecera,
                                nombreEmpleadoAsignado = empleadoAsignaInstalacion.NombreEmpleado,
                                apellidoEmpleadoAsignado = empleadoAsignaInstalacion.ApellidoEmpleado,
                                usuarioAsigna = instalacion.UsuarioAsignaInstalacionesCabecera,
                                nombreEmpleadoAsigna = empleadoAsignaInstalacion.NombreEmpleado,
                                apellidoEmpleadoAsigna = empleadoAsignaInstalacion.ApellidoEmpleado,
                                fechaUltimaModificacion = instalacion.FechaRegistroAsignacionInstalacionesCabecera,
                                tipoMovimiento = TipoMovimientoCita.LLAMADA,
                                estado = (int)TipoFinalizacionCallCeneter.CORRECTO
                            });
                        }
                        else
                        {
                            Agendas agenda = _wiseContext.Agendas
                                                                .Where(c => c.CodigoAgenda == instalacion.DocumentoOrigenInstalacionesCabecera)
                                                                .Include(a => a.EmpleadosAgendaNavigation)
                                                                .FirstOrDefault();

                            movimientos.Add(new VerMovimientosDto
                            {
                                codigo = instalacion.CodigoInstalacionesCabecera,
                                titulo = instalacion.ObservacionInstalacionesCabecera,
                                descripcion = instalacion.DescripcionInstalacionesCabecera,
                                fechaInicio = agenda.FechaInicioAgenda,
                                fechaFin = agenda.FechaFinAgenda,
                                fechaRegistro = instalacion.FechaRegistroAsignacionInstalacionesCabecera,
                                nombreEmpleadoAsignado = agenda.EmpleadosAgendaNavigation.NombreEmpleado,
                                apellidoEmpleadoAsignado = agenda.EmpleadosAgendaNavigation.ApellidoEmpleado,
                                usuarioAsigna = instalacion.UsuarioAsignaInstalacionesCabecera,
                                nombreEmpleadoAsigna = empleadoAsignaInstalacion.NombreEmpleado,
                                apellidoEmpleadoAsigna = empleadoAsignaInstalacion.ApellidoEmpleado,
                                fechaUltimaModificacion = agenda.FechaUltimaModificacionAgenda,
                                tipoMovimiento = TipoMovimientoCita.AGENDA,
                                estado = agenda.EstadoAgenda
                            });
                        }
                    }

                }
            }

            return Ok(movimientos);
        }

        [HttpPost("{idGrupoCitas}/agendar")]
        public IActionResult PostAgendar(string idGrupoCitas, AgendarCitaDto datosAgenda)
        {
            //TODO: CUANDO ES CITA POR REAGENDAR, ACTUALIZAR ORDEN DE INSTALACION DE ULTIMA CITA A ESTADO CANCELADO
            Citasmedicas ultimaCitaMedica = _wiseContext
                                                .Citasmedicas
                                                .Include(c => c.AgendasCitaMedicaNavigation)
                                                .OrderByDescending(c => c.FechaRegistroCitaMedica)
                                                .Where(c => c.CodigoGrupoCitaMedica == idGrupoCitas)
                                                .FirstOrDefault();

            //Verificar si no es de una agenda directa
            if (ultimaCitaMedica == null)
            {
                ultimaCitaMedica = _wiseContext
                                    .Citasmedicas
                                    .Include(c => c.AgendasCitaMedicaNavigation)
                                    .OrderByDescending(c => c.FechaRegistroCitaMedica)
                                    .Where(c => c.AgendasCitaMedica == idGrupoCitas)
                                    .FirstOrDefault();
            }


            if (ultimaCitaMedica.AgendasCitaMedicaNavigation.EstadoAgenda != (int)EstadoAgenda.AGENDADO &&
                ultimaCitaMedica.AgendasCitaMedicaNavigation.EstadoAgenda != (int)EstadoAgenda.ATENDIDA)
            {
                Empleados doctorAtiende = _wiseContext.Empleados
                                                        .Where(e => e.CodigoEmpleado == datosAgenda.agenda.doctorAtiende)
                                                        .FirstOrDefault();
                if (doctorAtiende != null)
                {
                    Agendas agendaNueva = new Agendas()
                    {
                        CodigoAgenda = ShortGuid.NewGuid().Value,
                        EmpleadosAgendaNavigation = doctorAtiende,
                        TituloAgenda = datosAgenda.agenda.titulo,
                        DescripcionAgenda = datosAgenda.agenda.descripcion,
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

                    Citasmedicas citaNueva = new Citasmedicas()
                    {
                        ClientesCitaMedica = datosAgenda.cliente,
                        CodigoCitaMedica = ShortGuid.NewGuid().Value,
                        DiagnosticoCitaMedica = "",
                        PacienteLlegoCitaMedica = 0,
                        SolicitudesCitaMedica = ultimaCitaMedica.SolicitudesCitaMedica,
                        TipoCitaMedica = datosAgenda.agenda.tipoCitaMedica,
                        ActivaCitaMedica = 1,
                        CodigoGrupoCitaMedica = idGrupoCitas,
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
                        _callCenter.grabarCallCenterCitaMedica(citaNueva, ultimaCitaMedica.AgendasCitaMedicaNavigation.CodigoAgenda, esNuevoInicioProceso: datosAgenda.esNuevoInicioProceso);
                    }
                    catch (AppException ex)
                    {
                        return BadRequest(new msjRespuesta { codigo = codigosMensajes.ERROR_AL_GRABAR, detalle = $"No se ha podido grabar el call center de la cita médica {ex.Message}" });
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(new msjRespuesta { codigo = codigosMensajes.ERROR_AL_GRABAR, detalle = $"No se ha podido grabar el call center de la cita médica {ex.Message}" });
                    }

                    if (_wiseContext.SaveChanges() > 0)
                    {
                        Agendas agendaCreada = _wiseContext.Agendas
                                                .Include(a => a.EmpleadosAgendaNavigation)
                                                .Include(a => a.TiposAgendasAgendaNavigation)
                                                .Include(a => a.Citasmedicas)
                                                .Where(a => a.CodigoAgenda == agendaNueva.CodigoAgenda)
                                                .FirstOrDefault();
                        return Created("", _mapper.Map<VerAgendaCitaMedicaDto>(agendaCreada));
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    return BadRequest();
                }

            }
            else
            {
                return BadRequest(new msjRespuesta { codigo = codigosMensajes.NO_SE_PUEDE_GRABAR, detalle = "La cita cuenta con una agenda vigente" });
            }

        }
    }
}