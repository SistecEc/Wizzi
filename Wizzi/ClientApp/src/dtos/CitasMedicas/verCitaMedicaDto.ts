import { VerAgendaDto } from "../Agendas";
import { VerFuentesRemision } from "../FuentesRemision";

export interface VerCitaMedicaDto {
    codigo: string;
    cliente: VerClienteDto;
    diagnostico: string;
    pacienteLlego: number;
    codigoSolicitudCitaMedica: string;
    tipoCita: string;
    activa: boolean;
    codigoSubCampaniaOrigen: string;
    grupoCita: string;
    fechaRegistro: Date;
    cantidadReagendados: number;
    agenda: VerAgendaDto;
    fuenteRemision: VerFuentesRemision;
}

export interface VerClienteDto {
    codigo: string;
    numeroIdentificacion: string;
    nombreComercial: string;
    prioridadNombreComercial: boolean;
    nombre: string;
    apellido: string;
    direccion: string;
    telefono: string;
    mail: string;
    sexo: string;
}