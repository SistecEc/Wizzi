using System.Collections.Generic;

namespace Wizzi.Entities
{
    public partial class Relacionrepresentantepaciente
    {
        public Relacionrepresentantepaciente()
        {
            Solicitudcitasmedicas = new HashSet<Solicitudcitasmedicas>();
        }

        public int CodigoRelacionRepresentantePaciente { get; set; }
        public string DescripcionRelacionRepresentantePaciente { get; set; }
        public string UsuariosRelacionRepresentantePaciente { get; set; }

        public virtual ICollection<Solicitudcitasmedicas> Solicitudcitasmedicas { get; set; }
    }
}
