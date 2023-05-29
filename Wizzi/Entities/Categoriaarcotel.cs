using System.Collections.Generic;

namespace Wizzi.Entities
{
    public partial class Categoriaarcotel
    {
        public Categoriaarcotel()
        {
            Ordeninstalacion = new HashSet<Ordeninstalacion>();
        }

        public string CodigoCategoriaAlcotel { get; set; }
        public string DescripcionCategoriaCategoriaArcotel { get; set; }
        public string UsuarioCategoriaCategoriaAlcotel { get; set; }

        public virtual ICollection<Ordeninstalacion> Ordeninstalacion { get; set; }
    }
}
