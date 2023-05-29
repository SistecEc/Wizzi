using System.Collections.Generic;

namespace Wizzi.Entities
{
    public partial class Tiposidentificacion
    {
        public Tiposidentificacion()
        {
            Clientes = new HashSet<Clientes>();
            EmpleadosTipoIdentificacionReemplazaEmpleadoNavigation = new HashSet<Empleados>();
            EmpleadosTiposIdentificacionEmpleadoNavigation = new HashSet<Empleados>();
        }

        public string CodigoTipoIdentificacion { get; set; }
        public string CodigoSricompraTipoIdentificacion { get; set; }
        public string CodigoSriventaTipoIdentificacion { get; set; }
        public string CodigoSrianexoTipoIdentificacion { get; set; }
        public string NombreTipoIdentificacion { get; set; }
        public uint NumeroCaracterTipoIdentificacion { get; set; }
        public string UsuariosTipoIdentificacion { get; set; }

        public virtual ICollection<Clientes> Clientes { get; set; }
        public virtual ICollection<Empleados> EmpleadosTipoIdentificacionReemplazaEmpleadoNavigation { get; set; }
        public virtual ICollection<Empleados> EmpleadosTiposIdentificacionEmpleadoNavigation { get; set; }
    }
}
