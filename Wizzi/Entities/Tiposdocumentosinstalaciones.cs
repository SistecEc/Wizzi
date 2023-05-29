using System.Collections.Generic;

namespace Wizzi.Entities
{
    public partial class Tiposdocumentosinstalaciones
    {
        public Tiposdocumentosinstalaciones()
        {
            Categoriastiposdocumentosinstalaciones = new HashSet<Categoriastiposdocumentosinstalaciones>();
            Empleadosatiendecallcenter = new HashSet<Empleadosatiendecallcenter>();
            Instalacionescabecera = new HashSet<Instalacionescabecera>();
        }

        public string CodigoTipoDocumentoInstalacion { get; set; }
        public string DescripcionTipoDocumentoInstalacion { get; set; }
        public int SecuenciaTipoDocumentoInstalacion { get; set; }
        public string HabilitadoTipoDocumentoInstalacion { get; set; }
        public string BloqueadoTipoDocumentoInstalacion { get; set; }
        public string UsuariosTipoDocumentoInstalacion { get; set; }

        public virtual ICollection<Categoriastiposdocumentosinstalaciones> Categoriastiposdocumentosinstalaciones { get; set; }
        public virtual ICollection<Empleadosatiendecallcenter> Empleadosatiendecallcenter { get; set; }
        public virtual ICollection<Instalacionescabecera> Instalacionescabecera { get; set; }
    }
}
