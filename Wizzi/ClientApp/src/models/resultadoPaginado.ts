export interface BaseResultadoPaginado {
    paginaActual: number;
    cantidadPaginas: number;
    tamanioPagina: number;
    totalRegistros: number;
    primerRegistroPagina: number;
    ultimoRegistroPagina: number;
}

export interface ResultadoPaginado<T> extends BaseResultadoPaginado {
    resultados: T[];
}