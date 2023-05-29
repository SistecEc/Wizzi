export interface RegistrarAgendaDto {
    fechaInicio: Date;
    fechaFin: Date;
    titulo: string;
    esTodoElDia: boolean;
    reglaRecurrencia: string;
    fechasExluidasRecurrencia: string;
    descripcion: string;
    doctorAtiende: string;
    tipoCitaMedica: string;
    fuenteRemision: string;
}