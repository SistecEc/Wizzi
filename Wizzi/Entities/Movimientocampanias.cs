using System;

namespace Wizzi.Entities
{
    public partial class Movimientocampanias
    {
        public int CodigoMovCamp { get; set; }
        public string SesionMovCamp { get; set; }
        public string SubCampaniasMovCamp { get; set; }
        public int IngresoDesdePublicidadMovCamp { get; set; }
        public int RegistroCitaMovCamp { get; set; }
        public DateTime FechaRegistroMovCamp { get; set; }

        public virtual Subcampanias SubCampaniasMovCampNavigation { get; set; }
    }
}
