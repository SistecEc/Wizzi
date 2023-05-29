namespace Wizzi.Dtos.SolicitudesCitasMedicas
{
    public class RegistroSolicitudCitaMedicaDto : _BaseDto
    {
        public int relacionRepresentantePaciente { get; set; }
        public string ciudad { get; set; }
        public string sucursal { get; set; }
        public string subCampaniaOrigen { get; set; }
    }
}
