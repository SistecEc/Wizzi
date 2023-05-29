using Wizzi.Dtos.Sucursales;

namespace Wizzi.Dtos.Agendas
{
    public class VerAgendaCitaMedicaDto : VerAgendaDto
    {
        public CitaMedicaAgendaDto cita { get; set; }
        public VerSucursalDto sucursal { get; set; }
    }

    public class CitaMedicaAgendaDto
    {
        public string Codigo { get; set; }
        public string tipoCitaMedica { get; set; }
        public string fuenteRemision { get; set; }
        public string solicitud { get; set; }
        public ClienteCitaMedicaAgendaDto cliente { get; set; }
    }

    public class ClienteCitaMedicaAgendaDto
    {
        public string Codigo { get; set; }
        public string Identificacion { get; set; }
        public string NombreComercial { get; set; }
        public bool PrioridadNombreComercial { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Genero { get; set; }
    }

}
