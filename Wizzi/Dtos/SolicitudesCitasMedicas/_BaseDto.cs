
using System;

namespace Wizzi.Dtos.SolicitudesCitasMedicas
{
    public abstract class _BaseDto
    {
        public string codigo { get; set; }
        public bool esPaciente { get; set; }
        public string nombrePaciente { get; set; }
        public string apellidoPaciente { get; set; }
        public string telefonoPaciente { get; set; }
        public string emailPaciente { get; set; }
        public string generoPaciente { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public string nombreRepresentante { get; set; }
        public string apellidoRepresentante { get; set; }
        public string telefonoRepresentante { get; set; }
        public string emailRepresentante { get; set; }
        public DateTime fechaTentativa { get; set; }
        public string observacion { get; set; }
    }

}
