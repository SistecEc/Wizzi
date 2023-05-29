using System.Collections.Generic;

namespace Wizzi.Entities
{
    public partial class Cuentascontabilidad
    {
        public Cuentascontabilidad()
        {
            Tiposajustes = new HashSet<Tiposajustes>();
        }

        public string CodigoCuentaContable { get; set; }
        public string NombreCuentaContable { get; set; }
        public string TipoCuentaContable { get; set; }
        public string ConciliarCuentaContable { get; set; }
        public string PermitirIngresarAsientoCuentaContable { get; set; }
        public string UsuariosCuentaContabilidad { get; set; }

        public virtual ICollection<Tiposajustes> Tiposajustes { get; set; }
    }
}
