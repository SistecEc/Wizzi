using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Wizzi.Dtos.Localizaciones;
using Wizzi.Dtos.Sucursales;
using Wizzi.Entities;
using Wizzi.Extensions;
using Wizzi.Helpers;
using Wizzi.Models;
using Wizzi.Services;

namespace Wizzi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SucursalesController : ControllerBase
    {
        private DataContext _wiseContext;
        private readonly UserResolverService _currentUserService;
        private IMapper _mapper;

        public SucursalesController(
            DataContext wiseContext,
            IMapper mapper,
            UserResolverService currentUserService
            )
        {
            _wiseContext = wiseContext;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        [HttpGet("")]
        public IActionResult GetAll(bool paginado = true, int p = 1, string ciudad = "", bool soloParaAgendar = false)
        {
            Expression<Func<Sucursales, bool>> condicionSucursal = s => s.ActivaAgendamientoSucursal == 1;

            if (!string.IsNullOrEmpty(ciudad))
            {
                condicionSucursal = condicionSucursal.And(s => s.CiudadesSucursal == ciudad);
            }


            IQueryable<Sucursales> query = _wiseContext.Sucursales
                                            .Include(s => s.PaisSucursalNavigation)
                                            .Include(s => s.ProvinciaSucursalNavigation)
                                            .Include(s => s.CiudadesSucursalNavigation)
                                            .Include(s => s.ParroquiaSucursalNavigation);

            if (soloParaAgendar)
            {
                query = query
                        .Include(s => s.Permisossucursalagendar);
            }

            query = query
                    .Where(condicionSucursal)
                    .OrderBy(s => s.NombreSucursal);

            if (soloParaAgendar)
            {
                string empleadoActual = _currentUserService.GetCode();
                query = query
                        .Where(s => s.Permisossucursalagendar.Any(ps => ps.EmpleadosPermisoSucursalAgendar == empleadoActual));
            }

            if (paginado)
            {
                ResultadoPaginado<VerSucursalDto> sucursalesDto = query
                                                                    .GetPaged<Sucursales, VerSucursalDto>(p, 5, _mapper, AgregarExtrasSucursalDto);
                return Ok(sucursalesDto);
            }
            else
            {
                IEnumerable<VerSucursalDto> sucursalesDto = _mapper.Map<IEnumerable<VerSucursalDto>>(query);
                return Ok(sucursalesDto);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            Sucursales sucursal = _wiseContext.Sucursales
                                .Include(s => s.PaisSucursalNavigation)
                                .Include(s => s.ProvinciaSucursalNavigation)
                                .Include(s => s.CiudadesSucursalNavigation)
                                .Include(s => s.ParroquiaSucursalNavigation)
                                .Where(C => C.CodigoSucursal == id)
                                .FirstOrDefault();
            if (sucursal != null)
            {
                VerSucursalDto sucursalDto = AgregarExtrasSucursalDto(sucursal);
                return Ok(sucursalDto);
            }
            else
            {
                return NoContent();
            }
        }

        private VerSucursalDto AgregarExtrasSucursalDto(Sucursales sucursal)
        {
            VerSucursalDto sucursalDto = _mapper.Map<VerSucursalDto>(sucursal);
            sucursalDto.EsMatriz = sucursal.MatrizSucursal == "1";
            return sucursalDto;
        }

        [HttpGet("cantones")]
        public IActionResult GetCantonesSucursales(bool soloParaAgendar = false)
        {
            IQueryable<Sucursales> queryCiudades = _wiseContext.Sucursales
                                                            .Include(s => s.CiudadesSucursalNavigation);

            if (soloParaAgendar)
            {
                string empleadoActual = _currentUserService.GetCode();
                queryCiudades = queryCiudades
                                .Include(s => s.Permisossucursalagendar)
                                .Where(s => s.Permisossucursalagendar.Any(ps => ps.EmpleadosPermisoSucursalAgendar == empleadoActual));
            }

            IEnumerable<Localizacionescantones> ciudades = queryCiudades
                                                            .Where(s => s.ActivaAgendamientoSucursal == 1)
                                                            .Select(s => s.CiudadesSucursalNavigation)
                                                            .Distinct()
                                                            .OrderBy(s => s.NombreLocalizacionCanton);
            return Ok(_mapper.Map<IEnumerable<VerCantonDto>>(ciudades));
        }

    }
}