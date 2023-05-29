using System.Collections.Generic;

namespace Wizzi.Entities
{
    public partial class Titulos
    {
        public Titulos()
        {
            Empleados = new HashSet<Empleados>();
        }

        public string CodigoTitulo { get; set; }
        public string NombreTitulo { get; set; }
        public string UsuariosTitulo { get; set; }

        public virtual ICollection<Empleados> Empleados { get; set; }
    }
}
