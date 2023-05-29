using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using Wizzi.Dtos.Llamadas;
using Wizzi.Entities;
using Wizzi.Enums;
using Wizzi.Extensions;
using Wizzi.Helpers;
using Wizzi.Interfaces;
using Wizzi.Models;

namespace Wizzi.Services
{
    public class CallCenterService : ICallCenterService
    {
        private DataContext _context;
        private readonly UserResolverService _currentUserService;
        private readonly AppSettings _appSettings;

        public CallCenterService(
            DataContext context,
            UserResolverService currentUserService,
            IOptions<AppSettings> appSettings
            )
        {
            _context = context;
            _currentUserService = currentUserService;
            _appSettings = appSettings.Value;
        }

        public void grabarCallCenterCitaMedica(Citasmedicas citaMedicaCreada, string codigoDocumentoOrigeInstalacion, bool esNuevoInicioProceso = false, int numeroIteracion = 0)
        {
            //TODO: VALIDAR SI TIENE UNA EMPRESA ASIGNADA LA CITA, SI NO COGER DESDE EL APPSETTINGS
            string bodega = _context.Parametros.Find(_appSettings.EmpresaDefecto).BodegaVentaParametro;
            if (bodega != null)
            {
                Agendas agendaCreada = citaMedicaCreada.AgendasCitaMedicaNavigation;
                Empleados empleadoLogeado = _currentUserService.GetEmpleado();
                string codigoEmpresa = agendaCreada.EmpleadosAgendaNavigation.EmpresasEmpleado;
                string codigoSucursal = agendaCreada.EmpleadosAgendaNavigation.SucursalesEmpleado;
                Ordeninstalacion ordeninstalacion = null;

                Instalacionescabecera instalacionGrabada = _context.Instalacionescabecera
                                                                    .Include(i => i.OrdenInstalacionInstalacionesCabeceraNavigation)
                                                                    .Where(i => i.DocumentoOrigenInstalacionesCabecera == codigoDocumentoOrigeInstalacion)
                                                                    .OrderBy(i => i.FechaRegistroAsignacionInstalacionesCabecera)
                                                                    .LastOrDefault();

                if (instalacionGrabada != null)
                {
                    atenderInstalacion(instalacionGrabada);
                    Docspendientes documentoPendienteGrabado = _context
                                                                .Docspendientes
                                                                .Where(doc => doc.DocsCabeceraDocPendiente == instalacionGrabada.CodigoInstalacionesCabecera
                                                                            && doc.EstadoDocPendiente == EstadoDocPendiente.PENDIENTE)
                                                                .FirstOrDefault();

                    if (documentoPendienteGrabado != null)
                    {
                        atenderDocumentoPendiente(documentoPendienteGrabado, empleadoLogeado);
                    }

                    if (esNuevoInicioProceso)
                    {
                        ordeninstalacion = _context
                                            .Ordeninstalacion
                                            .Where(o => o.ContratoCabeceraOrdenInstalacion == instalacionGrabada.OrdenInstalacionInstalacionesCabeceraNavigation.ContratoCabeceraOrdenInstalacion)
                                            .OrderBy(o => o.FechaRegistroOrdenInstalacion)
                                            .LastOrDefault();
                    }
                    else
                    {
                        ordeninstalacion = instalacionGrabada.OrdenInstalacionInstalacionesCabeceraNavigation;
                    }
                }
                else
                {
                    ordeninstalacion = new Ordeninstalacion()
                    {
                        CodigoOrdenInstalacion = $"{utils.generarCodigoFecha()}{numeroIteracion}",
                        EmpresaOrdenInstalacion = codigoEmpresa,
                        SucursalOrdenInstalacion = codigoSucursal,
                        EmpleadoRegistroOrdenInstalacion = empleadoLogeado.CodigoEmpleado,
                        ContratoCabeceraOrdenInstalacion = codigoDocumentoOrigeInstalacion,
                        ClienteOrdenInstalacion = citaMedicaCreada.ClientesCitaMedica,
                        FechaRegistroOrdenInstalacion = DateTime.Now,
                        MotivosDocumentosInstalaciones = TipoDocumentoPendiente.CITA_MEDICA,
                        EstadoOrdenInstalacion = (int)EstadoOrdenInstalacion.POR_ATENDER,
                        UsuarioRegistroOrdenInstalacion = empleadoLogeado.NombreUsuarioEmpleado
                    };
                    _context.Ordeninstalacion.Add(ordeninstalacion);
                }

                Categoriastiposdocumentosinstalaciones tipoCitaMedica = _context.Categoriastiposdocumentosinstalaciones
                                                                            .Include(categoria => categoria.TiposDocumentosInstalacionesCategoriaTiposDocumentoInstalacionesNavigation)
                                                                            .Where(categoria => categoria.CodigoCategoriasTiposDocumentosInstalaciones == citaMedicaCreada.TipoCitaMedica)
                                                                            .FirstOrDefault();

                if (ordeninstalacion != null && tipoCitaMedica != null)
                {
                    if (esNuevoInicioProceso)
                    {
                        ordeninstalacion.EstadoOrdenInstalacion = (int)EstadoOrdenInstalacion.POR_ATENDER;
                        _context.Ordeninstalacion.Update(ordeninstalacion);
                    }

                    Tiposdocumentosinstalaciones tipoDocumentoInstalacion = tipoCitaMedica
                                                                            .TiposDocumentosInstalacionesCategoriaTiposDocumentoInstalacionesNavigation;

                    Instalacionescabecera instalacionCabecera = new Instalacionescabecera()
                    {
                        CodigoInstalacionesCabecera = $"{utils.generarCodigoFecha()}{numeroIteracion}",
                        NumeroSecuencialInstalacionesCabecera = tipoDocumentoInstalacion.SecuenciaTipoDocumentoInstalacion.ToString(),
                        OrdenInstalacionInstalacionesCabecera = ordeninstalacion.CodigoOrdenInstalacion,
                        EmpresaInstalacionesCabecera = codigoEmpresa,
                        SucursalInstalacionesCabecera = codigoSucursal,
                        EmpleadoInstalacionesCabecera = agendaCreada.EmpleadosAgenda,
                        ClienteInstalacionesCabecera = citaMedicaCreada.ClientesCitaMedica,
                        FechaInstalacionesCabecera = agendaCreada.FechaInicioAgenda,
                        EstadoAsignacionInstalacionesCabecera = ((int)EstadoAsignacionInstalacion.ASIGNADO).ToString(),
                        EstadoInstalacionInstalacionesCabecera = ((int)TipoFinalizacionCallCeneter.POR_REALIZAR).ToString(),
                        CategoriasFinalizacionInstalacionesCabecera = "0",
                        FinalizadoMatrizInstalacionesCabecera = 0,
                        LatitudInstalacionesCabecera = "0",
                        LongitudInstalacionesCabecera = "0",
                        BodegaOrigenInstalacionesCabecera = bodega,
                        SubTotalInstalacionesCabecera = 0,
                        SubTotal0InstalacionesCabecera = 0,
                        DescripcionInstalacionesCabecera = "",
                        ObservacionInstalacionesCabecera = agendaCreada.DescripcionAgenda,
                        LocalInstalacionesCabecera = "",
                        EstadoAutorizacionInstalacionesCabecera = "0",
                        FechaAsignacionInstalacionesCabecera = DateTime.Now,
                        FechaInstalacionInstalacionesCabecera = DateTime.Parse("1900-01-01"),
                        FechaAnulacionInstalacionesCabecera = DateTime.Parse("1900-01-01"),
                        TiposDocumentoInstalacionesCabecera = tipoDocumentoInstalacion.CodigoTipoDocumentoInstalacion,
                        CategoriasTiposDocumentosInstalacionesCabecera = tipoCitaMedica.CodigoCategoriasTiposDocumentosInstalaciones,
                        NivelesPrioridadProcesosInstalacionesCabecera = (int)NivelPrioridad.MEDIA,
                        CodigoPadreInstalacionesCabecera = "",
                        FechaRegistroAsignacionInstalacionesCabecera = DateTime.Now,
                        DocumentoOrigenInstalacionesCabecera = agendaCreada.CodigoAgenda,
                        UsuarioAsignaInstalacionesCabecera = empleadoLogeado.NombreUsuarioEmpleado,
                        UsuarioInstalacionInstalacionesCabecera = "",
                        UsuarioAnulaInstalacionesCabecera = ""
                    };
                    tipoDocumentoInstalacion.SecuenciaTipoDocumentoInstalacion += 1;
                    _context.Instalacionescabecera.Add(instalacionCabecera);
                    _context.Tiposdocumentosinstalaciones.Update(tipoDocumentoInstalacion);

                    //Grabar observaciones
                    Observacionesempleadosinstalaciones observacion = new Observacionesempleadosinstalaciones()
                    {
                        CodigoObservacionEmpleadoInstalacion = $"{utils.generarCodigoFecha()}{numeroIteracion}",
                        InstalacionesCabeceraObservacionEmpleadoInstalacion = instalacionCabecera.CodigoInstalacionesCabecera,
                        ObservacionObservacionEmpleadoInstalacion = agendaCreada.DescripcionAgenda,
                        EmpleadosObservacionEmpleadosInstalacion = empleadoLogeado.CodigoEmpleado,
                        OrigenesObservacionesObservacionEmpleadosInstalacion = ((int)OrigenObservacionCallCenter.CALL_CENTER).ToString(),
                        EquiposEmpleadoObservacionEmpleado = "",
                        FechaObservacionEmpleado = DateTime.Now

                    };
                    _context.Observacionesempleadosinstalaciones.Add(observacion);

                    Docspendientes documentoPendiente = null;

                    if (agendaCreada.EstadoAgenda == (int)EstadoAgenda.AGENDADO)
                    {
                        //Generar documento pendiente de llamada para confirmar
                        documentoPendiente = new Docspendientes()
                        {
                            DocsCabeceraDocPendiente = instalacionCabecera.CodigoInstalacionesCabecera,
                            CodigoDocPendiente = $"{utils.generarCodigoFecha()}{numeroIteracion}",
                            TiposTramitesDocPendiente = AccionesDocumentos.POR_ATENDER.GetStringValue(),
                            PerfilesDocPendiente = empleadoLogeado.CodigoEmpleado,
                            PerfilesMailBccdocPendiente = empleadoLogeado.CodigoEmpleado,
                            CreacionDocPendiente = agendaCreada.FechaInicioAgenda.Date.AddDays(-1),
                            AtencionDocPendiente = DateTime.Parse("1900-01-01"),
                            EstadoDocPendiente = EstadoDocPendiente.PENDIENTE,
                            TipoDocumentoDocPendiente = TipoDocumentoPendiente.CITA_MEDICA_LLAMADA
                        };
                    }
                    else
                    {
                        //Generar documento pendiente de completar datos
                        documentoPendiente = new Docspendientes()
                        {
                            DocsCabeceraDocPendiente = instalacionCabecera.CodigoInstalacionesCabecera,
                            CodigoDocPendiente = $"{utils.generarCodigoFecha()}{numeroIteracion}",
                            TiposTramitesDocPendiente = AccionesDocumentos.POR_ATENDER.GetStringValue(),
                            PerfilesDocPendiente = empleadoLogeado.CodigoEmpleado,
                            PerfilesMailBccdocPendiente = empleadoLogeado.CodigoEmpleado,
                            CreacionDocPendiente = agendaCreada.FechaInicioAgenda.Date,
                            AtencionDocPendiente = DateTime.Parse("1900-01-01"),
                            EstadoDocPendiente = EstadoDocPendiente.PENDIENTE,
                            TipoDocumentoDocPendiente = TipoDocumentoPendiente.CITA_MEDICA_COMPLETAR_DATOS
                        };
                    }

                    _context.Docspendientes.Add(documentoPendiente);

                }
                else
                {
                    throw new AppException("No se ha encontrado el tipo de cita médica");
                }
            }
            else
            {
                throw new AppException("No se ha encontrado una bodega para la venta");
            }
        }

        public void reasignarCallCenterCitaMedica(Agendas agendaGrabada, Citasmedicas citaMedicaNueva)
        {
            Empleados empleadoLogeado = _currentUserService.GetEmpleado();
            //TODO: VALIDAR SI TIENE UNA EMPRESA ASIGNADA LA CITA, SI NO COGER DESDE EL APPSETTINGS
            string bodega = _context.Parametros.Find(_appSettings.EmpresaDefecto).BodegaVentaParametro;
            if (bodega != null)
            {
                Instalacionescabecera instalacionGrabada = _context
                                                            .Instalacionescabecera
                                                            .Where(i => i.DocumentoOrigenInstalacionesCabecera == agendaGrabada.CodigoAgenda)
                                                            .OrderBy(i => i.FechaRegistroAsignacionInstalacionesCabecera)
                                                            .LastOrDefault();
                if (instalacionGrabada != null)
                {
                    atenderInstalacion(instalacionGrabada);

                    //Grabar observación de reasignado
                    int contadorObservacion = 0;
                    _context.Observacionesempleadosinstalaciones.Add(new Observacionesempleadosinstalaciones()
                    {
                        CodigoObservacionEmpleadoInstalacion = string.Concat(utils.generarCodigoFecha(), contadorObservacion),
                        InstalacionesCabeceraObservacionEmpleadoInstalacion = instalacionGrabada.CodigoInstalacionesCabecera,
                        ObservacionObservacionEmpleadoInstalacion = "Reasignado desde el módulo de agendamiento",
                        EmpleadosObservacionEmpleadosInstalacion = empleadoLogeado.CodigoEmpleado,
                        OrigenesObservacionesObservacionEmpleadosInstalacion = ((int)OrigenObservacionCallCenter.CALL_CENTER).ToString(),
                        EquiposEmpleadoObservacionEmpleado = "",
                        FechaObservacionEmpleado = DateTime.Now
                    });
                    contadorObservacion += 1;

                    Docspendientes documentoPendienteGrabado = _context
                                                                .Docspendientes
                                                                .Where(doc => doc.DocsCabeceraDocPendiente == instalacionGrabada.CodigoInstalacionesCabecera
                                                                            && doc.EstadoDocPendiente == EstadoDocPendiente.PENDIENTE)
                                                                .FirstOrDefault();
                    if (documentoPendienteGrabado != null)
                    {
                        atenderDocumentoPendiente(documentoPendienteGrabado, empleadoLogeado);

                        Agendas agendaNueva = citaMedicaNueva.AgendasCitaMedicaNavigation;
                        string codigoEmpresa = agendaNueva.EmpleadosAgendaNavigation.EmpresasEmpleado;
                        string codigoSucursal = agendaNueva.EmpleadosAgendaNavigation.SucursalesEmpleado;

                        Categoriastiposdocumentosinstalaciones tipoCitaMedica = _context.Categoriastiposdocumentosinstalaciones
                                                                                    .Include(categoria => categoria.TiposDocumentosInstalacionesCategoriaTiposDocumentoInstalacionesNavigation)
                                                                                    .Where(categoria => categoria.CodigoCategoriasTiposDocumentosInstalaciones == citaMedicaNueva.TipoCitaMedica)
                                                                                    .FirstOrDefault();

                        if (tipoCitaMedica != null)
                        {
                            Tiposdocumentosinstalaciones tipoDocumentoInstalacion = tipoCitaMedica
                                                                                    .TiposDocumentosInstalacionesCategoriaTiposDocumentoInstalacionesNavigation;
                            Instalacionescabecera instalacionCabecera = new Instalacionescabecera()
                            {
                                CodigoInstalacionesCabecera = utils.generarCodigoFecha(),
                                NumeroSecuencialInstalacionesCabecera = tipoDocumentoInstalacion.SecuenciaTipoDocumentoInstalacion.ToString(),
                                OrdenInstalacionInstalacionesCabecera = instalacionGrabada.OrdenInstalacionInstalacionesCabecera,
                                EmpresaInstalacionesCabecera = codigoEmpresa,
                                SucursalInstalacionesCabecera = codigoSucursal,
                                EmpleadoInstalacionesCabecera = agendaNueva.EmpleadosAgenda,
                                ClienteInstalacionesCabecera = citaMedicaNueva.ClientesCitaMedica,
                                FechaInstalacionesCabecera = agendaNueva.FechaInicioAgenda,
                                EstadoAsignacionInstalacionesCabecera = ((int)EstadoAsignacionInstalacion.ASIGNADO).ToString(),
                                EstadoInstalacionInstalacionesCabecera = ((int)TipoFinalizacionCallCeneter.POR_REALIZAR).ToString(),
                                CategoriasFinalizacionInstalacionesCabecera = ((int)CategoriasFinalizacionCallCenter.POR_REALIZAR).ToString(),
                                FinalizadoMatrizInstalacionesCabecera = 0,
                                LatitudInstalacionesCabecera = "0",
                                LongitudInstalacionesCabecera = "0",
                                BodegaOrigenInstalacionesCabecera = bodega,
                                SubTotalInstalacionesCabecera = 0,
                                SubTotal0InstalacionesCabecera = 0,
                                DescripcionInstalacionesCabecera = "",
                                ObservacionInstalacionesCabecera = agendaNueva.DescripcionAgenda,
                                LocalInstalacionesCabecera = "",
                                EstadoAutorizacionInstalacionesCabecera = "0",
                                FechaAsignacionInstalacionesCabecera = DateTime.Now,
                                FechaInstalacionInstalacionesCabecera = DateTime.Parse("1900-01-01"),
                                FechaAnulacionInstalacionesCabecera = DateTime.Parse("1900-01-01"),
                                TiposDocumentoInstalacionesCabecera = tipoDocumentoInstalacion.CodigoTipoDocumentoInstalacion,
                                CategoriasTiposDocumentosInstalacionesCabecera = tipoCitaMedica.CodigoCategoriasTiposDocumentosInstalaciones,
                                NivelesPrioridadProcesosInstalacionesCabecera = (int)NivelPrioridad.MEDIA,
                                CodigoPadreInstalacionesCabecera = instalacionGrabada.CodigoInstalacionesCabecera,
                                FechaRegistroAsignacionInstalacionesCabecera = DateTime.Now,
                                DocumentoOrigenInstalacionesCabecera = agendaNueva.CodigoAgenda,
                                UsuarioAsignaInstalacionesCabecera = empleadoLogeado.NombreUsuarioEmpleado,
                                UsuarioInstalacionInstalacionesCabecera = "",
                                UsuarioAnulaInstalacionesCabecera = ""
                            };
                            tipoDocumentoInstalacion.SecuenciaTipoDocumentoInstalacion += 1;
                            _context.Instalacionescabecera.Add(instalacionCabecera);
                            _context.Tiposdocumentosinstalaciones.Update(tipoDocumentoInstalacion);

                            //Grabar observaciones
                            Observacionesempleadosinstalaciones observacion = new Observacionesempleadosinstalaciones()
                            {
                                CodigoObservacionEmpleadoInstalacion = string.Concat(utils.generarCodigoFecha(), contadorObservacion),
                                InstalacionesCabeceraObservacionEmpleadoInstalacion = instalacionCabecera.CodigoInstalacionesCabecera,
                                ObservacionObservacionEmpleadoInstalacion = agendaNueva.DescripcionAgenda,
                                EmpleadosObservacionEmpleadosInstalacion = empleadoLogeado.CodigoEmpleado,
                                OrigenesObservacionesObservacionEmpleadosInstalacion = ((int)OrigenObservacionCallCenter.CALL_CENTER).ToString(),
                                EquiposEmpleadoObservacionEmpleado = "",
                                FechaObservacionEmpleado = DateTime.Now

                            };
                            _context.Observacionesempleadosinstalaciones.Add(observacion);
                            contadorObservacion += 1;

                            Docspendientes documentoPendiente = null;

                            if (agendaNueva.EstadoAgenda == (int)EstadoAgenda.AGENDADO)
                            {
                                //Generar documento pendiente de llamada para confirmar
                                documentoPendiente = new Docspendientes()
                                {
                                    DocsCabeceraDocPendiente = instalacionCabecera.CodigoInstalacionesCabecera,
                                    CodigoDocPendiente = utils.generarCodigoFecha(),
                                    TiposTramitesDocPendiente = AccionesDocumentos.POR_ATENDER.GetStringValue(),
                                    PerfilesDocPendiente = empleadoLogeado.CodigoEmpleado,
                                    PerfilesMailBccdocPendiente = empleadoLogeado.CodigoEmpleado,
                                    CreacionDocPendiente = agendaNueva.FechaInicioAgenda.Date.AddDays(-1),
                                    AtencionDocPendiente = DateTime.Parse("1900-01-01"),
                                    EstadoDocPendiente = EstadoDocPendiente.PENDIENTE,
                                    TipoDocumentoDocPendiente = TipoDocumentoPendiente.CITA_MEDICA_LLAMADA
                                };
                            }
                            else
                            {
                                //Generar documento pendiente de completar datos
                                documentoPendiente = new Docspendientes()
                                {
                                    DocsCabeceraDocPendiente = instalacionCabecera.CodigoInstalacionesCabecera,
                                    CodigoDocPendiente = utils.generarCodigoFecha(),
                                    TiposTramitesDocPendiente = AccionesDocumentos.POR_ATENDER.GetStringValue(),
                                    PerfilesDocPendiente = empleadoLogeado.CodigoEmpleado,
                                    PerfilesMailBccdocPendiente = empleadoLogeado.CodigoEmpleado,
                                    CreacionDocPendiente = agendaNueva.FechaInicioAgenda.Date,
                                    AtencionDocPendiente = DateTime.Parse("1900-01-01"),
                                    EstadoDocPendiente = EstadoDocPendiente.PENDIENTE,
                                    TipoDocumentoDocPendiente = TipoDocumentoPendiente.CITA_MEDICA_COMPLETAR_DATOS
                                };
                            }

                            _context.Docspendientes.Add(documentoPendiente);

                        }
                        else
                        {
                            throw new AppException("No se ha encontrado el tipo de cita médica");
                        }
                    }
                    else
                    {
                        throw new AppException("No se ha encontrado un documento pendiente por atender de la agenda grabada");
                    }
                }
                else
                {
                    throw new AppException("No se ha encontrado el documento de call center correspondiente a la agenda grabada");
                }

            }
            else
            {
                throw new AppException("No se ha encontrado una bodega para la venta");
            }
        }

        public void grabarLlamadaSolicitudCita(Solicitudcitasmedicas solicitud, RegistrarLlamadaDto infoLlamada, bool forzarNuevaOrden = false)
        {
            Empleados empleadoLogeado = _currentUserService.GetEmpleado();
            string codigoEmpresa = solicitud.SucursalesSoliCitaMedicaNavigation?.EmpresasSucursalNavigation.CodigoEmpresa ?? _appSettings.EmpresaDefecto;
            string codigoSucursal = solicitud.SucursalesSoliCitaMedica ?? _appSettings.SucursalDefecto;
            Ordeninstalacion ordeninstalacion = null;
            bool crearNuevaOrden = forzarNuevaOrden;

            Instalacionescabecera instalacionGrabada = _context.Instalacionescabecera
                                                                .Include(i => i.OrdenInstalacionInstalacionesCabeceraNavigation)
                                                                .Where(i => i.DocumentoOrigenInstalacionesCabecera == solicitud.CodigoSoliCitaMedica)
                                                                .OrderBy(i => i.FechaRegistroAsignacionInstalacionesCabecera)
                                                                .LastOrDefault();

            if (instalacionGrabada != null)
            {
                atenderInstalacion(instalacionGrabada);
                ordeninstalacion = instalacionGrabada.OrdenInstalacionInstalacionesCabeceraNavigation;
                Docspendientes documentoPendienteGrabado = _context
                                                            .Docspendientes
                                                            .Where(doc => doc.DocsCabeceraDocPendiente == instalacionGrabada.CodigoInstalacionesCabecera
                                                                        && doc.EstadoDocPendiente == EstadoDocPendiente.PENDIENTE)
                                                            .FirstOrDefault();

                if (documentoPendienteGrabado != null)
                {
                    atenderDocumentoPendiente(documentoPendienteGrabado, empleadoLogeado);
                }
            }
            else
            {
                crearNuevaOrden = true;
            }

            if (crearNuevaOrden)
            {
                ordeninstalacion = new Ordeninstalacion()
                {
                    CodigoOrdenInstalacion = utils.generarCodigoFecha(),
                    EmpresaOrdenInstalacion = codigoEmpresa,
                    SucursalOrdenInstalacion = codigoSucursal,
                    EmpleadoRegistroOrdenInstalacion = empleadoLogeado.CodigoEmpleado,
                    ContratoCabeceraOrdenInstalacion = solicitud.CodigoSoliCitaMedica,
                    ClienteOrdenInstalacion = null,
                    FechaRegistroOrdenInstalacion = DateTime.Now,
                    MotivosDocumentosInstalaciones = TipoDocumentoPendiente.CITA_MEDICA,
                    EstadoOrdenInstalacion = (int)EstadoOrdenInstalacion.POR_ATENDER,
                    UsuarioRegistroOrdenInstalacion = empleadoLogeado.NombreUsuarioEmpleado
                };
                _context.Ordeninstalacion.Add(ordeninstalacion);
            }

            if (ordeninstalacion != null)
            {

                Parametros parametros = _context.Parametros
                                                .Include(p => p.CategoriaDocumentoLlamadaCallCenterParametroNavigation)
                                                    .ThenInclude(c => c.TiposDocumentosInstalacionesCategoriaTiposDocumentoInstalacionesNavigation)
                                                .Where(p => p.EmpresasParametro == codigoEmpresa)
                                                .FirstOrDefault();

                string bodega = parametros.BodegaVentaParametro;
                Categoriastiposdocumentosinstalaciones categoriaTipoDocumentoLlamada = parametros.CategoriaDocumentoLlamadaCallCenterParametroNavigation;


                if (bodega != null)
                {

                    if (categoriaTipoDocumentoLlamada != null)
                    {
                        Empleadosatiendecallcenter empleadoAtiende = _context.Empleadosatiendecallcenter
                                                                            .Where(e => e.Categoriasdocumentosinstalacionesempleadoatiendecallcenter == categoriaTipoDocumentoLlamada.CodigoCategoriasTiposDocumentosInstalaciones)
                                                                            .FirstOrDefault();

                        if (empleadoAtiende != null)
                        {
                            Tiposdocumentosinstalaciones tipoDocumentoInstalacion = categoriaTipoDocumentoLlamada
                                                                                    .TiposDocumentosInstalacionesCategoriaTiposDocumentoInstalacionesNavigation;

                            Instalacionescabecera instalacionCabecera = new Instalacionescabecera()
                            {
                                CodigoInstalacionesCabecera = utils.generarCodigoFecha(),
                                NumeroSecuencialInstalacionesCabecera = tipoDocumentoInstalacion.SecuenciaTipoDocumentoInstalacion.ToString(),
                                OrdenInstalacionInstalacionesCabecera = ordeninstalacion.CodigoOrdenInstalacion,
                                EmpresaInstalacionesCabecera = codigoEmpresa,
                                SucursalInstalacionesCabecera = codigoSucursal,
                                EmpleadoInstalacionesCabecera = empleadoAtiende.Empleadoempleadoatiendecallcenter,
                                ClienteInstalacionesCabecera = null,
                                FechaInstalacionesCabecera = DateTime.Now,
                                EstadoAsignacionInstalacionesCabecera = ((int)EstadoAsignacionInstalacion.ASIGNADO).ToString(),
                                EstadoInstalacionInstalacionesCabecera = ((int)TipoFinalizacionCallCeneter.POR_REALIZAR).ToString(),
                                CategoriasFinalizacionInstalacionesCabecera = ((int)CategoriasFinalizacionCallCenter.POR_REALIZAR).ToString(),
                                FinalizadoMatrizInstalacionesCabecera = 0,
                                LatitudInstalacionesCabecera = "0",
                                LongitudInstalacionesCabecera = "0",
                                BodegaOrigenInstalacionesCabecera = bodega,
                                SubTotalInstalacionesCabecera = 0,
                                SubTotal0InstalacionesCabecera = 0,
                                DescripcionInstalacionesCabecera = "",
                                ObservacionInstalacionesCabecera = infoLlamada.observacion,
                                LocalInstalacionesCabecera = "",
                                EstadoAutorizacionInstalacionesCabecera = "0",
                                FechaAsignacionInstalacionesCabecera = DateTime.Now,
                                FechaInstalacionInstalacionesCabecera = DateTime.Parse("1900-01-01"),
                                FechaAnulacionInstalacionesCabecera = DateTime.Parse("1900-01-01"),
                                TiposDocumentoInstalacionesCabecera = tipoDocumentoInstalacion.CodigoTipoDocumentoInstalacion,
                                CategoriasTiposDocumentosInstalacionesCabecera = categoriaTipoDocumentoLlamada.CodigoCategoriasTiposDocumentosInstalaciones,
                                NivelesPrioridadProcesosInstalacionesCabecera = (int)NivelPrioridad.MEDIA,
                                CodigoPadreInstalacionesCabecera = instalacionGrabada?.CodigoInstalacionesCabecera ?? "",
                                FechaRegistroAsignacionInstalacionesCabecera = DateTime.Now,
                                DocumentoOrigenInstalacionesCabecera = solicitud.CodigoSoliCitaMedica,
                                UsuarioAsignaInstalacionesCabecera = empleadoLogeado.NombreUsuarioEmpleado,
                                UsuarioInstalacionInstalacionesCabecera = "",
                                UsuarioAnulaInstalacionesCabecera = ""
                            };
                            tipoDocumentoInstalacion.SecuenciaTipoDocumentoInstalacion += 1;
                            _context.Instalacionescabecera.Add(instalacionCabecera);
                            _context.Tiposdocumentosinstalaciones.Update(tipoDocumentoInstalacion);

                            //Generar documento pendiente
                            Docspendientes documentoPendiente = new Docspendientes()
                            {
                                DocsCabeceraDocPendiente = instalacionCabecera.CodigoInstalacionesCabecera,
                                CodigoDocPendiente = utils.generarCodigoFecha(),
                                TiposTramitesDocPendiente = AccionesDocumentos.POR_ATENDER.GetStringValue(),
                                PerfilesDocPendiente = empleadoAtiende.Empleadoempleadoatiendecallcenter,
                                PerfilesMailBccdocPendiente = empleadoAtiende.Empleadoempleadoatiendecallcenter,
                                CreacionDocPendiente = DateTime.Now,
                                AtencionDocPendiente = DateTime.Parse("1900-01-01"),
                                EstadoDocPendiente = EstadoDocPendiente.PENDIENTE,
                                TipoDocumentoDocPendiente = TipoDocumentoPendiente.CITA_MEDICA_LLAMADA
                            };
                            _context.Docspendientes.Add(documentoPendiente);

                            try
                            {
                                _context.SaveChanges();
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                        }
                        else
                        {
                            throw new AppException("No se ha encontrado el empleado que atiende el tipo de cita médica");
                        }
                    }
                    else
                    {
                        throw new AppException("No se ha encontrado el tipo de cita médica");
                    }
                }
                else
                {
                    throw new AppException("No se ha encontrado una bodega para la venta");
                }
            }
            else
            {
                throw new AppException("No se ha encontrado podido verificar la orden de la cita médica");
            }
        }

        private void atenderDocumentoPendiente(Docspendientes documentoPendienteGrabado, Empleados empleado)
        {
            documentoPendienteGrabado.EstadoDocPendiente = empleado.NombreUsuarioEmpleado;
            documentoPendienteGrabado.AtencionDocPendiente = DateTime.Now;
            _context.Docspendientes.Update(documentoPendienteGrabado);
        }

        private void atenderInstalacion(Instalacionescabecera instalacionGrabada)
        {
            instalacionGrabada.EstadoInstalacionInstalacionesCabecera = ((int)TipoFinalizacionCallCeneter.CORRECTO).ToString();
            instalacionGrabada.CategoriasFinalizacionInstalacionesCabecera = ((int)CategoriasFinalizacionCallCenter.CORRECTO).ToString();
            instalacionGrabada.FinalizadoMatrizInstalacionesCabecera = 1;
            instalacionGrabada.FechaInstalacionInstalacionesCabecera = DateTime.Now;
            _context.Instalacionescabecera.Update(instalacionGrabada);
        }

        public void grabarLlamadaCita(Citasmedicas cita, RegistrarLlamadaDto infoLlamada)
        {
            Empleados empleadoLogeado = _currentUserService.GetEmpleado();
            string codigoEmpresa = cita.AgendasCitaMedicaNavigation.EmpleadosAgendaNavigation.SucursalesEmpleadoNavigation.EmpresasSucursal;
            string codigoSucursal = cita.AgendasCitaMedicaNavigation.EmpleadosAgendaNavigation.SucursalesEmpleado;
            Ordeninstalacion ordeninstalacion = null;

            Instalacionescabecera instalacionGrabada = _context.Instalacionescabecera
                                                                .Include(i => i.OrdenInstalacionInstalacionesCabeceraNavigation)
                                                                .Where(i => i.DocumentoOrigenInstalacionesCabecera == cita.AgendasCitaMedicaNavigation.CodigoAgenda)
                                                                .OrderBy(i => i.FechaRegistroAsignacionInstalacionesCabecera)
                                                                .LastOrDefault();
            if (instalacionGrabada != null)
            {
                atenderInstalacion(instalacionGrabada);
                Docspendientes documentoPendienteGrabado = _context
                                                            .Docspendientes
                                                            .Where(doc => doc.DocsCabeceraDocPendiente == instalacionGrabada.CodigoInstalacionesCabecera
                                                                        && doc.EstadoDocPendiente == EstadoDocPendiente.PENDIENTE)
                                                            .FirstOrDefault();

                if (documentoPendienteGrabado != null)
                {
                    atenderDocumentoPendiente(documentoPendienteGrabado, empleadoLogeado);
                }

                if (infoLlamada.esNuevoInicioProceso)
                {
                    ordeninstalacion = _context
                                        .Ordeninstalacion
                                        .Where(o => o.ContratoCabeceraOrdenInstalacion == instalacionGrabada.OrdenInstalacionInstalacionesCabeceraNavigation.ContratoCabeceraOrdenInstalacion)
                                        .OrderBy(o => o.FechaRegistroOrdenInstalacion)
                                        .LastOrDefault();
                }
                else
                {
                    ordeninstalacion = instalacionGrabada.OrdenInstalacionInstalacionesCabeceraNavigation;
                }

            }
            else
            {
                ordeninstalacion = new Ordeninstalacion()
                {
                    CodigoOrdenInstalacion = utils.generarCodigoFecha(),
                    EmpresaOrdenInstalacion = codigoEmpresa,
                    SucursalOrdenInstalacion = codigoSucursal,
                    EmpleadoRegistroOrdenInstalacion = empleadoLogeado.CodigoEmpleado,
                    ContratoCabeceraOrdenInstalacion = cita.SolicitudesCitaMedica ?? cita.AgendasCitaMedica,
                    ClienteOrdenInstalacion = null,
                    FechaRegistroOrdenInstalacion = DateTime.Now,
                    MotivosDocumentosInstalaciones = TipoDocumentoPendiente.CITA_MEDICA,
                    EstadoOrdenInstalacion = (int)EstadoOrdenInstalacion.POR_ATENDER,
                    UsuarioRegistroOrdenInstalacion = empleadoLogeado.NombreUsuarioEmpleado
                };
                _context.Ordeninstalacion.Add(ordeninstalacion);
            }

            if (ordeninstalacion != null)
            {
                Parametros parametros = _context.Parametros
                                                .Include(p => p.CategoriaDocumentoLlamadaCallCenterParametroNavigation)
                                                    .ThenInclude(c => c.TiposDocumentosInstalacionesCategoriaTiposDocumentoInstalacionesNavigation)
                                                .Where(p => p.EmpresasParametro == codigoEmpresa)
                                                .FirstOrDefault();

                string bodega = parametros.BodegaVentaParametro;
                Categoriastiposdocumentosinstalaciones categoriaTipoDocumentoLlamada = parametros.CategoriaDocumentoLlamadaCallCenterParametroNavigation;

                if (bodega != null)
                {
                    if (categoriaTipoDocumentoLlamada != null)
                    {
                        Empleadosatiendecallcenter empleadoAtiende = _context.Empleadosatiendecallcenter
                                                                            .Where(e => e.Categoriasdocumentosinstalacionesempleadoatiendecallcenter == categoriaTipoDocumentoLlamada.CodigoCategoriasTiposDocumentosInstalaciones)
                                                                            .FirstOrDefault();

                        if (empleadoAtiende != null)
                        {
                            Tiposdocumentosinstalaciones tipoDocumentoInstalacion = categoriaTipoDocumentoLlamada
                                                                                    .TiposDocumentosInstalacionesCategoriaTiposDocumentoInstalacionesNavigation;

                            Instalacionescabecera instalacionCabecera = new Instalacionescabecera()
                            {
                                CodigoInstalacionesCabecera = utils.generarCodigoFecha(),
                                NumeroSecuencialInstalacionesCabecera = tipoDocumentoInstalacion.SecuenciaTipoDocumentoInstalacion.ToString(),
                                OrdenInstalacionInstalacionesCabecera = ordeninstalacion.CodigoOrdenInstalacion,
                                EmpresaInstalacionesCabecera = codigoEmpresa,
                                SucursalInstalacionesCabecera = codigoSucursal,
                                EmpleadoInstalacionesCabecera = empleadoAtiende.Empleadoempleadoatiendecallcenter,
                                ClienteInstalacionesCabecera = null,
                                FechaInstalacionesCabecera = DateTime.Now,
                                EstadoAsignacionInstalacionesCabecera = ((int)EstadoAsignacionInstalacion.ASIGNADO).ToString(),
                                EstadoInstalacionInstalacionesCabecera = ((int)TipoFinalizacionCallCeneter.POR_REALIZAR).ToString(),
                                CategoriasFinalizacionInstalacionesCabecera = ((int)CategoriasFinalizacionCallCenter.POR_REALIZAR).ToString(),
                                FinalizadoMatrizInstalacionesCabecera = 0,
                                LatitudInstalacionesCabecera = "0",
                                LongitudInstalacionesCabecera = "0",
                                BodegaOrigenInstalacionesCabecera = bodega,
                                SubTotalInstalacionesCabecera = 0,
                                SubTotal0InstalacionesCabecera = 0,
                                DescripcionInstalacionesCabecera = "",
                                ObservacionInstalacionesCabecera = infoLlamada.observacion,
                                LocalInstalacionesCabecera = "",
                                EstadoAutorizacionInstalacionesCabecera = "0",
                                FechaAsignacionInstalacionesCabecera = DateTime.Now,
                                FechaInstalacionInstalacionesCabecera = DateTime.Parse("1900-01-01"),
                                FechaAnulacionInstalacionesCabecera = DateTime.Parse("1900-01-01"),
                                TiposDocumentoInstalacionesCabecera = tipoDocumentoInstalacion.CodigoTipoDocumentoInstalacion,
                                CategoriasTiposDocumentosInstalacionesCabecera = categoriaTipoDocumentoLlamada.CodigoCategoriasTiposDocumentosInstalaciones,
                                NivelesPrioridadProcesosInstalacionesCabecera = (int)NivelPrioridad.MEDIA,
                                CodigoPadreInstalacionesCabecera = instalacionGrabada?.CodigoInstalacionesCabecera ?? "",
                                FechaRegistroAsignacionInstalacionesCabecera = DateTime.Now,
                                DocumentoOrigenInstalacionesCabecera = cita.AgendasCitaMedicaNavigation.CodigoAgenda,
                                UsuarioAsignaInstalacionesCabecera = empleadoLogeado.NombreUsuarioEmpleado,
                                UsuarioInstalacionInstalacionesCabecera = "",
                                UsuarioAnulaInstalacionesCabecera = ""
                            };
                            tipoDocumentoInstalacion.SecuenciaTipoDocumentoInstalacion += 1;
                            _context.Instalacionescabecera.Add(instalacionCabecera);
                            _context.Tiposdocumentosinstalaciones.Update(tipoDocumentoInstalacion);

                            //Generar documento pendiente
                            Docspendientes documentoPendiente = new Docspendientes()
                            {
                                DocsCabeceraDocPendiente = instalacionCabecera.CodigoInstalacionesCabecera,
                                CodigoDocPendiente = utils.generarCodigoFecha(),
                                TiposTramitesDocPendiente = AccionesDocumentos.POR_ATENDER.GetStringValue(),
                                PerfilesDocPendiente = empleadoAtiende.Empleadoempleadoatiendecallcenter,
                                PerfilesMailBccdocPendiente = empleadoAtiende.Empleadoempleadoatiendecallcenter,
                                CreacionDocPendiente = DateTime.Now,
                                AtencionDocPendiente = DateTime.Parse("1900-01-01"),
                                EstadoDocPendiente = EstadoDocPendiente.PENDIENTE,
                                TipoDocumentoDocPendiente = TipoDocumentoPendiente.CITA_MEDICA_LLAMADA
                            };
                            _context.Docspendientes.Add(documentoPendiente);

                            try
                            {
                                _context.SaveChanges();
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                        }
                        else
                        {
                            throw new AppException("No se ha encontrado el empleado que atiende el tipo de cita médica");
                        }
                    }
                    else
                    {
                        throw new AppException("No se ha encontrado el tipo de cita médica");
                    }
                }
                else
                {
                    throw new AppException("No se ha encontrado una bodega para la venta");
                }
            }
            else
            {
                throw new AppException("No se ha encontrado podido verificar la orden de la cita médica");
            }
        }

        public void grabarReagendamiento(Agendas agendaGrabada, Ordeninstalacion ordenGrabada, RegistrarLlamadaDto infoLlamada)
        {
            Solicitudcitasmedicas solicitud = agendaGrabada.Citasmedicas.SolicitudesCitaMedicaNavigation;
            string codigoPadre = solicitud?.CodigoSoliCitaMedica ?? ordenGrabada.ContratoCabeceraOrdenInstalacion;

            Empleados empleadoLogeado = _currentUserService.GetEmpleado();

            string codigoEmpresa = agendaGrabada.EmpleadosAgendaNavigation?.SucursalesEmpleadoNavigation.EmpresasSucursal ?? _appSettings.EmpresaDefecto;
            string codigoSucursal = agendaGrabada.EmpleadosAgendaNavigation?.SucursalesEmpleado ?? _appSettings.SucursalDefecto;


            //Ordeninstalacion ordenGrabada = _context.Instalacionescabecera
            //                                    .Include(i => i.OrdenInstalacionInstalacionesCabeceraNavigation)
            //                                    .Where(i => i.DocumentoOrigenInstalacionesCabecera == id)
            //                                    .Select(i => i.OrdenInstalacionInstalacionesCabeceraNavigation)
            //                                    .FirstOrDefault();

            Ordeninstalacion ultimaOrdeninstalacionGrabada = _context
                                                            .Ordeninstalacion
                                                            .Include(o => o.Instalacionescabecera)
                                                            .Where(o => o.CodigoOrdenInstalacion == ordenGrabada.CodigoOrdenInstalacion)
                                                            .OrderBy(i => i.FechaRegistroOrdenInstalacion)
                                                            .LastOrDefault();

            Instalacionescabecera ultimaInstalacionGrabada = ultimaOrdeninstalacionGrabada
                                                                .Instalacionescabecera
                                                                .OrderBy(i => i.FechaRegistroAsignacionInstalacionesCabecera)
                                                                .LastOrDefault();

            if (ultimaInstalacionGrabada != null)
            {
                atenderInstalacion(ultimaInstalacionGrabada);
                Docspendientes documentoPendienteGrabado = _context
                                                            .Docspendientes
                                                            .Where(doc => doc.DocsCabeceraDocPendiente == ultimaInstalacionGrabada.CodigoInstalacionesCabecera
                                                                        && doc.EstadoDocPendiente == EstadoDocPendiente.PENDIENTE)
                                                            .FirstOrDefault();

                if (documentoPendienteGrabado != null)
                {
                    atenderDocumentoPendiente(documentoPendienteGrabado, empleadoLogeado);
                }
            }

            Ordeninstalacion ordeninstalacionNueva = new Ordeninstalacion()
            {
                CodigoOrdenInstalacion = utils.generarCodigoFecha(),
                EmpresaOrdenInstalacion = codigoEmpresa,
                SucursalOrdenInstalacion = codigoSucursal,
                EmpleadoRegistroOrdenInstalacion = empleadoLogeado.CodigoEmpleado,
                ContratoCabeceraOrdenInstalacion = codigoPadre,
                ClienteOrdenInstalacion = ultimaOrdeninstalacionGrabada.ClienteOrdenInstalacion,
                FechaRegistroOrdenInstalacion = DateTime.Now,
                MotivosDocumentosInstalaciones = TipoDocumentoPendiente.CITA_MEDICA,
                EstadoOrdenInstalacion = (int)EstadoOrdenInstalacion.POR_REINGRESAR,
                UsuarioRegistroOrdenInstalacion = empleadoLogeado.NombreUsuarioEmpleado
            };
            _context.Ordeninstalacion.Add(ordeninstalacionNueva);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
