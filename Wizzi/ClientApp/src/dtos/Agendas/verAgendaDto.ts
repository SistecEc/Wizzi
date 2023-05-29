export interface VerAgendaDto {
    codigo: string;
    titulo: string;
    descripcion: string;
    fechaInicio: Date;
    fechaFin: Date;
    todoElDia: boolean;
    reglaRecurrencia: string;
    fechasExcluidasRecurrencia: string;
    empleado: VerEmpleadoDto;
    fechaRegistro: Date;
    fechaUltimaModificacion: Date;
    tipoAgenda: VerTipoAgendaDto;
    estado: number;
    cantidadMovimientos: number;
    cantidadLlamadasUltimoProceso: number;
}

export interface VerEmpleadoDto {
    codigo: string;
    nombre: string;
    apellido: string;
}

export interface VerTipoAgendaDto {
    codigo: string;
    descripcion: string;
}