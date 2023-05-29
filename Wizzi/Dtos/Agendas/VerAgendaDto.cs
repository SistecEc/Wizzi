using System;

namespace Wizzi.Dtos.Agendas
{
    public class VerAgendaDto
    {
        public string Codigo { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public bool TodoElDia { get; set; }
        public string ReglaRecurrencia { get; set; }
        public string FechasExcluidasRecurrencia { get; set; }
        public VerEmpleadoDto Empleado { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime fechaUltimaModificacion { get; set; }
        public VerTipoAgendaDto TipoAgenda { get; set; }
        public int Estado { get; set; }
        public int cantidadMovimientos { get; set; }
        public int cantidadLlamadasUltimoProceso { get; set; }
    }

    public class VerEmpleadoDto
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
    }

    public class VerTipoAgendaDto
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
    }
}
