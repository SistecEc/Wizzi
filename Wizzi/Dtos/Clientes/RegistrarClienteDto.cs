using System;

namespace Wizzi.Dtos.Clientes
{
    public class RegistrarClienteDto
    {
        public string Codigo { get; set; }
        public string TipoIdentificacion { get; set; }
        public string Identificacion { get; set; }
        public string NombreComercial { get; set; }
        public bool PrioridadNombreComercial { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Genero { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public LocalizacionSimpleDto localizacion { get; set; }
    }

    public class LocalizacionSimpleDto
    {
        public string Pais { get; set; }
        public string Provincia { get; set; }
        public string Canton { get; set; }
        public string Parroquia { get; set; }
    }
}
