using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Wizzi.Helpers;

namespace Wizzi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CiudadesController : ControllerBase
    {
        private DataContext _wiseContext;
        public CiudadesController(DataContext wiseContext)
        {
            _wiseContext = wiseContext;
        }

        [HttpGet("")]
        public IActionResult GetCiudades()
        {
            var ciudades = _wiseContext.Localizacionescantones
                .Select(c => new { c.CodigoLocalizacionCanton, c.NombreLocalizacionCanton });

            return Ok(ciudades);
        }

    }
}