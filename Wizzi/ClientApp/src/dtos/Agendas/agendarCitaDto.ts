import { RegistrarAgendaDto } from "./registrarAgendaDto";

export interface AgendarCitaDto {
    cliente: string;
    agenda: RegistrarAgendaDto;
    esNuevoInicioProceso: boolean;
}