using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Wizzi.Helpers;

namespace Wizzi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FuentesRemisionController : ControllerBase
    {
        private DataContext _wiseContext;
        public FuentesRemisionController(DataContext wiseContext)
        {
            _wiseContext = wiseContext;
        }

        [HttpGet("")]
        public IActionResult GetFuentes()
        {
            var tipos = _wiseContext.Fuentesremision
                .Select(fuente => new
                {
                    codigo = fuente.CodigoFuenteRemision,
                    descripcion = fuente.DescripcionFuenteRemision
                });

            return Ok(tipos);
        }

    }
}