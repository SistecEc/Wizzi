
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
    public class TiposCitasMedicasController : ControllerBase
    {
        private DataContext _wiseContext;
        public TiposCitasMedicasController(DataContext wiseContext)
        {
            _wiseContext = wiseContext;
        }

        [HttpGet("")]
        public IActionResult GetTipos()
        {
            var tipos = _wiseContext.Categoriastiposdocumentosinstalaciones
                .Where(categoria => categoria.TiposDocumentosInstalacionesCategoriaTiposDocumentoInstalaciones == ((int)TipoDocumentoInstalacionCallCenter.ATENCION_CITA_AGENDADA).ToString())
                .OrderBy(categoria => categoria.OrdenCategoriasTiposDocumentosInstalaciones)
                .Select(categoria => new
                {
                    codigo = categoria.CodigoCategoriasTiposDocumentosInstalaciones,
                    descripcion = categoria.DescripcionCategoriasTiposDocumentosInstalaciones
                });

            return Ok(tipos);
        }

    }
}