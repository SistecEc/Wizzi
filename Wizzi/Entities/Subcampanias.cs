using System;
using System.Collections.Generic;

namespace Wizzi.Entities
{
    public partial class Subcampanias
    {
        public Subcampanias()
        {
            Citasmedicas = new HashSet<Citasmedicas>();
            Movimientocampanias = new HashSet<Movimientocampanias>();
            Solicitudcitasmedicas = new HashSet<Solicitudcitasmedicas>();
        }

        public string CodigoSubCampania { get; set; }
        public string CampaniasSubCampania { get; set; }
        public string DescripcionSubCampania { get; set; }
        public string ImagenSubCampania { get; set; }
        public DateTime FechaInicioSubCampania { get; set; }
        public DateTime FechaFinSubCampania { get; set; }

        public virtual Campanias CampaniasSubCampaniaNavigation { get; set; }
        public virtual ICollection<Citasmedicas> Citasmedicas { get; set; }
        public virtual ICollection<Movimientocampanias> Movimientocampanias { get; set; }
        public virtual ICollection<Solicitudcitasmedicas> Solicitudcitasmedicas { get; set; }
    }
}
