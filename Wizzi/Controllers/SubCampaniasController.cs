using AutoMapper;
using CSharpVitamins;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using Wizzi.Constants;
using Wizzi.Dtos.Subcampanias;
using Wizzi.Entities;
using Wizzi.Extensions;
using Wizzi.Helpers;
using Wizzi.Models;
using Z.EntityFramework.Plus;

namespace Wizzi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SubCampaniasController : ControllerBase
    {
        private DataContext _wiseContext;
        private IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public SubCampaniasController(
            DataContext wiseContext,
            IMapper mapper,
            IWebHostEnvironment env
            )
        {
            _wiseContext = wiseContext;
            _mapper = mapper;
            _env = env;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            Subcampanias subCampania = _wiseContext.Subcampanias.SingleOrDefault(s => s.CodigoSubCampania == id);
            if (subCampania == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(subCampania);
            }
        }

        [AllowAnonymous]
        [HttpGet("{id}/imagen")]
        public IActionResult GetImagenCampania(string id)
        {
            string imgSubcampania = _wiseContext.Subcampanias
                .Where(s => s.CodigoSubCampania == id)
                .Select(s => s.ImagenSubCampania)
                .FirstOrDefault();
            if (imgSubcampania == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(imgSubcampania);
            }
        }

        [AllowAnonymous]
        [HttpPost("")]
        public IActionResult registrarSubCampania([FromForm] ManejoSubCampaniasDto subCampaniaRecibida)
        {
            Campanias campaniaPadre = _wiseContext.Campanias.Find(subCampaniaRecibida.codigoCampania);
            if (campaniaPadre != null)
            {
                string nombreArchivo = "";
                if (subCampaniaRecibida.imagen != null)
                {
                    nombreArchivo = string.Concat(ShortGuid.NewGuid().Value,
                                                        Path.GetExtension(subCampaniaRecibida.imagen.FileName));
                    string pathAgrabar = Path.Combine(_env.WebRootPath, "imagenes", nombreArchivo);

                    subCampaniaRecibida.imagen.SaveAs(pathAgrabar);
                }

                Subcampanias subCampania = _mapper.Map<Subcampanias>(subCampaniaRecibida);
                subCampania.CodigoSubCampania = utils.generarCodigoFecha();
                subCampania.ImagenSubCampania = nombreArchivo;
                _wiseContext.Subcampanias.Add(subCampania);
                if (_wiseContext.SaveChanges() > 0)
                {
                    return Ok(_mapper.Map<SubCampaniasDto>(subCampania));
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

        [HttpPut("")]
        public IActionResult actualizarSubCampania([FromForm] ManejoSubCampaniasDto subCampaniaRecibida)
        {
            Subcampanias subCampaniaGrabada = _wiseContext.Subcampanias.Find(subCampaniaRecibida.codigo);
            if (subCampaniaGrabada != null)
            {
                subCampaniaGrabada.DescripcionSubCampania = subCampaniaRecibida.descripcion;
                if (subCampaniaRecibida.imagen != null)
                {
                    string nombreArchivo = "";
                    nombreArchivo = string.Concat(ShortGuid.NewGuid().Value,
                                                        Path.GetExtension(subCampaniaRecibida.imagen.FileName));
                    string pathAgrabar = Path.Combine(_env.WebRootPath, "imagenes", nombreArchivo);

                    subCampaniaRecibida.imagen.SaveAs(pathAgrabar);
                    subCampaniaGrabada.ImagenSubCampania = nombreArchivo;
                }
                subCampaniaGrabada.FechaInicioSubCampania = subCampaniaRecibida.fechaInicio.ToTimeZoneTime();
                subCampaniaGrabada.FechaFinSubCampania = subCampaniaRecibida.fechaFin.ToTimeZoneTime();

                _wiseContext.Subcampanias.Update(subCampaniaGrabada);
                if (_wiseContext.SaveChanges() > 0)
                {
                    return Ok(_mapper.Map<SubCampaniasDto>(subCampaniaGrabada));
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

        [HttpDelete("{idSubcampania}")]
        public IActionResult DeleteSubcampania(string idSubcampania)
        {
            Subcampanias subcampaniaGrabada = _wiseContext.Subcampanias
                                            .Include(s => s.Citasmedicas)
                                            .Include(s => s.Solicitudcitasmedicas)
                                            .Where(s => s.CodigoSubCampania == idSubcampania)
                                            .FirstOrDefault();
            if (subcampaniaGrabada != null)
            {
                if (subcampaniaGrabada.Solicitudcitasmedicas.Count == 0)
                {
                    if (subcampaniaGrabada.Citasmedicas.Count == 0)
                    {
                        _wiseContext.Subcampanias.Remove(subcampaniaGrabada);
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
                        return BadRequest(new msjRespuesta { codigo = codigosMensajes.NO_SE_PUEDE_GRABAR, detalle = "La subcampaña tiene citas médicas asignadas" });
                    }
                }
                else
                {
                    return BadRequest(new msjRespuesta { codigo = codigosMensajes.NO_SE_PUEDE_GRABAR, detalle = "La subcampaña tiene citas agendadas" });
                }
            }
            else
            {
                return NotFound(new msjRespuesta { codigo = codigosMensajes.NO_SE_PUEDE_ENCONTRAR });
            }
        }

    }
}