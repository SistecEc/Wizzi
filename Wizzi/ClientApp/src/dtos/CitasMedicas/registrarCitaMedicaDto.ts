export interface RegistrarCitaMedicaDto {
    codigo: string;
    codigoCliente: string;
    diagnostico: string;
    pacienteLlego: boolean;
    codigoSolicitudCitaMedica: string;
    tipoCita: string;
    activa: boolean;
    codigoSubCampaniaOrigen: string;
    codigoAgenda: string;
}