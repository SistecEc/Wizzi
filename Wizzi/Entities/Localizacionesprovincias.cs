using System.Collections.Generic;

namespace Wizzi.Entities
{
    public partial class Localizacionesprovincias
    {
        public Localizacionesprovincias()
        {
            Clienteslocalizaciones = new HashSet<Clienteslocalizaciones>();
            Localizacionescantones = new HashSet<Localizacionescantones>();
            Sucursales = new HashSet<Sucursales>();
        }

        public string CodigoLocalizacionProvincia { get; set; }
        public string CodigoInecLocalizacionProvincia { get; set; }
        public string NombreLocalizacionProvincia { get; set; }
        public string AbreviadoLocalizacionProvincia { get; set; }
        public string PaisesLocalizacionProvincia { get; set; }
        public string CodigoMatriculacionLocalizacionProvincia { get; set; }
        public string UsuariosLocalizacionProvincia { get; set; }

        public virtual Localizacionespaises PaisesLocalizacionProvinciaNavigation { get; set; }
        public virtual ICollection<Clienteslocalizaciones> Clienteslocalizaciones { get; set; }
        public virtual ICollection<Localizacionescantones> Localizacionescantones { get; set; }
        public virtual ICollection<Sucursales> Sucursales { get; set; }
    }
}
