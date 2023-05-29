
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Wizzi.Enums;
using Wizzi.Helpers;

namespace Wizzi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TiposIdentificacionController : ControllerBase
    {
        private DataContext _wiseContext;
        public TiposIdentificacionController(DataContext wiseContext)
        {
            _wiseContext = wiseContext;
        }

        [HttpGet("")]
        public IActionResult GetTipos()
        {
            var tipos = _wiseContext.Tiposidentificacion
                        .Where(tipo => tipo.CodigoTipoIdentificacion != ((int)TipoIdentificacion.CONSUMIDOR_FINAL).ToString())
                        .Select(tipo => new
                        {
                            codigo = tipo.CodigoTipoIdentificacion,
                            descripcion = tipo.NombreTipoIdentificacion
                        });

            return Ok(tipos);
        }

    }
}