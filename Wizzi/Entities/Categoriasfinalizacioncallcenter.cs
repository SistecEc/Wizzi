using System.Collections.Generic;

namespace Wizzi.Entities
{
    public partial class Categoriasfinalizacioncallcenter
    {
        public Categoriasfinalizacioncallcenter()
        {
            Instalacionescabecera = new HashSet<Instalacionescabecera>();
        }

        public string CodigoCategoriaFinalizacionCallCenter { get; set; }
        public string TiposFinalizacionCategoriaFinalizacionCallCenter { get; set; }
        public string DescripcionCategoriaFinalizacionCallCenter { get; set; }
        public string UsuariosCategoriaFinalizacionCallCenter { get; set; }

        public virtual Tiposfinalizacioncallcenter TiposFinalizacionCategoriaFinalizacionCallCenterNavigation { get; set; }
        public virtual ICollection<Instalacionescabecera> Instalacionescabecera { get; set; }
    }
}
