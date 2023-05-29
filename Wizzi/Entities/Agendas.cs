using System;

namespace Wizzi.Entities
{
    public partial class Agendas
    {
        public string CodigoAgenda { get; set; }
        public string EmpleadosAgenda { get; set; }
        public string TituloAgenda { get; set; }
        public string DescripcionAgenda { get; set; }
        public DateTime FechaInicioAgenda { get; set; }
        public DateTime FechaFinAgenda { get; set; }
        public DateTime FechaRegistroAgenda { get; set; }
        public DateTime FechaUltimaModificacionAgenda { get; set; }
        public int TiposAgendasAgenda { get; set; }
        public int EstadoAgenda { get; set; }
        public int EsTodoElDiaAgenda { get; set; }
        public string ReglaRecurrenciaAgenda { get; set; }
        public string FechasExluidasRecurrencia { get; set; }

        public virtual Empleados EmpleadosAgendaNavigation { get; set; }
        public virtual Tiposagendas TiposAgendasAgendaNavigation { get; set; }
        public virtual Citasmedicas Citasmedicas { get; set; }
    }
}
