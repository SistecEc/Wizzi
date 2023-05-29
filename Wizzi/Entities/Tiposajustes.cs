using System.Collections.Generic;

namespace Wizzi.Entities
{
    public partial class Tiposajustes
    {
        public Tiposajustes()
        {
            Parametros = new HashSet<Parametros>();
        }

        public string CodigoTipoAjuste { get; set; }
        public string NombreTipoAjuste { get; set; }
        public string CuentaContableTipoAjuste { get; set; }
        public string AfectaCostoTipoAjuste { get; set; }
        public string UsuariosTipoAjuste { get; set; }

        public virtual Cuentascontabilidad CuentaContableTipoAjusteNavigation { get; set; }
        public virtual ICollection<Parametros> Parametros { get; set; }
    }
}
