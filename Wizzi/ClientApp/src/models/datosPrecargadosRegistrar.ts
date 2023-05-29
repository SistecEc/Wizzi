import { LocalizacionSimpleDto } from "../dtos/Clientes";

export interface DatosPrecargadosRegistrar {
    nombre: string,
    apellido: string,
    telefono: string,
    email: string,
    genero: string,
    fechaNacimiento: Date,
    localizacion: LocalizacionSimpleDto,
};