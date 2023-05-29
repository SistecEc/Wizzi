using System.Collections.Generic;

namespace Wizzi.Entities
{
    public partial class Formareclamo
    {
        public Formareclamo()
        {
            Ordeninstalacion = new HashSet<Ordeninstalacion>();
        }

        public string CodigoFormaReclamo { get; set; }
        public string DescripcionFormaReclamo { get; set; }
        public string UsuarioReclamoFormaReclamo { get; set; }

        public virtual ICollection<Ordeninstalacion> Ordeninstalacion { get; set; }
    }
}
