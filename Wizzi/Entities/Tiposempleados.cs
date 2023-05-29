using System.Collections.Generic;

namespace Wizzi.Entities
{
    public partial class Tiposempleados
    {
        public Tiposempleados()
        {
            Empleados = new HashSet<Empleados>();
        }

        public string CodigoTipoEmpleado { get; set; }
        public string NombreTipoEmpleado { get; set; }
        public string EsVendedorTipoEmpleado { get; set; }
        public string UsuariosTipoEmpleado { get; set; }

        public virtual ICollection<Empleados> Empleados { get; set; }
    }
}
