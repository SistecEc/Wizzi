using System.Collections.Generic;

namespace Wizzi.Entities
{
    public partial class Fuentesremision
    {
        public Fuentesremision()
        {
            Citasmedicas = new HashSet<Citasmedicas>();
        }

        public string CodigoFuenteRemision { get; set; }
        public string DescripcionFuenteRemision { get; set; }
        public string UsuariosFuenteRemision { get; set; }

        public virtual ICollection<Citasmedicas> Citasmedicas { get; set; }
    }
}
