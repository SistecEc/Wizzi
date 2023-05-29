using System.Collections.Generic;

namespace Wizzi.Entities
{
    public partial class Localizacionesparroquias
    {
        public Localizacionesparroquias()
        {
            Clienteslocalizaciones = new HashSet<Clienteslocalizaciones>();
            Sucursales = new HashSet<Sucursales>();
        }

        public string CodigoLocalizacionParroquia { get; set; }
        public string CodigoInecLocalizacionParroquia { get; set; }
        public string NombreLocalizacionParroquia { get; set; }
        public string LocalizacionesCantonesLocalizacionParroquia { get; set; }
        public string UsuariosLocalizacionParroquia { get; set; }

        public virtual Localizacionescantones LocalizacionesCantonesLocalizacionParroquiaNavigation { get; set; }
        public virtual ICollection<Clienteslocalizaciones> Clienteslocalizaciones { get; set; }
        public virtual ICollection<Sucursales> Sucursales { get; set; }
    }
}
