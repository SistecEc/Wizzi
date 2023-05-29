using System;

namespace Wizzi.Dtos.Agendas
{
    public class RegistrarAgendaDto
    {
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFin { get; set; }
        public string titulo { get; set; }
        public bool esTodoElDia { get; set; }
        public string reglaRecurrencia { get; set; }
        public string fechasExluidasRecurrencia { get; set; }
        public string descripcion { get; set; }
        public string doctorAtiende { get; set; }
        public string tipoCitaMedica { get; set; }
        public string fuenteRemision { get; set; }
    }

}
