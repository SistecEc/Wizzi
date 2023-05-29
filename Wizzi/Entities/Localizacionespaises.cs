using System.Collections.Generic;

namespace Wizzi.Entities
{
    public partial class Localizacionespaises
    {
        public Localizacionespaises()
        {
            Clienteslocalizaciones = new HashSet<Clienteslocalizaciones>();
            Localizacionesprovincias = new HashSet<Localizacionesprovincias>();
            Sucursales = new HashSet<Sucursales>();
        }

        public string CodigoLocalizacionPais { get; set; }
        public string CodigoAccesoLocalizacionPais { get; set; }
        public string NombreLocalizacionPais { get; set; }
        public string AbreviadoLocalizacionPais { get; set; }
        public string UsuariosLocalizacionPaises { get; set; }

        public virtual ICollection<Clienteslocalizaciones> Clienteslocalizaciones { get; set; }
        public virtual ICollection<Localizacionesprovincias> Localizacionesprovincias { get; set; }
        public virtual ICollection<Sucursales> Sucursales { get; set; }
    }
}
