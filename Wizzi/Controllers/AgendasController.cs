using AutoMapper;
using CSharpVitamins;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Wizzi.Constants;
using Wizzi.Dtos.Agendas;
using Wizzi.Dtos.Llamadas;
using Wizzi.Dtos.Sucursales;
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
    public class AgendasController : ControllerBase
    {
        private DataContext _wiseContext;
        private IMapper _mapper;
        private ICallCenterService _callCenter;
        private IClienteService _clienteService;
        private readonly UserResolverService _currentUserService;

        public AgendasController(
            DataContext wiseContext,
            IMapper mapper,
            ICallCenterService callCenter,
            IClienteService clienteService,
            UserResolverService currentUserService
            )
        {
            _wiseContext = wiseContext;
            _mapper = mapper;
            _callCenter = callCenter;
            _clienteService = clienteService;
            _currentUserService = currentUserService;
        }


        [HttpGet]

        public IActionResult GetAll(DateTime inicio, DateTime fin, string empleado, int p = 1, [FromQuery] List<int> estados = null, string ciudad = "", string sucursal = "")
        {
            inicio = inicio.ToTimeZoneTime();
            fin = fin.ToTimeZoneTime();
            string empleadoActual = _currentUserService.GetCode();
            Expression<Func<Agendas, bool>> condicionAgenda = a => a.FechaInicioAgenda >= inicio && a.FechaFinAgenda <= fin;
            if (empleado != null && empleado != "0")
            {
                condicionAgenda = condicionAgenda.And(a => a.EmpleadosAgenda == empleado);
            }

            if (estados == null)
            {
                //List<int> listaEstados = new List<int>();
                estados.Add((int)EstadoAgenda.AGENDADO_ATENDER);
                //estados = listaEstados.ToArray();
            }

            Expression<Func<Agendas, bool>> condicionEstadoAgenda = a => estados.Contains(a.EstadoAgenda);

            if (!string.IsNullOrEmpty(sucursal))
            {
                condicionAgenda = condicionAgenda.And(e => e.EmpleadosAgendaNavigation.SucursalesEmpleado == sucursal);
            }
            if (!string.IsNullOrEmpty(ciudad))
            {
                condicionAgenda = condicionAgenda.And(e => e.EmpleadosAgendaNavigation.SucursalesEmpleadoNavigation.CiudadesSucursal == ciudad);
            }

            IEnumerable<Agendas> agendas = _wiseContext.Agendas
                                            .AsNoTracking()
                                            .Include(a => a.EmpleadosAgendaNavigation)
                                                .ThenInclude(e => e.SucursalesEmpleadoNavigation)
                                                    .ThenInclude(s => s.PaisSucursalNavigation)
                                            .Include(a => a.EmpleadosAgendaNavigation)
                                                .ThenInclude(e => e.SucursalesEmpleadoNavigation)
                                                    .ThenInclude(s => s.ProvinciaSucursalNavigation)
                                            .Include(a => a.EmpleadosAgendaNavigation)
                                                .ThenInclude(e => e.SucursalesEmpleadoNavigation)
                                                    .ThenInclude(s => s.CiudadesSucursalNavigation)
                                            .Include(a => a.EmpleadosAgendaNavigation)
                                                .ThenInclude(e => e.SucursalesEmpleadoNavigation)
                                                    .ThenInclude(s => s.ParroquiaSucursalNavigation)
                                            .Include(a => a.TiposAgendasAgendaNavigation)
                                            .Include(a => a.Citasmedicas)
                                                .ThenInclude(c => c.ClientesCitaMedicaNavigation)
                                            .Where(condicionAgenda)
                                            .Where(condicionEstadoAgenda)
                                            .Where(a => a.EmpleadosAgendaNavigation.SucursalesEmpleadoNavigation.Permisossucursalagendar.Any(ps => ps.EmpleadosPermisoSucursalAgendar == empleadoActual))
                                            .ToList();
            IEnumerable<VerAgendaDto> agendasDto = _mapper.Map<IEnumerable<VerAgendaCitaMedicaDto>>(agendas);
            return Ok(agendasDto);
        }

        [HttpGet("buscar")]
        public IActionResult GetBuscarAgendas(string q, string ciudad = "", string sucursal = "", string empleado = "")
        {
            if (!string.IsNullOrEmpty(q))
            {
                List<Clientes> clientesFiltro = _clienteService.Buscar(q);
                List<VerAgendaCitaMedicaDto> agendasResultado = new List<VerAgendaCitaMedicaDto>();

                if (clientesFiltro.Count > 0)
                {
                    string empleadoActual = _currentUserService.GetCode();

                    List<string> codigosClientes = clientesFiltro.Select(c => c.CodigoCliente)
                                                                .ToList();
                    Expression<Func<Agendas, bool>> condicionAgenda = a => codigosClientes.Contains(a.Citasmedicas.ClientesCitaMedica);
                    if (!string.IsNullOrEmpty(sucursal))
                    {
                        condicionAgenda = condicionAgenda.And(e => e.EmpleadosAgendaNavigation.SucursalesEmpleado == sucursal);
                    }
                    if (!string.IsNullOrEmpty(ciudad))
                    {
                        condicionAgenda = condicionAgenda.And(e => e.EmpleadosAgendaNavigation.SucursalesEmpleadoNavigation.CiudadesSucursal == ciudad);
                    }

                    List<Agendas> agendas = _wiseContext.Agendas
                                                        .AsNoTracking()
                                                        .Include(a => a.EmpleadosAgendaNavigation)
                                                            .ThenInclude(e => e.SucursalesEmpleadoNavigation)
                                                                .ThenInclude(s => s.PaisSucursalNavigation)
                                                        .Include(a => a.EmpleadosAgendaNavigation)
                                                            .ThenInclude(e => e.SucursalesEmpleadoNavigation)
                                                                .ThenInclude(s => s.ProvinciaSucursalNavigation)
                                                        .Include(a => a.EmpleadosAgendaNavigation)
                                                            .ThenInclude(e => e.SucursalesEmpleadoNavigation)
                                                                .ThenInclude(s => s.CiudadesSucursalNavigation)
                                                        .Include(a => a.EmpleadosAgendaNavigation)
                                                            .ThenInclude(e => e.SucursalesEmpleadoNavigation)
                                                                .ThenInclude(s => s.ParroquiaSucursalNavigation)
                                                        .Include(a => a.TiposAgendasAgendaNavigation)
                                                        .Include(a => a.Citasmedicas)
                                                            .ThenInclude(c => c.ClientesCitaMedicaNavigation)
                                                        .Where(condicionAgenda)
                                                        .Where(a => a.EmpleadosAgendaNavigation
                                                                        .SucursalesEmpleadoNavigation
                                                                        .Permisossucursalagendar
                                                                        .Any(ps => ps.EmpleadosPermisoSucursalAgendar == empleadoActual)
                                                                )
                                                        .OrderBy(a => a.FechaInicioAgenda)
                                                        .ToList();

                    agendasResultado = _mapper.Map<List<VerAgendaCitaMedicaDto>>(agendas);
                    return Ok(agendasResultado);
                }
                return Ok(agendasResultado);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("porConfirmar/{id}")]
        public IActionResult GetAgendasPorConfirmarById(string id)
        {
            Expression<Func<Agendas, bool>> condicionAgenda = a => a.EstadoAgenda == (int)EstadoAgenda.AGENDADO
                                                                    && a.CodigoAgenda == id;

            Agendas agenda = queryAgendasPorCompletarConfirmar()
                                .Where(condicionAgenda)
                                .FirstOrDefault();

            VerAgendaCitaMedicaDto agendaDto = AgregarExtrasAgendaDto(agenda);
            return Ok(agendaDto);
        }

        [HttpGet("porConfirmar")]
        public IActionResult GetAgendasPorConfirmarAll(int p = 1, string ciudad = "", string sucursal = "", DateTime? fechaRegistroInicio = null, DateTime? fechaRegistroFinal = null, bool aplicarfiltrofecha = false)
        {
            Expression<Func<Agendas, bool>> condicionAgenda = a => a.EstadoAgenda == (int)EstadoAgenda.AGENDADO;
            if (aplicarfiltrofecha)
            {
                condicionAgenda = condicionAgenda.And(s => (s.FechaInicioAgenda.Date > fechaRegistroInicio.Value.Date) && (s.FechaInicioAgenda.Date < fechaRegistroFinal.Value.Date));
            }

            ResultadoPaginado<VerAgendaCitaMedicaDto> agendasDto = queryAgendasPorCompletarConfirmar(ciudad, sucursal)
                                                                    .Where(condicionAgenda)
                                                                    .OrderBy(a => a.FechaInicioAgenda)
                                                                    .GetPaged<Agendas, VerAgendaCitaMedicaDto>(p, 10, _mapper, AgregarExtrasAgendaDto);

            ResultadoPaginado<VerAgendaCitaMedicaDto> agendasDtos = queryAgendasPorCompletarConfirmar(ciudad, sucursal)
                                                                   .Where(condicionAgenda)
                                                                   .OrderBy(a => a.FechaInicioAgenda)
                                                                   .GetPaged<Agendas, VerAgendaCitaMedicaDto>(p, agendasDto.CantidadPaginas* 10, _mapper, AgregarExtrasAgendaDto);
            return Ok(agendasDtos);
        }

        [HttpGet("porCompletar/{id}")]
        public IActionResult GetAgendasPorCompletarById(string id)
        {
            Expression<Func<Agendas, bool>> condicionAgenda = a => a.EstadoAgenda == (int)EstadoAgenda.AGENDADO_CONFIRMADO
                                                                    && a.CodigoAgenda == id;

            Agendas agenda = queryAgendasPorCompletarConfirmar()
                                .Where(condicionAgenda)
                                .FirstOrDefault();

            VerAgendaCitaMedicaDto agendaDto = AgregarExtrasAgendaDto(agenda);

            return Ok(agendaDto);
        }

        
        [HttpGet("porCompletar")]
        public IActionResult GetAgendasPorCompletarAll(int p = 1, string ciudad = "", string sucursal = "", DateTime? fechaRegistroInicio = null, DateTime? fechaRegistroFinal = null,bool aplicarfiltrofecha = false)
        {
            Expression<Func<Agendas, bool>> condicionAgenda = a => a.EstadoAgenda == (int)EstadoAgenda.AGENDADO_CONFIRMADO;
            if (aplicarfiltrofecha)
            {
                condicionAgenda = condicionAgenda.And(s => (s.FechaInicioAgenda.Date > fechaRegistroInicio.Value.Date) && (s.FechaInicioAgenda.Date < fechaRegistroFinal.Value.Date));
            }

            ResultadoPaginado<VerAgendaCitaMedicaDto> agendasDto = queryAgendasPorCompletarConfirmar(ciudad, sucursal)
                                                                    .Where(condicionAgenda)
                                                                    .OrderBy(a => a.FechaInicioAgenda)
                                                                    .GetPaged<Agendas, VerAgendaCitaMedicaDto>(p, 10, _mapper, AgregarExtrasAgendaDto);

            ResultadoPaginado<VerAgendaCitaMedicaDto> agendasDtos = queryAgendasPorCompletarConfirmar(ciudad, sucursal)
                                                                   .Where(condicionAgenda)
                                                                   .OrderBy(a => a.FechaInicioAgenda)
                                                                   .GetPaged<Agendas, VerAgendaCitaMedicaDto>(p, agendasDto.CantidadPaginas* 10, _mapper, AgregarExtrasAgendaDto);
            return Ok(agendasDtos);
        }

        private IQueryable<Agendas> queryAgendasPorCompletarConfirmar(string ciudad = "", string sucursal = "")
        {
            string empleadoActual = _currentUserService.GetCode();
            Expression<Func<Agendas, bool>> condicionAgenda = a => true;
            if (!string.IsNullOrEmpty(sucursal))
            {
                condicionAgenda = condicionAgenda.And(e => e.EmpleadosAgendaNavigation.SucursalesEmpleado == sucursal);
            }
            if (!string.IsNullOrEmpty(ciudad))
            {
                condicionAgenda = condicionAgenda.And(e => e.EmpleadosAgendaNavigation.SucursalesEmpleadoNavigation.CiudadesSucursal == ciudad);
            }

            return _wiseContext
                    .Agendas
                    .AsNoTracking()
                    .Include(a => a.EmpleadosAgendaNavigation)
                        .ThenInclude(e => e.SucursalesEmpleadoNavigation)
                            .ThenInclude(s => s.PaisSucursalNavigation)
                    .Include(a => a.EmpleadosAgendaNavigation)
                        .ThenInclude(e => e.SucursalesEmpleadoNavigation)
                            .ThenInclude(s => s.ProvinciaSucursalNavigation)
                    .Include(a => a.EmpleadosAgendaNavigation)
                        .ThenInclude(e => e.SucursalesEmpleadoNavigation)
                            .ThenInclude(s => s.CiudadesSucursalNavigation)
                    .Include(a => a.EmpleadosAgendaNavigation)
                        .ThenInclude(e => e.SucursalesEmpleadoNavigation)
                            .ThenInclude(s => s.ParroquiaSucursalNavigation)
                    .Include(a => a.TiposAgendasAgendaNavigation)
                    .Include(a => a.Citasmedicas)
                        .ThenInclude(c => c.ClientesCitaMedicaNavigation)
                    .Where(condicionAgenda)
                    .Where(a => a.EmpleadosAgendaNavigation
                                    .SucursalesEmpleadoNavigation
                                    .Permisossucursalagendar
                                    .Any(ps => ps.EmpleadosPermisoSucursalAgendar == empleadoActual)
                            );
        }

        private VerAgendaCitaMedicaDto AgregarExtrasAgendaDto(Agendas agenda)
        {
            int cantidadMovimientos = 0;

            //Obtenemos el código de documento de origen de la agenda, evitamos el problema de nulo si fue reagendada dentro del mismo proceso
            string codigoDocumentoOrigen = _wiseContext
                                            .Instalacionescabecera
                                            .Include(i => i.OrdenInstalacionInstalacionesCabeceraNavigation)
                                            .Where(i => i.DocumentoOrigenInstalacionesCabecera == agenda.CodigoAgenda)
                                            .Select(i => i.OrdenInstalacionInstalacionesCabeceraNavigation.ContratoCabeceraOrdenInstalacion)
                                            .FirstOrDefault();

            List<Ordeninstalacion> ordenes = _wiseContext.Ordeninstalacion
                                                        .Include(o => o.Instalacionescabecera)
                                                        .Where(o => o.ContratoCabeceraOrdenInstalacion == codigoDocumentoOrigen)
                                                        .OrderBy(o => o.FechaRegistroOrdenInstalacion)
                                                        .ToList();
            VerAgendaCitaMedicaDto verAgendaDto = _mapper.Map<VerAgendaCitaMedicaDto>(agenda);
            verAgendaDto.sucursal = _mapper.Map<VerSucursalDto>(agenda.EmpleadosAgendaNavigation.SucursalesEmpleadoNavigation);

            Ordeninstalacion ultimaOrden = ordenes.LastOrDefault();

            if (ultimaOrden != null)
            {
                Parametros parametros = _wiseContext.Parametros
                                                    .Include(p => p.CategoriaDocumentoLlamadaCallCenterParametroNavigation)
                                                    .Where(p => p.EmpresasParametro == ultimaOrden.EmpresaOrdenInstalacion)
                                                    .FirstOrDefault();

                Instalacionescabecera ultimaInstalacion = ultimaOrden.Instalacionescabecera
                                                                    .OrderBy(i => i.FechaRegistroAsignacionInstalacionesCabecera)
                                                                    .LastOrDefault();
                if (ultimaInstalacion != null)
                {
                    if (ultimaInstalacion.CategoriasTiposDocumentosInstalacionesCabecera == parametros.CategoriaDocumentoLlamadaCallCenterParametro)
                    {
                        while (ultimaInstalacion != null)
                        {
                            verAgendaDto.cantidadLlamadasUltimoProceso += 1;
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

            verAgendaDto.cantidadMovimientos = cantidadMovimientos;
            return verAgendaDto;
        }

        [HttpPatch("{id}")]
        public IActionResult ActualizarAgenda(string id, RegistrarAgendaDto datosAgenda)
        {
            Agendas agendaGrabada = _wiseContext.Agendas
                                    .Include(a => a.Citasmedicas)
                                    .Include(a => a.EmpleadosAgendaNavigation)
                                    .Where(a => a.CodigoAgenda == id)
                                    .FirstOrDefault();
            if (agendaGrabada != null)
            {
                Empleados empleadoAgendar = _wiseContext.Empleados.Find(datosAgenda.doctorAtiende);
                if (empleadoAgendar != null)
                {
                    agendaGrabada.FechaUltimaModificacionAgenda = DateTime.Now.ToTimeZoneTime();
                    agendaGrabada.EstadoAgenda = (int)EstadoAgenda.REAGENDADA;

                    Citasmedicas citaMedicaGrabada = agendaGrabada.Citasmedicas;
                    if (citaMedicaGrabada != null)
                    {
                        citaMedicaGrabada.ActivaCitaMedica = 0;
                        Agendas agendaNueva = new Agendas()
                        {
                            CodigoAgenda = ShortGuid.NewGuid().Value,
                            EmpleadosAgendaNavigation = empleadoAgendar,
                            TituloAgenda = datosAgenda.titulo,
                            DescripcionAgenda = datosAgenda.descripcion,
                            FechaInicioAgenda = datosAgenda.fechaInicio.ToTimeZoneTime(),
                            FechaFinAgenda = datosAgenda.fechaFin.ToTimeZoneTime(),
                            FechaRegistroAgenda = DateTime.Now.ToTimeZoneTime(),
                            FechaUltimaModificacionAgenda = DateTime.Now.ToTimeZoneTime(),
                            TiposAgendasAgenda = (int)TipoAgenda.CITA_MEDICA,
                            EstadoAgenda = (int)EstadoAgenda.AGENDADO,
                            EsTodoElDiaAgenda = datosAgenda.esTodoElDia ? 1 : 0,
                            ReglaRecurrenciaAgenda = datosAgenda.reglaRecurrencia,
                            FechasExluidasRecurrencia = datosAgenda.fechasExluidasRecurrencia,
                        };

                        if (agendaNueva.FechaInicioAgenda.Date == DateTime.Now.Date)
                        {
                            agendaNueva.EstadoAgenda = (int)EstadoAgenda.AGENDADO_CONFIRMADO;
                        }

                        Citasmedicas citaNueva = citaMedicaGrabada;
                        citaNueva.CodigoCitaMedica = ShortGuid.NewGuid().Value;
                        citaNueva.PacienteLlegoCitaMedica = 0;
                        citaNueva.TipoCitaMedica = datosAgenda.tipoCitaMedica;
                        citaNueva.FechaRegistroCitaMedica = agendaNueva.FechaRegistroAgenda;
                        citaNueva.AgendasCitaMedicaNavigation = agendaNueva;

                        if (datosAgenda.fuenteRemision != "")
                        {
                            citaNueva.FuentesRemisionCitaMedica = datosAgenda.fuenteRemision;
                        }

                        _wiseContext.Citasmedicas.Update(citaMedicaGrabada);
                        _wiseContext.Agendas.Update(agendaGrabada);
                        _wiseContext.Agendas.Add(agendaNueva);
                        _wiseContext.Citasmedicas.Add(citaNueva);

                        try
                        {
                            _callCenter.reasignarCallCenterCitaMedica(agendaGrabada, citaNueva);
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
                                                        .ThenInclude(c => c.ClientesCitaMedicaNavigation)
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
                    return BadRequest(new msjRespuesta { codigo = codigosMensajes.NO_SE_PUEDE_GRABAR, detalle = "No se ha podido recuperar la información del empleado asignado" });
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}/confirmarAsistencia")]
        public IActionResult ConfirmarAsistencia(string id, RegistrarLlamadaDto confirmarAsistenciaDto)
        {
            Agendas agendaGrabada = _wiseContext.Agendas
                                                .Include(a => a.Citasmedicas)
                                                    .ThenInclude(c => c.SolicitudesCitaMedicaNavigation)
                                                .Include(a => a.EmpleadosAgendaNavigation)
                                                    .ThenInclude(e => e.SucursalesEmpleadoNavigation)
                                                        .ThenInclude(s => s.EmpresasSucursalNavigation)
                                                .Include(a => a.EmpleadosAgendaNavigation)
                                                .Where(a => a.CodigoAgenda == id)
                                                .FirstOrDefault();
            if (agendaGrabada != null)
            {
                agendaGrabada.EstadoAgenda = (int)EstadoAgenda.AGENDADO_CONFIRMADO;
                _wiseContext.Agendas.Update(agendaGrabada);

                RegistrarLlamadaDto infoLlamada = new RegistrarLlamadaDto()
                {
                    observacion = confirmarAsistenciaDto.observacion
                };
                _callCenter.grabarLlamadaCita(agendaGrabada.Citasmedicas, infoLlamada);
                try
                {
                    _wiseContext.SaveChanges();
                    return Ok();
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}/registrarInasistencia")]
        public IActionResult RegistrarInasistencia(string id, RegistrarInasistenciaDto registrarInasistencia)
        {
            Agendas agendaGrabada = _wiseContext.Agendas
                                                .Include(a => a.Citasmedicas)
                                                    .ThenInclude(c => c.SolicitudesCitaMedicaNavigation)
                                                .Include(a => a.EmpleadosAgendaNavigation)
                                                    .ThenInclude(e => e.SucursalesEmpleadoNavigation)
                                                        .ThenInclude(s => s.EmpresasSucursalNavigation)
                                                .Include(a => a.EmpleadosAgendaNavigation)
                                                .Where(a => a.CodigoAgenda == id)
                                                .FirstOrDefault();

            Ordeninstalacion ordenGrabada = _wiseContext.Instalacionescabecera
                                                .Include(i => i.OrdenInstalacionInstalacionesCabeceraNavigation)
                                                .Where(i => i.DocumentoOrigenInstalacionesCabecera == id)
                                                .Select(i => i.OrdenInstalacionInstalacionesCabeceraNavigation)
                                                .FirstOrDefault();


            if (agendaGrabada != null && ordenGrabada != null)
            {
                agendaGrabada.FechaUltimaModificacionAgenda = DateTime.Now.ToTimeZoneTime();
                agendaGrabada.EstadoAgenda = (int)EstadoAgenda.CANCELADA;
                _wiseContext.Agendas.Update(agendaGrabada);

                RegistrarLlamadaDto infoLlamada = new RegistrarLlamadaDto()
                {
                    observacion = registrarInasistencia.observacion
                };
                _callCenter.grabarLlamadaCita(agendaGrabada.Citasmedicas, infoLlamada);

                cancelarOrdenInstalacion(ordenGrabada);

                if (registrarInasistencia.reagendar)
                {
                    _callCenter.grabarReagendamiento(agendaGrabada, ordenGrabada, infoLlamada);
                }
                else
                {
                    Solicitudcitasmedicas solicitudCita = agendaGrabada.Citasmedicas.SolicitudesCitaMedicaNavigation;
                    if (solicitudCita != null)
                    {
                        solicitudCita.EstadoSoliCitaMedica = (int)EstadoSolicitudCita.CANCELADA;
                        _wiseContext.Solicitudcitasmedicas.Update(solicitudCita);
                    }
                }

                try
                {
                    _wiseContext.SaveChanges();
                    return Ok();
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        private void cancelarOrdenInstalacion(Ordeninstalacion ordenCancelar)
        {
            ordenCancelar.EstadoOrdenInstalacion = (int)EstadoOrdenInstalacion.CANCELADA;
            _wiseContext.Ordeninstalacion.Update(ordenCancelar);
        }

        [HttpPost]
        public IActionResult PostAgendar(AgendarCitaDto datosAgenda)
        {
            Empleados empleadoAgendar = _wiseContext.Empleados.Find(datosAgenda.agenda.doctorAtiende);
            if (empleadoAgendar != null)
            {
                List<Agendas> agendasGrabar = new List<Agendas>();

                if (datosAgenda.agenda.reglaRecurrencia != "")
                {
                    RecurrencePattern patronRecurrencia = new RecurrencePattern(datosAgenda.agenda.reglaRecurrencia);
                    DateTime inicioRecurrencia = datosAgenda.agenda.fechaInicio;
                    DateTime finRecurrencia = inicioRecurrencia.AddYears(10);
                    TimeSpan tiempoEntreFechaInicioFin = datosAgenda.agenda.fechaFin - datosAgenda.agenda.fechaInicio;

                    CalendarEvent cevent = new CalendarEvent
                    {
                        Start = new CalDateTime(inicioRecurrencia),
                        End = new CalDateTime(finRecurrencia),
                        RecurrenceRules = new List<RecurrencePattern> { patronRecurrencia }
                    };
                    foreach (Occurrence recurrencia in cevent.GetOccurrences(inicioRecurrencia, finRecurrencia))
                    {
                        DateTime fechaInicioAgendaRecurrencia = recurrencia.Period.StartTime.Value.ToTimeZoneTime();

                        Citasmedicas citaNueva = new Citasmedicas()
                        {
                            CodigoCitaMedica = ShortGuid.NewGuid().Value,
                            ClientesCitaMedica = datosAgenda.cliente,
                            DiagnosticoCitaMedica = "",
                            PacienteLlegoCitaMedica = 0,
                            TipoCitaMedica = datosAgenda.agenda.tipoCitaMedica,
                            ActivaCitaMedica = 1,
                            CodigoGrupoCitaMedica = ShortGuid.NewGuid().Value,
                            FechaRegistroCitaMedica = DateTime.Now.ToTimeZoneTime(),
                        };

                        if (datosAgenda.agenda.fuenteRemision != "")
                        {
                            citaNueva.FuentesRemisionCitaMedica = datosAgenda.agenda.fuenteRemision;
                        }

                        Agendas agendaNueva = new Agendas()
                        {
                            CodigoAgenda = ShortGuid.NewGuid().Value,
                            EmpleadosAgendaNavigation = empleadoAgendar,
                            TituloAgenda = datosAgenda.agenda.titulo,
                            DescripcionAgenda = datosAgenda.agenda.descripcion,
                            FechaInicioAgenda = fechaInicioAgendaRecurrencia,
                            FechaFinAgenda = fechaInicioAgendaRecurrencia.Add(tiempoEntreFechaInicioFin),
                            FechaRegistroAgenda = DateTime.Now.ToTimeZoneTime(),
                            FechaUltimaModificacionAgenda = DateTime.Now.ToTimeZoneTime(),
                            TiposAgendasAgenda = (int)TipoAgenda.CITA_MEDICA,
                            EstadoAgenda = (int)EstadoAgenda.AGENDADO,
                            EsTodoElDiaAgenda = datosAgenda.agenda.esTodoElDia ? 1 : 0,
                            FechasExluidasRecurrencia = datosAgenda.agenda.fechasExluidasRecurrencia,
                            Citasmedicas = citaNueva
                        };

                        agendasGrabar.Add(agendaNueva);
                    }
                }
                else
                {
                    Citasmedicas citaNueva = new Citasmedicas()
                    {
                        CodigoCitaMedica = ShortGuid.NewGuid().Value,
                        ClientesCitaMedica = datosAgenda.cliente,
                        DiagnosticoCitaMedica = "",
                        PacienteLlegoCitaMedica = 0,
                        TipoCitaMedica = datosAgenda.agenda.tipoCitaMedica,
                        ActivaCitaMedica = 1,
                        CodigoGrupoCitaMedica = ShortGuid.NewGuid().Value,
                        FechaRegistroCitaMedica = DateTime.Now.ToTimeZoneTime(),
                    };

                    if (datosAgenda.agenda.fuenteRemision != "")
                    {
                        citaNueva.FuentesRemisionCitaMedica = datosAgenda.agenda.fuenteRemision;
                    }

                    Agendas agendaNueva = new Agendas()
                    {
                        CodigoAgenda = ShortGuid.NewGuid().Value,
                        EmpleadosAgendaNavigation = empleadoAgendar,
                        TituloAgenda = datosAgenda.agenda.titulo,
                        DescripcionAgenda = datosAgenda.agenda.descripcion,
                        FechaInicioAgenda = datosAgenda.agenda.fechaInicio.ToTimeZoneTime(),
                        FechaFinAgenda = datosAgenda.agenda.fechaFin.ToTimeZoneTime(),
                        FechaRegistroAgenda = DateTime.Now.ToTimeZoneTime(),
                        FechaUltimaModificacionAgenda = DateTime.Now.ToTimeZoneTime(),
                        TiposAgendasAgenda = (int)TipoAgenda.CITA_MEDICA,
                        EstadoAgenda = (int)EstadoAgenda.AGENDADO,
                        EsTodoElDiaAgenda = datosAgenda.agenda.esTodoElDia ? 1 : 0,
                        FechasExluidasRecurrencia = datosAgenda.agenda.fechasExluidasRecurrencia,
                        Citasmedicas = citaNueva
                    };

                    agendasGrabar.Add(agendaNueva);

                }

                int contador = 0;
                foreach (Agendas agendaNueva in agendasGrabar)
                {
                    _wiseContext.Agendas.Add(agendaNueva);
                    _wiseContext.Citasmedicas.Add(agendaNueva.Citasmedicas);

                    try
                    {
                        _callCenter.grabarCallCenterCitaMedica(agendaNueva.Citasmedicas, agendaNueva.CodigoAgenda, numeroIteracion: contador);
                        contador++;
                    }
                    catch (AppException ex)
                    {
                        return BadRequest(new msjRespuesta { codigo = codigosMensajes.ERROR_AL_GRABAR, detalle = $"No se ha podido grabar el call center de la cita médica {ex.Message}" });
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(new msjRespuesta { codigo = codigosMensajes.ERROR_AL_GRABAR, detalle = $"No se ha podido grabar el call center de la cita médica {ex.Message}" });
                    }
                }


                try
                {
                    _wiseContext.SaveChanges();
                    List<Agendas> agendaCreada = _wiseContext.Agendas
                                                            .Include(a => a.EmpleadosAgendaNavigation)
                                                            .Include(a => a.TiposAgendasAgendaNavigation)
                                                            .Include(a => a.Citasmedicas)
                                                                .ThenInclude(c => c.ClientesCitaMedicaNavigation)
                                                            .Where(a => agendasGrabar.Select(a => a.CodigoAgenda).Contains(a.CodigoAgenda))
                                                            .ToList();
                    return Ok(_mapper.Map<List<VerAgendaCitaMedicaDto>>(agendaCreada));
                }
                catch (Exception ex)
                {
                    return BadRequest();
                }
            }
            else
            {
                return NotFound(new msjRespuesta { codigo = codigosMensajes.NO_SE_PUEDE_ENCONTRAR, detalle = "No se ha podido recuperar la información del empleado asignado" });
            }

        }

        [HttpPut("{id}/cancelar")]
        public IActionResult PutCancelarAgenda(string id, CancelarAgendaDto cancelarAgendaDto)
        {
            Agendas agendaGrabada = _wiseContext.Agendas
                                                .Include(a => a.Citasmedicas)
                                                    .ThenInclude(c => c.SolicitudesCitaMedicaNavigation)
                                                .Include(a => a.EmpleadosAgendaNavigation)
                                                    .ThenInclude(e => e.SucursalesEmpleadoNavigation)
                                                        .ThenInclude(s => s.EmpresasSucursalNavigation)
                                                .Include(a => a.EmpleadosAgendaNavigation)
                                                .Where(a => a.CodigoAgenda == id)
                                                .FirstOrDefault();

            Ordeninstalacion ordenGrabada = _wiseContext.Instalacionescabecera
                                                .Include(i => i.OrdenInstalacionInstalacionesCabeceraNavigation)
                                                .Where(i => i.DocumentoOrigenInstalacionesCabecera == id)
                                                .OrderByDescending(i => i.FechaRegistroAsignacionInstalacionesCabecera)
                                                .Select(i => i.OrdenInstalacionInstalacionesCabeceraNavigation)
                                                .FirstOrDefault();


            if (agendaGrabada != null && ordenGrabada != null)
            {
                agendaGrabada.FechaUltimaModificacionAgenda = DateTime.Now.ToTimeZoneTime();
                agendaGrabada.EstadoAgenda = (int)EstadoAgenda.CANCELADA;
                _wiseContext.Agendas.Update(agendaGrabada);

                RegistrarLlamadaDto infoLlamada = new RegistrarLlamadaDto()
                {
                    observacion = cancelarAgendaDto.observacion
                };

                if (cancelarAgendaDto.desdeLlamada)
                {
                    _callCenter.grabarLlamadaCita(agendaGrabada.Citasmedicas, infoLlamada);
                }

                cancelarOrdenInstalacion(ordenGrabada);

                if (cancelarAgendaDto.reagendar)
                {
                    _callCenter.grabarReagendamiento(agendaGrabada, ordenGrabada, infoLlamada);
                }
                else
                {
                    Solicitudcitasmedicas solicitudCita = agendaGrabada.Citasmedicas.SolicitudesCitaMedicaNavigation;
                    if (solicitudCita != null)
                    {
                        List<Ordeninstalacion> ordenesCancelar = _wiseContext
                                                                    .Ordeninstalacion
                                                                    .Where(o => o.ContratoCabeceraOrdenInstalacion == solicitudCita.CodigoSoliCitaMedica
                                                                            && o.EstadoOrdenInstalacion == (int)EstadoOrdenInstalacion.POR_REINGRESAR)
                                                                    .ToList();
                        foreach (Ordeninstalacion ordenCancelar in ordenesCancelar)
                        {
                            cancelarOrdenInstalacion(ordenCancelar);
                        }
                        solicitudCita.EstadoSoliCitaMedica = (int)EstadoSolicitudCita.CANCELADA;
                        _wiseContext.Solicitudcitasmedicas.Update(solicitudCita);
                    }
                }

                try
                {
                    _wiseContext.SaveChanges();
                    return Ok();
                }
                catch (Exception)
                {
                    return BadRequest();
                }

            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("{id}/generarDocumentoEmpleado")]
        public IActionResult PostGenerarDocumentoEmpleado(string id)
        {
            Empleados empleadoLogeado = _currentUserService.GetEmpleado();
            Agendas agendaGrabada = _wiseContext.Agendas
                                                .Include(a => a.Citasmedicas)
                                                    .ThenInclude(c => c.SolicitudesCitaMedicaNavigation)
                                                .Include(a => a.EmpleadosAgendaNavigation)
                                                    .ThenInclude(e => e.SucursalesEmpleadoNavigation)
                                                        .ThenInclude(s => s.EmpresasSucursalNavigation)
                                                .Include(a => a.EmpleadosAgendaNavigation)
                                                .Where(a => a.CodigoAgenda == id)
                                                .FirstOrDefault();

            if (agendaGrabada != null)
            {
                InstalacionXdocumentoPendiente ultimaInstalacionXdocumento = _wiseContext.Instalacionescabecera
                                                                                    .Include(i => i.OrdenInstalacionInstalacionesCabeceraNavigation)
                                                                                    .Where(i => i.DocumentoOrigenInstalacionesCabecera == id)
                                                                                    .OrderBy(i => i.FechaRegistroAsignacionInstalacionesCabecera)
                                                                                    .Join(_wiseContext.Docspendientes,
                                                                                            i => i.CodigoInstalacionesCabecera,
                                                                                            d => d.DocsCabeceraDocPendiente,
                                                                                            (i, d) => new InstalacionXdocumentoPendiente()
                                                                                            {
                                                                                                instalacion = i,
                                                                                                docPendiente = d,
                                                                                            }
                                                                                        )
                                                                                    .LastOrDefault();
                if (ultimaInstalacionXdocumento.docPendiente.EstadoDocPendiente == EstadoDocPendiente.PENDIENTE)
                {
                    //Separar en servicio con call center
                    ultimaInstalacionXdocumento.docPendiente.EstadoDocPendiente = empleadoLogeado.NombreUsuarioEmpleado;
                    ultimaInstalacionXdocumento.docPendiente.AtencionDocPendiente = DateTime.Now;
                    _wiseContext.Docspendientes.Update(ultimaInstalacionXdocumento.docPendiente);

                    ultimaInstalacionXdocumento.instalacion.ClienteInstalacionesCabecera = agendaGrabada.Citasmedicas.ClientesCitaMedica;
                    _wiseContext.Instalacionescabecera.Update(ultimaInstalacionXdocumento.instalacion);

                    agendaGrabada.FechaUltimaModificacionAgenda = DateTime.Now.ToTimeZoneTime();
                    agendaGrabada.EstadoAgenda = (int)EstadoAgenda.AGENDADO_ATENDER;
                    _wiseContext.Agendas.Update(agendaGrabada);

                    Docspendientes documentoPendiente = new Docspendientes()
                    {
                        DocsCabeceraDocPendiente = ultimaInstalacionXdocumento.instalacion.CodigoInstalacionesCabecera,
                        CodigoDocPendiente = utils.generarCodigoFecha(),
                        TiposTramitesDocPendiente = AccionesDocumentos.POR_ATENDER.GetStringValue(),
                        PerfilesDocPendiente = agendaGrabada.EmpleadosAgenda,
                        PerfilesMailBccdocPendiente = agendaGrabada.EmpleadosAgenda,
                        CreacionDocPendiente = DateTime.Now.ToTimeZoneTime(),
                        AtencionDocPendiente = DateTime.Parse("1900-01-01"),
                        EstadoDocPendiente = EstadoDocPendiente.PENDIENTE,
                        TipoDocumentoDocPendiente = TipoDocumentoPendiente.CITA_MEDICA
                    };
                    _wiseContext.Docspendientes.Add(documentoPendiente);

                    try
                    {
                        _wiseContext.SaveChanges();
                        return Ok();
                    }
                    catch (Exception)
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
                return BadRequest();
            }
        }
    }
}