using System.Collections.Generic;

namespace Wizzi.Entities
{
    public partial class Categoriastiposdocumentosinstalaciones
    {
        public Categoriastiposdocumentosinstalaciones()
        {
            Citasmedicas = new HashSet<Citasmedicas>();
            Empleadosatiendecallcenter = new HashSet<Empleadosatiendecallcenter>();
            Instalacionescabecera = new HashSet<Instalacionescabecera>();
            Parametros = new HashSet<Parametros>();
        }

        public string CodigoCategoriasTiposDocumentosInstalaciones { get; set; }
        public string TiposDocumentosInstalacionesCategoriaTiposDocumentoInstalaciones { get; set; }
        public string DescripcionCategoriasTiposDocumentosInstalaciones { get; set; }
        public string HabilitadoCategoriasTiposDocumentosInstalaciones { get; set; }
        public int OrdenCategoriasTiposDocumentosInstalaciones { get; set; }
        public string UsuariosCategoriasTiposDocumentosInstalaciones { get; set; }

        public virtual Tiposdocumentosinstalaciones TiposDocumentosInstalacionesCategoriaTiposDocumentoInstalacionesNavigation { get; set; }
        public virtual ICollection<Citasmedicas> Citasmedicas { get; set; }
        public virtual ICollection<Empleadosatiendecallcenter> Empleadosatiendecallcenter { get; set; }
        public virtual ICollection<Instalacionescabecera> Instalacionescabecera { get; set; }
        public virtual ICollection<Parametros> Parametros { get; set; }
    }
}
