using AutoMapper;
using CSharpVitamins;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Wizzi.Constants;
using Wizzi.Dtos.Campanias;
using Wizzi.Entities;
using Wizzi.Extensions;
using Wizzi.Helpers;
using Wizzi.Models;

namespace Wizzi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CampaniasController : ControllerBase
    {
        private DataContext _wiseContext;
        private IMapper _mapper;
        public CampaniasController(
            DataContext wiseContext,
            IMapper mapper
            )
        {
            _wiseContext = wiseContext;
            _mapper = mapper;
        }

        [HttpPost("")]
        public IActionResult registrarCampania([FromBody] CampaniaDto campaniaRecibida)
        {
            Campanias campaniaGrabada = _mapper.Map<Campanias>(campaniaRecibida);
            campaniaGrabada.CodigoCampania = ShortGuid.NewGuid().Value;
            _wiseContext.Campanias.Add(campaniaGrabada);
            if (_wiseContext.SaveChanges() > 0)
            {
                return Ok(_mapper.Map<CampaniaDto>(campaniaGrabada));
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("")]
        public IActionResult actualizarCampania([FromBody] CampaniaDto campaniaRecibida)
        {
            Campanias campaniaGrabada = _wiseContext.Campanias.Find(campaniaRecibida.codigo);
            if (campaniaGrabada != null)
            {
                campaniaGrabada.TituloCampania = campaniaRecibida.titulo;
                campaniaGrabada.DescripcionCampania = campaniaRecibida.descripcion;
                campaniaGrabada.PresupuestoCampania = campaniaRecibida.presupuesto;
                campaniaGrabada.FechaInicioCampania = campaniaRecibida.fechaInicio.ToTimeZoneTime();
                campaniaGrabada.FechaFinCampania = campaniaRecibida.fechaFin.ToTimeZoneTime();

                _wiseContext.Campanias.Update(campaniaGrabada);
                if (_wiseContext.SaveChanges() > 0)
                {
                    return Ok(_mapper.Map<CampaniaDto>(campaniaGrabada));
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return NotFound();
            }

        }

        [HttpGet]
        public IActionResult GetAll(int p = 1)
        {
            ResultadoPaginado<CampaniaDto> campaniasDtos = _wiseContext.Campanias
                                                            .Include(c => c.Subcampanias)
                                                            .OrderByDescending(f =>f.FechaInicioCampania)
                                                            .GetPaged<Campanias, CampaniaDto>(p, 5, _mapper, AgregarExtrasCampaniaDto);
            return Ok(campaniasDtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            Campanias campania = _wiseContext.Campanias
                .Include(c => c.Subcampanias)
                .Where(C => C.CodigoCampania == id)
                .FirstOrDefault();
            if (campania != null)
            {
                CampaniaDto campaniaDto = AgregarExtrasCampaniaDto(campania);
                return Ok(campaniaDto);
            }
            else
            {
                return NoContent();
            }
        }

        private CampaniaDto AgregarExtrasCampaniaDto(Campanias campania)
        {
            CampaniaDto campaniaDto = _mapper.Map<CampaniaDto>(campania);
            campaniaDto.cantidadSubCampanias = campania.Subcampanias.Count();
            //campaniaDto.PrimerasSubcampanias= new List<SubCampaniasDto>();
            //campania.Subcampanias.Take(3).ToList().ForEach(s =>
            //{
            //    campaniaDto.PrimerasSubcampanias.Add(_mapper.Map<SubCampaniasDto>(s));
            //});
            campaniaDto.PrimerasSubcampanias = campania.Subcampanias.Take(3).Select(s => _mapper.Map<SubCampaniasDto>(s)).ToList();
            //campania.Subcampanias.Take(3).ToList().ForEach(s =>
            //{
            //    campaniaDto.PrimerasSubcampanias.Add(_mapper.Map<SubCampaniasDto>(s));
            //});
            return campaniaDto;
        }

        [HttpDelete("{idCampania}")]
        public IActionResult DeleteCampania(string idCampania)
        {
            Campanias campaniaGrabada = _wiseContext.Campanias
                                            .Include(s => s.Subcampanias)
                                            .Where(s => s.CodigoCampania == idCampania)
                                            .FirstOrDefault();
            if (campaniaGrabada != null)
            {
                if (campaniaGrabada.Subcampanias.Count == 0)
                {
                    _wiseContext.Campanias.Remove(campaniaGrabada);
                    try
                    {
                        _wiseContext.SaveChanges();
                        return Ok(new msjRespuesta());
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(new msjRespuesta { codigo = codigosMensajes.ERROR_AL_GRABAR, detalle = ex.Message });
                    }
                }
                else
                {
                    return BadRequest(new msjRespuesta { codigo = codigosMensajes.NO_SE_PUEDE_GRABAR, detalle = "La campaña tiene subcampañas" });
                }
            }
            else
            {
                return NotFound(new msjRespuesta { codigo = codigosMensajes.NO_SE_PUEDE_ENCONTRAR });
            }
        }

        [HttpGet("{id}/subCampanias")]
        public IActionResult GetSubCampanias(string id)
        {
            List<Subcampanias> subcampanias = _wiseContext.Subcampanias
                .Where(s => s.CampaniasSubCampania == id)
                .ToList();
            if (subcampanias.Count > 0)
            {
                return Ok(_mapper.Map<List<SubCampaniasDto>>(subcampanias));
            }
            else
            {
                return NoContent();
            }
        }

    }
}