import { ClienteCitaMedicaAgendaDto } from "../dtos/Agendas";
import { ModeloAgenda } from "./modeloAgenda";

export interface ModeloAgendaGrabada extends ModeloAgenda {
    codigo: string,
    tipoAgenda: string,
    cliente: ClienteCitaMedicaAgendaDto,
};