import { TipoMovimientoCita } from "../../enums";

export interface VerMovimientosDto {
    codigo: string;
    titulo: string;
    descripcion: string;
    fechaInicio: Date;
    fechaFin: Date;
    fechaRegistro: Date;
    nombreEmpleadoAsignado: string;
    apellidoEmpleadoAsignado: string;
    usuarioAsigna: string;
    nombreEmpleadoAsigna: string;
    apellidoEmpleadoAsigna: string;
    fechaUltimaModificacion: Date;
    tipoMovimiento: TipoMovimientoCita;
    estado: number;
}