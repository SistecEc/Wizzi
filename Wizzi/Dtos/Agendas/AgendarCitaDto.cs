namespace Wizzi.Dtos.Agendas
{
    public class AgendarCitaDto
    {
        public string cliente { get; set; }
        public RegistrarAgendaDto agenda { get; set; }
        public bool esNuevoInicioProceso { get; set; }
    }

}
