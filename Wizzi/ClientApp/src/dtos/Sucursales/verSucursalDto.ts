import { VerPaisDto, VerProvinciaDto, VerCantonDto, VerParroquiaDto } from "../Localizaciones";

export interface VerSucursalDto {
    codigo: string;
    nombre: string;
    direccion: string;
    esMatriz: boolean;
    localizacion: Localizacion;
}

export interface Localizacion {
    pais: VerPaisDto;
    provincia: VerProvinciaDto;
    canton: VerCantonDto;
    parroquia: VerParroquiaDto;
}