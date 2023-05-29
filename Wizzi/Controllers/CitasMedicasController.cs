using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Wizzi.Dtos.CitasMedicas;
using Wizzi.Dtos.Llamadas;
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
    public class CitasMedicasController : ControllerBase
    {
        private DataContext _wiseContext;
        private IMapper _mapper;
        private ICallCenterService _callCenter;

        public CitasMedicasController(
            DataContext wiseContext,
            IMapper mapper,
            ICallCenterService callCenter
            )
        {
            _wiseContext = wiseContext;
            _mapper = mapper;
            _callCenter = callCenter;
        }

        [HttpGet]
        public IActionResult GetAll(int p = 1)
        {
            ResultadoPaginado<VerCitaMedicaDto> citasDtos = _wiseContext.Citasmedicas
                                                                        .Include(c => c.ClientesCitaMedicaNavigation)
                                                                        .Include(c => c.AgendasCitaMedicaNavigation)
                                                                            .ThenInclude(a => a.EmpleadosAgendaNavigation)
                                                                        .Include(c => c.FuentesRemisionCitaMedicaNavigation)
                                                                        .Where(c => c.AgendasCitaMedicaNavigation.EstadoAgenda != (int)EstadoAgenda.CANCELADA) //c.ActivaCitaMedica == 1 &&
                                                                        .GroupBy(c => c.CodigoGrupoCitaMedica)
                                                                        .Select(c => c.OrderByDescending(co => co.FechaRegistroCitaMedica).FirstOrDefault())
                                                                        .GetPaged<Citasmedicas, VerCitaMedicaDto>(p, 5, _mapper, AgregarExtrasCitaMedicaDto);
            return Ok(citasDtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            Citasmedicas citaMedica = _wiseContext.Citasmedicas
                                                .Include(c => c.ClientesCitaMedicaNavigation)
                                                .Include(c => c.AgendasCitaMedicaNavigation)
                                                    .ThenInclude(a => a.EmpleadosAgendaNavigation)
                                                .Include(c => c.FuentesRemisionCitaMedicaNavigation)
                                                .Where(cm => cm.CodigoCitaMedica == id)
                                                .FirstOrDefault();
            if (citaMedica != null)
            {
                VerCitaMedicaDto verCitaMedica = AgregarExtrasCitaMedicaDto(citaMedica);
                return Ok(verCitaMedica);
            }
            return NoContent();
        }

        [HttpPost("{id}/registrarLlamada")]
        public IActionResult PostRegistrarLlamada(string id, RegistrarLlamadaDto datosLlamada)
        {
            Citasmedicas citaMedica = _wiseContext.Citasmedicas
                                                    .Include(c => c.AgendasCitaMedicaNavigation)
                                                        .ThenInclude(s => s.EmpleadosAgendaNavigation)
                                                            .ThenInclude(s => s.SucursalesEmpleadoNavigation)
                                                                .ThenInclude(s => s.EmpresasSucursalNavigation)
                                                    .Include(c => c.SolicitudesCitaMedicaNavigation)
                                                    .Where(c => c.CodigoCitaMedica == id)
                                                    .FirstOrDefault();
            if (citaMedica != null)
            {
                try
                {
                    _callCenter.grabarLlamadaCita(citaMedica, datosLlamada);
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

        private VerCitaMedicaDto AgregarExtrasCitaMedicaDto(Citasmedicas cita)
        {
            VerCitaMedicaDto verCitaMedica = _mapper.Map<VerCitaMedicaDto>(cita);
            verCitaMedica.cantidadReagendados = _wiseContext.Citasmedicas
                                                    .Where(c => c.CodigoGrupoCitaMedica == cita.CodigoGrupoCitaMedica)
                                                    .Count() - 1;
            verCitaMedica.activa = cita.ActivaCitaMedica == 1;

            return verCitaMedica;
        }

    }
}