using System;
using Wizzi.Dtos.CitasMedicas;
using Wizzi.Dtos.Localizaciones;
using Wizzi.Dtos.RelacionesRepresentantes;
using Wizzi.Dtos.Subcampanias;
using Wizzi.Dtos.Sucursales;

namespace Wizzi.Dtos.SolicitudesCitasMedicas
{
    public class VerSolicitudCitaMedicaDto : _BaseDto
    {
        public SubCampaniasDto subcampaniaOrigen { get; set; }
        public VerCitaMedicaDto cita { get; set; }
        public LocalizacionDto localizacion { get; set; }
        public VerRelacionRepresentantePaciente relacionRepresentantePaciente { get; set; }
        public VerSucursalDto sucursal { get; set; }
        public DateTime fechaRegistro { get; set; }
        public int cantidadAgendas { get; set; }
        public int cantidadLlamadasUltimoProceso { get; set; }
        public int cantidadMovimientos { get; set; }
    }

    public class LocalizacionDto
    {
        public VerPaisDto Pais { get; set; }
        public VerProvinciaDto Provincia { get; set; }
        public VerCantonDto Canton { get; set; }
        public VerParroquiaDto Parroquia { get; set; }
    }

    //public class VerSucursalDto
    //{
    //    public string Codigo { get; set; }
    //    public string Nombre { get; set; }
    //}

}
