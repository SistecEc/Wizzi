import { VerCitaMedicaDto } from "../CitasMedicas";
import { VerPaisDto, VerProvinciaDto, VerCantonDto, VerParroquiaDto } from "../Localizaciones";
import { VerRelacionRepresentantePaciente } from "../RelacionesRepresentantes";
import { SubCampaniasDto } from "../Subcampanias";
import { VerSucursalDto } from "../Sucursales";
import { _BaseDto } from "./_BaseDto";

export interface VerSolicitudCitaMedicaDto extends _BaseDto {
    subcampaniaOrigen: SubCampaniasDto;
    cita: VerCitaMedicaDto;
    localizacion: LocalizacionDto;
    relacionRepresentantePaciente: VerRelacionRepresentantePaciente;
    sucursal: VerSucursalDto;
    fechaRegistro: Date;
    cantidadAgendas: number;
    cantidadLlamadasUltimoProceso: number;
    cantidadMovimientos: number;
}

export interface LocalizacionDto {
    pais: VerPaisDto;
    provincia: VerProvinciaDto;
    canton: VerCantonDto;
    parroquia: VerParroquiaDto;
}