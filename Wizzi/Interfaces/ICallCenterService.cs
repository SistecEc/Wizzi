using Wizzi.Dtos.Llamadas;
using Wizzi.Entities;

namespace Wizzi.Interfaces
{
    public interface ICallCenterService
    {
        void grabarCallCenterCitaMedica(Citasmedicas agenda, string codigoDocumentoOrigeInstalacion, bool esNuevoInicioProceso = false, int numeroIteracion = 0);
        void reasignarCallCenterCitaMedica(Agendas agendaGrabada, Citasmedicas citaMedicaNueva);
        void grabarLlamadaSolicitudCita(Solicitudcitasmedicas solicitud, RegistrarLlamadaDto infoLlamada, bool forzarNuevaOrden = false);
        void grabarLlamadaCita(Citasmedicas solicitud, RegistrarLlamadaDto infoLlamada);
        void grabarReagendamiento(Agendas agendaGrabada, Ordeninstalacion ordenGrabada, RegistrarLlamadaDto infoLlamada);
    }
}
