using System.Collections.Generic;

namespace Wizzi.Entities
{
    public partial class Tiposagendas
    {
        public Tiposagendas()
        {
            Agendas = new HashSet<Agendas>();
        }

        public int CodigoTipoAgenda { get; set; }
        public string DescripcionTipoAgenda { get; set; }

        public virtual ICollection<Agendas> Agendas { get; set; }
    }
}
