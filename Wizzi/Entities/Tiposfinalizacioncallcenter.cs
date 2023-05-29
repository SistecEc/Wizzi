using System.Collections.Generic;

namespace Wizzi.Entities
{
    public partial class Tiposfinalizacioncallcenter
    {
        public Tiposfinalizacioncallcenter()
        {
            Categoriasfinalizacioncallcenter = new HashSet<Categoriasfinalizacioncallcenter>();
            Instalacionescabecera = new HashSet<Instalacionescabecera>();
        }

        public string CodigoTipoFinalizacionCallCenter { get; set; }
        public string DescripcionTipoFinalizacionCallCenter { get; set; }
        public int GeneraDocPendienteTipoFinalizacionCallCenter { get; set; }
        public string UsuariosTipoFinalizacionCallCenter { get; set; }

        public virtual ICollection<Categoriasfinalizacioncallcenter> Categoriasfinalizacioncallcenter { get; set; }
        public virtual ICollection<Instalacionescabecera> Instalacionescabecera { get; set; }
    }
}
