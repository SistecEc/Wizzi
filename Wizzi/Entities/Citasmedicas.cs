using System;

namespace Wizzi.Entities
{
    public partial class Citasmedicas
    {
        public string CodigoCitaMedica { get; set; }
        public string ClientesCitaMedica { get; set; }
        public string DiagnosticoCitaMedica { get; set; }
        public int PacienteLlegoCitaMedica { get; set; }
        public string SolicitudesCitaMedica { get; set; }
        public string TipoCitaMedica { get; set; }
        public int ActivaCitaMedica { get; set; }
        public string SubCampaniasOrigen { get; set; }
        public string CodigoGrupoCitaMedica { get; set; }
        public DateTime FechaRegistroCitaMedica { get; set; }
        public string AgendasCitaMedica { get; set; }
        public string FuentesRemisionCitaMedica { get; set; }

        public virtual Agendas AgendasCitaMedicaNavigation { get; set; }
        public virtual Clientes ClientesCitaMedicaNavigation { get; set; }
        public virtual Fuentesremision FuentesRemisionCitaMedicaNavigation { get; set; }
        public virtual Solicitudcitasmedicas SolicitudesCitaMedicaNavigation { get; set; }
        public virtual Subcampanias SubCampaniasOrigenNavigation { get; set; }
        public virtual Categoriastiposdocumentosinstalaciones TipoCitaMedicaNavigation { get; set; }
    }
}
