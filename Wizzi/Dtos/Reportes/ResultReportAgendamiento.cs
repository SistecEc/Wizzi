using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wizzi.Dtos.Reportes
{
    public class ResultReportAgendamiento
    {
        public int cantidadLEAD  { get; set; }
        public List<RepAgendamientoAtencion> data { get; set; }

    }
}
