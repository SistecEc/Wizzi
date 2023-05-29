using System;

namespace Wizzi.Entities
{
    public partial class Observacionesempleadosinstalaciones
    {
        public string CodigoObservacionEmpleadoInstalacion { get; set; }
        public string InstalacionesCabeceraObservacionEmpleadoInstalacion { get; set; }
        public string ObservacionObservacionEmpleadoInstalacion { get; set; }
        public string EmpleadosObservacionEmpleadosInstalacion { get; set; }
        public string OrigenesObservacionesObservacionEmpleadosInstalacion { get; set; }
        public string EquiposEmpleadoObservacionEmpleado { get; set; }
        public DateTime FechaObservacionEmpleado { get; set; }

        public virtual Instalacionescabecera InstalacionesCabeceraObservacionEmpleadoInstalacionNavigation { get; set; }
    }
}
