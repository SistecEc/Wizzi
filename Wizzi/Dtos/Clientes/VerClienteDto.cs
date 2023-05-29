using System;

namespace Wizzi.Dtos.Clientes
{
    public class VerClienteDto
    {
        public string Codigo { get; set; }
        public tipoIdentificacionDto tipoIdentificacion { get; set; }
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
        public LocalizacionDto localizacion { get; set; }
    }
}
