using System;
using Wizzi.Enums;

namespace Wizzi.Dtos.Movimientos
{
    public class VerMovimientosDto
    {
        public string codigo { get; set; }
        public string titulo { get; set; }
        public string descripcion { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFin { get; set; }
        public DateTime fechaRegistro { get; set; }
        public string nombreEmpleadoAsignado { get; set; }
        public string apellidoEmpleadoAsignado { get; set; }
        public string usuarioAsigna { get; set; }
        public string nombreEmpleadoAsigna { get; set; }
        public string apellidoEmpleadoAsigna { get; set; }
        public DateTime fechaUltimaModificacion { get; set; }
        public TipoMovimientoCita tipoMovimiento { get; set; }
        public int estado { get; set; }
    }
}
