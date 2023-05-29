using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Wizzi.Dtos.Localizaciones;
using Wizzi.Entities;
using Wizzi.Extensions;
using Wizzi.Helpers;
using Wizzi.Models;

namespace Wizzi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LocalizacionesController : ControllerBase
    {
        private DataContext _wiseContext;
        private IMapper _mapper;
        public LocalizacionesController(
            DataContext wiseContext,
            IMapper mapper
            )
        {
            _wiseContext = wiseContext;
            _mapper = mapper;
        }

        [HttpGet("paises")]
        public IActionResult GetPaises(bool paginado = true, int p = 1)
        {
            IQueryable<Localizacionespaises> query = _wiseContext.Localizacionespaises
                                                        .OrderBy(p => p.NombreLocalizacionPais);
            if (paginado)
            {
                ResultadoPaginado<VerPaisDto> paisesDto = query
                                                        .GetPaged<Localizacionespaises, VerPaisDto>(p, 15, _mapper);
                return Ok(paisesDto);
            }
            else
            {
                List<VerPaisDto> paisesDto = _mapper.Map<List<VerPaisDto>>(query);
                return Ok(paisesDto);
            }
        }

        [HttpGet("provincias")]
        public IActionResult GetProvincias(string pais = "", bool paginado = true, int p = 1)
        {
            Expression<Func<Localizacionesprovincias, bool>> condicionPais = p => true;
            if (!string.IsNullOrEmpty(pais))
            {
                condicionPais = l => l.PaisesLocalizacionProvincia == pais;
            }

            IQueryable<Localizacionesprovincias> query = _wiseContext.Localizacionesprovincias
                                                        .OrderBy(p => p.NombreLocalizacionProvincia)
                                                        .Where(condicionPais);
            if (paginado)
            {
                ResultadoPaginado<VerProvinciaDto> provinciasDto = query
                                                                .GetPaged<Localizacionesprovincias, VerProvinciaDto>(p, 15, _mapper);
                return Ok(provinciasDto);
            }
            else
            {
                List<VerProvinciaDto> provinciasDto = _mapper.Map<List<VerProvinciaDto>>(query);
                return Ok(provinciasDto);
            }
        }

        [HttpGet("cantones")]
        public IActionResult GetCantones(string provincia = "", bool paginado = true, int p = 1)
        {
            Expression<Func<Localizacionescantones, bool>> condicionProvincia = pl => true;
            if (!string.IsNullOrEmpty(provincia))
            {
                condicionProvincia = l => l.ProvinciasLocalizacionCanton == provincia;
            }

            IQueryable<Localizacionescantones> query = _wiseContext.Localizacionescantones
                                                        .OrderBy(p => p.NombreLocalizacionCanton)
                                                        .Where(condicionProvincia);
            if (paginado)
            {
                ResultadoPaginado<VerCantonDto> cantonesDto = query
                                                            .GetPaged<Localizacionescantones, VerCantonDto>(p, 15, _mapper);
                return Ok(cantonesDto);
            }
            else
            {
                List<VerCantonDto> cantonesDto = _mapper.Map<List<VerCantonDto>>(query);
                return Ok(cantonesDto);
            }
        }

        [HttpGet("parroquias")]
        public IActionResult GetParroquias(string canton = "", bool paginado = true, int p = 1)
        {
            Expression<Func<Localizacionesparroquias, bool>> condicionCanton = p => true;
            if (!string.IsNullOrEmpty(canton))
            {
                condicionCanton = l => l.LocalizacionesCantonesLocalizacionParroquia == canton;
            }

            IQueryable<Localizacionesparroquias> query = _wiseContext.Localizacionesparroquias
                                                        .OrderBy(p => p.NombreLocalizacionParroquia)
                                                        .Where(condicionCanton);
            if (paginado)
            {
                ResultadoPaginado<VerParroquiaDto> parroquiasDto = query
                                                                .GetPaged<Localizacionesparroquias, VerParroquiaDto>(p, 15, _mapper);
                return Ok(parroquiasDto);
            }
            else
            {
                List<VerParroquiaDto> parroquiasDto = _mapper.Map<List<VerParroquiaDto>>(query);
                return Ok(parroquiasDto);
            }
        }

    }
}