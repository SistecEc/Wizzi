

using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wizzi.Dtos.Reportes;
using Wizzi.Helpers;
using Wizzi.Interfaces;

namespace Wizzi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReporteController : ControllerBase
    {
        private DataContext _wiseContext;
        private IMapper _mapper;
        private IProcedureSql _procedureSql;
        private ICallCenterService _callCenter;
        public ReporteController(
                DataContext wiseContext,
                IMapper mapper,
                ICallCenterService callCenter,
                IProcedureSql procedureSql)
        {
            _procedureSql = procedureSql;
            _wiseContext = wiseContext;
            _mapper = mapper;
            _callCenter = callCenter;
        }

        [HttpGet("CitasAtendidas/{idSucursal}/{idEmpleado}/{fechaInicio}/{fechaFinal}/{estadoAgenda}")]
        public IActionResult GetReporte(string idSucursal = "", string idEmpleado = "", string fechaInicio = "", string fechaFinal = "", string estadoAgenda = "0")
        {
            if (idSucursal == null)
            {
                idSucursal = "";
            }
            if (idEmpleado == null)
            {
                idEmpleado = "";
            }

            //int leads = _wiseContext.Solicitudcitasmedicas.Where(x => x.FechaRegistroSoliCitaMedica >= Convert.ToDateTime(fechaInicio != "" ? fechaInicio : "12/12/2021") && x.FechaRegistroSoliCitaMedica <= Convert.ToDateTime(fechaFinal != "" ? fechaFinal : "12/12/2021")).Count();
            string cadena = returnSelectCountLEAD(fechaInicio, fechaFinal, idSucursal, idEmpleado);
            int leads = _wiseContext.Agendas.FromSqlRaw(cadena).Count();
            var param = new MySqlParameter[] {
                        new MySqlParameter() {ParameterName = "@ParSucursal",Value = idSucursal},
                        new MySqlParameter() {ParameterName = "@ParEmpleado ",Value = idEmpleado},
                        new MySqlParameter() {ParameterName = "@ParFechainicio",Value = fechaInicio},
                        new MySqlParameter() {ParameterName = "@ParFechafin",Value = fechaFinal},
                        new MySqlParameter() {ParameterName = "@ParEstadoAgenda",Value = 1}
                        };
            List<RepAgendamientoAtencion> result = _procedureSql.ExecuteProcedureSql("ReporteAgendamiento", param);
            List<RepAgendamientoAtencion> resultadoDTO = result;

            switch (estadoAgenda)
            {
                case "0":
                    resultadoDTO = resultadoDTO.Where(x => x.campania != null).ToList();
                    break;
                case "1":
                    resultadoDTO = resultadoDTO.Where(x => x.agendado == "SI").ToList();
                    break;
                case "2":
                    resultadoDTO = resultadoDTO.Where(x => x.atendido == "SI").ToList();
                    break;
                case "3":
                    resultadoDTO = resultadoDTO.Where(x => x.candidato == "SI").ToList();
                    break;
                case "4":
                    resultadoDTO = resultadoDTO.Where(x => x.vendido == "SI").ToList();
                    break;
            }
            ResultReportAgendamiento reporte = new ResultReportAgendamiento();
            reporte.cantidadLEAD = leads;
            reporte.data = resultadoDTO;
            return Ok(reporte);
        }

        public string returnSelectCountLEAD(string fechaInicio, string fechaFinal, string idsucursal, string idempleado)
        {
            string cadena = "SELECT	solicitudcitasmedicas.CodigoSoliCitaMedica as CodSoliCitaMedica,";
            cadena += " citamedicas.CodigoCitaMedica as codCitaMedica,";
            cadena += " SucursalesSoliCitaMedica as idsucursal,";
            cadena += " citamedicas.idEmpleado";
            cadena += " FROM solicitudcitasmedicas ";
            cadena += " LEFT JOIN (";
            cadena += " SELECT CodigoCitaMedica, EmpleadosAgenda, Max(FechaRegistroCitaMedica), SolicitudesCitaMedica, agendas.EmpleadosAgenda as idEmpleado";
            cadena += " from citasmedicas ";
            cadena += " INNER JOIN agendas ON citasmedicas.AgendasCitaMedica = agendas.CodigoAgenda ";
            cadena += " where SolicitudesCitaMedica ";
            cadena += " and agendas.EstadoAgenda = '1' ";
            cadena += " group by codigoGrupoCitaMedica ";
            cadena += " ) as citamedicas on citamedicas.SolicitudesCitaMedica = solicitudcitasmedicas.CodigoSoliCitaMedica ";
            cadena += $" where  FechaRegistroSoliCitaMedica>='{fechaInicio}'  ";
            cadena += $" and FechaRegistroSoliCitaMedica<='{Convert.ToDateTime(fechaFinal).AddDays(1).ToString("yyyy-MM-dd")}' ";
            if (idsucursal != "")
            {
                cadena += $" and SucursalesSoliCitaMedica = '{idsucursal}' ";
            }
            if (idempleado != "")
            {
                cadena += $" and  citamedicas.idEmpleado = '{idempleado}' ";
            }
            return cadena;
        }
    }
}
