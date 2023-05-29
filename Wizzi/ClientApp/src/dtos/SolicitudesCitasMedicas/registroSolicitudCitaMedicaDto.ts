import { _BaseDto } from "./_BaseDto";

export interface RegistroSolicitudCitaMedicaDto extends _BaseDto {
    relacionRepresentantePaciente: number;
    ciudad: string;
    sucursal: string;
    subCampaniaOrigen: string;
}