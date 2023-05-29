export interface RegistrarClienteDto {
    codigo: string;
    tipoIdentificacion: string;
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
    localizacion: LocalizacionSimpleDto;
}

export interface LocalizacionSimpleDto {
    pais: string;
    provincia: string;
    canton: string;
    parroquia: string;
}