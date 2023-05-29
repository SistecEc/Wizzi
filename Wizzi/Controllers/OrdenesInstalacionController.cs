using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Wizzi.Dtos.Agendas;
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
    public class OrdenesInstalacionController : ControllerBase
    {
        private DataContext _wiseContext;
        private IMapper _mapper;
        private ICallCenterService _callCenter;
        private readonly UserResolverService _currentUserService;

        public OrdenesInstalacionController(
            DataContext wiseContext,
            IMapper mapper,
            ICallCenterService callCenter,
            UserResolverService currentUserService
            )
        {
            _wiseContext = wiseContext;
            _mapper = mapper;
            _callCenter = callCenter;
            _currentUserService = currentUserService;
        }


        [HttpGet("porReagendar")]
        public IActionResult GetOrdenesPorReagendar(int p = 1, string ciudad = "", string sucursal = "", DateTime? fechaRegistroInicio = null, DateTime? fechaRegistroFinal = null, bool aplicarfiltrofecha = false)
        {
            string empleadoActual = _currentUserService.GetCode();
            List<string> sucursalesPermitidas = _wiseContext.Permisossucursalagendar
                                                            .Where(p => p.EmpleadosPermisoSucursalAgendar == empleadoActual)
                                                            .Select(p => p.SucursalesPermisoSucursalAgendar)
                                                            .ToList();
            ResultadoPaginado<VerAgendaCitaMedicaDto> agendasDto = new ResultadoPaginado<VerAgendaCitaMedicaDto>();
            ResultadoPaginado<VerAgendaCitaMedicaDto> agendasDtos = new ResultadoPaginado<VerAgendaCitaMedicaDto>();
            if (sucursalesPermitidas.Count > 0)
            {
                //Ordenes marcadas para reagendar
                List<Ordeninstalacion> ordenesReagendar = _wiseContext
                                                            .Ordeninstalacion
                                                            .Where(o => o.EstadoOrdenInstalacion == (int)EstadoOrdenInstalacion.POR_REINGRESAR)
                                                            .ToList();

                //códigos de documento de origen(agenda, solicitud) que generó la orden
                List<string> codigosPadreOrdenes = ordenesReagendar
                                                    .Select(o => o.ContratoCabeceraOrdenInstalacion)
                                                    .ToList();

                //Códigos de las solicitudes que generaron las ordenes
                List<string> codigosDocumentosPadreOrdenesDeSolicitud = _wiseContext
                                                                        .Solicitudcitasmedicas
                                                                        .Where(s => codigosPadreOrdenes.Contains(s.CodigoSoliCitaMedica))
                                                                        .Select(s => s.CodigoSoliCitaMedica)
                                                                        .ToList();

                //Códigos de las agendas directas que generaron las ordenes
                List<string> codigosDocumentosPadreOrdenesDeAgendasDirectas = codigosPadreOrdenes
                                                                                .Where(o => !codigosDocumentosPadreOrdenesDeSolicitud.Contains(o))
                                                                                .ToList();



                //Lista de ordenes que fueron generadas por una agenda directa y que fueron canceladas
                List<Ordeninstalacion> ordenesAgendasDirectasPreviasCanceladas = _wiseContext
                                                                                    .Ordeninstalacion
                                                                                    .Include(o => o.Instalacionescabecera)
                                                                                    .Where(o => codigosDocumentosPadreOrdenesDeAgendasDirectas.Contains(o.ContratoCabeceraOrdenInstalacion)
                                                                                                && o.EstadoOrdenInstalacion == (int)EstadoOrdenInstalacion.CANCELADA
                                                                                                )
                                                                                    .OrderByDescending(o => o.FechaRegistroOrdenInstalacion)
                                                                                    .ToList();

                //Códigos de la última agenda de cada grupo ordenes generadas por una agenda directa
                List<string> codigosAgendasDirectasPorReagendar = ordenesAgendasDirectasPreviasCanceladas
                                                                    .GroupBy(o => o.ContratoCabeceraOrdenInstalacion)
                                                                    .Select(o => o.FirstOrDefault()
                                                                                    .Instalacionescabecera
                                                                                    .OrderByDescending(i => i.FechaRegistroAsignacionInstalacionesCabecera)
                                                                                    .FirstOrDefault()
                                                                                    )
                                                                    .Join(_wiseContext.Agendas,
                                                                                            i => i.DocumentoOrigenInstalacionesCabecera,
                                                                                            a => a.CodigoAgenda,
                                                                                            (i, a) => a.CodigoAgenda
                                                                                        )
                                                                    .ToList();


                Expression<Func<Agendas, bool>> condicionAgenda = a => a.EstadoAgenda == (int)EstadoAgenda.CANCELADA
                                                                        && (codigosDocumentosPadreOrdenesDeSolicitud.Contains(a.Citasmedicas.SolicitudesCitaMedica)
                                                                            || codigosAgendasDirectasPorReagendar.Contains(a.CodigoAgenda));
                if (!string.IsNullOrEmpty(sucursal))
                {
                    condicionAgenda = condicionAgenda.And(e => e.EmpleadosAgendaNavigation.SucursalesEmpleado == sucursal);
                }
                if (!string.IsNullOrEmpty(ciudad))
                {
                    condicionAgenda = condicionAgenda.And(e => e.EmpleadosAgendaNavigation.SucursalesEmpleadoNavigation.CiudadesSucursal == ciudad);
                }
                if (aplicarfiltrofecha)
                {
                    condicionAgenda = condicionAgenda.And(s => (s.FechaInicioAgenda.Date > fechaRegistroInicio.Value.Date) && (s.FechaInicioAgenda.Date < fechaRegistroFinal.Value.Date));
                }

                agendasDto = _wiseContext.Agendas
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
                                        .Where(a => sucursalesPermitidas
                                                    .Contains(a.EmpleadosAgendaNavigation.SucursalesEmpleado)
                                                )
                                        .GetPaged<Agendas, VerAgendaCitaMedicaDto>(p, 10, _mapper, AgregarExtrasAgendaDto);

                agendasDtos = _wiseContext.Agendas
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
                                        .Where(a => sucursalesPermitidas
                                                    .Contains(a.EmpleadosAgendaNavigation.SucursalesEmpleado)
                                                )
                                        .GetPaged<Agendas, VerAgendaCitaMedicaDto>(p, agendasDto.CantidadPaginas * 10, _mapper, AgregarExtrasAgendaDto);
            }

            return Ok(agendasDtos);
        }

        private VerAgendaCitaMedicaDto AgregarExtrasAgendaDto(Agendas agenda)
        {
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

            Ordeninstalacion ultimaOrden = ordenes.LastOrDefault();


            Parametros parametros = _wiseContext.Parametros
                                                .Include(p => p.CategoriaDocumentoLlamadaCallCenterParametroNavigation)
                                                .Where(p => p.EmpresasParametro == ultimaOrden.EmpresaOrdenInstalacion)
                                                .FirstOrDefault();

            VerAgendaCitaMedicaDto verAgendaDto = _mapper.Map<VerAgendaCitaMedicaDto>(agenda);

            verAgendaDto.sucursal = _mapper.Map<VerSucursalDto>(agenda.EmpleadosAgendaNavigation.SucursalesEmpleadoNavigation);

            int cantidadMovimientos = 0;

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

    }
}