export interface CampaniaDto {
    codigo: string;
    titulo: string;
    descripcion: string;
    presupuesto: number;
    fechaInicio: Date;
    fechaFin: Date;
    cantidadSubCampanias: number;
    primerasSubcampanias: SubCampaniasDto[];
}

export interface SubCampaniasDto {
    codigo: string;
    descripcion: string;
    imagen: string;
    fechaInicio: Date;
    fechaFin: Date;
}