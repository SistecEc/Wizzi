using Wizzi.Dtos.Localizaciones;

namespace Wizzi.Dtos.Clientes
{
    public class LocalizacionDto
    {
        public VerPaisDto Pais { get; set; }
        public VerProvinciaDto Provincia { get; set; }
        public VerCantonDto Canton { get; set; }
        public VerParroquiaDto Parroquia { get; set; }
    }
}
