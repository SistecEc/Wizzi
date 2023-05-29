using System;

namespace Wizzi.Entities
{
    public partial class Docspendientes
    {
        public string DocsCabeceraDocPendiente { get; set; }
        public string CodigoDocPendiente { get; set; }
        public string TiposTramitesDocPendiente { get; set; }
        public string PerfilesDocPendiente { get; set; }
        public string PerfilesMailBccdocPendiente { get; set; }
        public DateTime CreacionDocPendiente { get; set; }
        public DateTime AtencionDocPendiente { get; set; }
        public string EstadoDocPendiente { get; set; }
        public string TipoDocumentoDocPendiente { get; set; }
    }
}
