using System.Collections.Generic;

namespace Wizzi.Entities
{
    public partial class Tiposclientescartera
    {
        public Tiposclientescartera()
        {
            Clientes = new HashSet<Clientes>();
        }

        public string CodTipoClienteCartera { get; set; }
        public string DescripcionTipoClienteCartera { get; set; }
        public string UsuariosTipoClienteCartera { get; set; }

        public virtual ICollection<Clientes> Clientes { get; set; }
    }
}
