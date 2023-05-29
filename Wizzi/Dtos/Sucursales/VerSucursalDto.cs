using Wizzi.Dtos.Localizaciones;

namespace Wizzi.Dtos.Sucursales
{
    public class VerSucursalDto
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public bool EsMatriz { get; set; }
        public Localizacion localizacion { get; set; }
    }

    public class Localizacion
    {
        public VerPaisDto Pais { get; set; }
        public VerProvinciaDto Provincia { get; set; }
        public VerCantonDto Canton { get; set; }
        public VerParroquiaDto Parroquia { get; set; }
    }

}
