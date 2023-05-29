import { VerSucursalDto } from "../Sucursales";
import { VerAgendaDto } from "./verAgendaDto";

export interface VerAgendaCitaMedicaDto extends VerAgendaDto {
    cita: CitaMedicaAgendaDto;
    sucursal: VerSucursalDto;
}

export interface CitaMedicaAgendaDto {
    codigo: string;
    tipoCitaMedica: string;
    fuenteRemision: string;
    solicitud: string;
    cliente: ClienteCitaMedicaAgendaDto;
}

export interface ClienteCitaMedicaAgendaDto {
    codigo: string;
    identificacion: string;
    nombreComercial: string;
    prioridadNombreComercial: boolean;
    nombre: string;
    apellido: string;
    direccion: string;
    telefono: string;
    email: string;
    genero: string;
}