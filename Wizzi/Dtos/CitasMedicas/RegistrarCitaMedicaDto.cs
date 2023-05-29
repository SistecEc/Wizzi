
namespace Wizzi.Dtos.CitasMedicas
{
    public class RegistrarCitaMedicaDto
    {
        public string codigo { get; set; }
        public string codigoCliente { get; set; }
        public string diagnostico { get; set; }
        public bool pacienteLlego { get; set; }
        public string codigoSolicitudCitaMedica { get; set; }
        public string tipoCita { get; set; }
        public bool activa { get; set; }
        public string codigoSubCampaniaOrigen { get; set; }
        public string codigoAgenda { get; set; }
    }
}
