using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wizzi.Dtos.Reportes
{
    public class RepAgendamientoAtencion
    {
        public string id { get; set; }
        public string fecha { get; set; }
        public string nombre { get; set; }
        public string campania { get; set; }
        public string centro { get; set; }
        public string agendado { get; set; }
        public string atendido { get; set; }
        public string candidato { get; set; }
        public string vendido { get; set; }
        public string empleado { get; set; }
        public string sucursal { get; set; }

    }
}
