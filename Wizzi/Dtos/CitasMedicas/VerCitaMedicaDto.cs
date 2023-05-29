using Wizzi.Dtos.Agendas;
using Wizzi.Dtos.FuentesRemision;

namespace Wizzi.Dtos.CitasMedicas
{
    public class VerCitaMedicaDto
    {
        public string codigo { get; set; }
        public VerClienteDto cliente { get; set; }
        public string diagnostico { get; set; }
        public int pacienteLlego { get; set; }
        public string codigoSolicitudCitaMedica { get; set; }
        public string tipoCita { get; set; }
        public bool activa { get; set; }
        public string codigoSubCampaniaOrigen { get; set; }
        public string grupoCita { get; set; }
        public string fechaRegistro { get; set; }
        public int cantidadReagendados { get; set; }
        public VerAgendaDto agenda { get; set; }
        public VerFuentesRemision fuenteRemision { get; set; }
    }

    public class VerClienteDto
    {
        public string Codigo { get; set; }
        public string NumeroIdentificacion { get; set; }
        public string NombreComercial { get; set; }
        public bool PrioridadNombreComercial { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Mail { get; set; }
        public string Sexo { get; set; }
    }
}
