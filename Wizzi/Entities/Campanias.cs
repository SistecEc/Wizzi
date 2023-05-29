using System;
using System.Collections.Generic;

namespace Wizzi.Entities
{
    public partial class Campanias
    {
        public Campanias()
        {
            Subcampanias = new HashSet<Subcampanias>();
        }

        public string CodigoCampania { get; set; }
        public string TituloCampania { get; set; }
        public string DescripcionCampania { get; set; }
        public decimal PresupuestoCampania { get; set; }
        public DateTime FechaInicioCampania { get; set; }
        public DateTime FechaFinCampania { get; set; }

        public virtual ICollection<Subcampanias> Subcampanias { get; set; }
    }
}
