using System.Collections.Generic;

namespace Wizzi.Entities
{
    public partial class Localizacionescantones
    {
        public Localizacionescantones()
        {
            Clienteslocalizaciones = new HashSet<Clienteslocalizaciones>();
            Localizacionesparroquias = new HashSet<Localizacionesparroquias>();
            Solicitudcitasmedicas = new HashSet<Solicitudcitasmedicas>();
            Sucursales = new HashSet<Sucursales>();
        }

        public string CodigoLocalizacionCanton { get; set; }
        public string CodigoInecLocalizacionCanton { get; set; }
        public string NombreLocalizacionCanton { get; set; }
        public string ProvinciasLocalizacionCanton { get; set; }
        public string AbreviadoLocalizacionCanton { get; set; }
        public string UsuariosLocalizacionCanton { get; set; }

        public virtual Localizacionesprovincias ProvinciasLocalizacionCantonNavigation { get; set; }
        public virtual ICollection<Clienteslocalizaciones> Clienteslocalizaciones { get; set; }
        public virtual ICollection<Localizacionesparroquias> Localizacionesparroquias { get; set; }
        public virtual ICollection<Solicitudcitasmedicas> Solicitudcitasmedicas { get; set; }
        public virtual ICollection<Sucursales> Sucursales { get; set; }
    }
}
