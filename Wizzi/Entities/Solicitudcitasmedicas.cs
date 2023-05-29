using System;
using System.Collections.Generic;

namespace Wizzi.Entities
{
    public partial class Solicitudcitasmedicas
    {
        public Solicitudcitasmedicas()
        {
            Citasmedicas = new HashSet<Citasmedicas>();
        }

        public string CodigoSoliCitaMedica { get; set; }
        public int EsPacienteSoliCitaMedica { get; set; }
        public string NombreClienteSoliCitaMedica { get; set; }
        public string ApellidoClienteSoliCitaMedica { get; set; }
        public string CelularClienteSoliCitaMedica { get; set; }
        public string EmailClienteSoliCitaMedica { get; set; }
        public string GeneroClienteSoliCitaMedica { get; set; }
        public DateTime FechaNacimientoClienteSoliCitaMedica { get; set; }
        public string NombreRepresentanteSoliCitaMedica { get; set; }
        public string ApellidoRepresentanteSoliCitaMedica { get; set; }
        public int RelacionesRepresentantePacienteSoliCitaMedica { get; set; }
        public string CelularRepresentanteSoliCitaMedica { get; set; }
        public string EmailRepresentanteSoliCitaMedica { get; set; }
        public string CiudadesSoliCitaMedica { get; set; }
        public string SucursalesSoliCitaMedica { get; set; }
        public string ObservacionSoliCitaMedica { get; set; }
        public DateTime? FechaTentativaSoliCitaMedica { get; set; }
        public DateTime FechaRegistroSoliCitaMedica { get; set; }
        public string SubCampaniasOrigen { get; set; }
        public int EstadoSoliCitaMedica { get; set; }

        public virtual Localizacionescantones CiudadesSoliCitaMedicaNavigation { get; set; }
        public virtual Relacionrepresentantepaciente RelacionesRepresentantePacienteSoliCitaMedicaNavigation { get; set; }
        public virtual Subcampanias SubCampaniasOrigenNavigation { get; set; }
        public virtual Sucursales SucursalesSoliCitaMedicaNavigation { get; set; }
        public virtual ICollection<Citasmedicas> Citasmedicas { get; set; }
    }
}
