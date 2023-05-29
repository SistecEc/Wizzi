using System.Collections.Generic;

namespace Wizzi.Entities
{
    public partial class Perfiles
    {
        public Perfiles()
        {
            Empleados = new HashSet<Empleados>();
        }

        public string CodigoPerfil { get; set; }
        public string NombrePerfil { get; set; }

        public virtual ICollection<Empleados> Empleados { get; set; }
    }
}
