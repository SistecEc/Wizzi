import { VerPaisDto, VerProvinciaDto, VerCantonDto, VerParroquiaDto } from "../Localizaciones";

export interface LocalizacionDto {
    pais: VerPaisDto;
    provincia: VerProvinciaDto;
    canton: VerCantonDto;
    parroquia: VerParroquiaDto;
}