
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Wizzi.Dtos.Empleados;
using Wizzi.Entities;
using Wizzi.Extensions;
using Wizzi.Helpers;
using Wizzi.Models;

namespace Wizzi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EmpleadosController : ControllerBase
    {
        private DataContext _wiseContext;
        private IMapper _mapper;
        public EmpleadosController(
            DataContext wiseContext,
            IMapper mapper
            )
        {
            _wiseContext = wiseContext;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetEmpleados(bool paginado = true, int p = 1, bool soloRol = true, string ciudad = "", string sucursal = "", bool soloPuedeAgendar = false)
        {
            Expression<Func<Empleados, bool>> condicionEmpleado = e => true;
            if (soloRol)
            {
                condicionEmpleado = condicionEmpleado.And(e => e.RolPagoEmpleado == "1");
            }
            if (!string.IsNullOrEmpty(sucursal))
            {
                condicionEmpleado = condicionEmpleado.And(e => e.SucursalesEmpleado == sucursal);
            }
            if (!string.IsNullOrEmpty(ciudad))
            {
                condicionEmpleado = condicionEmpleado.And(e => e.SucursalesEmpleadoNavigation.CiudadesSucursal == ciudad);
            }
            if (soloPuedeAgendar)
            {
                condicionEmpleado = condicionEmpleado.And(e => e.PermiteAgendamientoEmpleados == 1);
            }
            IQueryable<Empleados> query = _wiseContext.Empleados
                                                .Include(e => e.SucursalesEmpleadoNavigation)
                                                .Where(condicionEmpleado)
                                                .OrderBy(e => e.ApellidoEmpleado + " " + e.NombreEmpleado);

            if (paginado)
            {
                ResultadoPaginado<VerEmpleadoDto> paisesDto = query
                                                        .GetPaged<Empleados, VerEmpleadoDto>(p, 15, _mapper);
                return Ok(paisesDto);
            }
            else
            {
                IEnumerable<VerEmpleadoDto> paisesDto = _mapper.Map<IEnumerable<VerEmpleadoDto>>(query);
                return Ok(paisesDto);
            }
        }

    }
}