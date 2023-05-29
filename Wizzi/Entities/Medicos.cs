using System.Collections.Generic;

namespace Wizzi.Entities
{
    public partial class Medicos
    {
        public Medicos()
        {
            Agendas = new HashSet<Agendas>();
            Usuariosmedicos = new HashSet<Usuariosmedicos>();
        }

        public string CodigoMedico { get; set; }
        public string NombreMedico { get; set; }
        public string ApellidoMedico { get; set; }
        public string DireccionMedico { get; set; }
        public string TelefonoMedico { get; set; }
        public string EspecialidadesMedico { get; set; }
        public string HospitalesMedico { get; set; }
        public string CiudadMedico { get; set; }
        public string TelefonoCelularMedico { get; set; }
        public string CorreoElectronicoMedico { get; set; }

        public virtual ICollection<Agendas> Agendas { get; set; }
        public virtual ICollection<Usuariosmedicos> Usuariosmedicos { get; set; }
    }
}
