import { tipoIdentificacionDto, LocalizacionDto } from ".";

export interface VerClienteDto {
    codigo: string;
    tipoIdentificacion: tipoIdentificacionDto;
    identificacion: string;
    nombreComercial: string;
    prioridadNombreComercial: boolean;
    nombre: string;
    apellido: string;
    direccion: string;
    telefono: string;
    email: string;
    genero: string;
    fechaNacimiento: Date;
    localizacion: LocalizacionDto;
}