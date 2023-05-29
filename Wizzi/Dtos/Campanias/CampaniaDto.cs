using System;
using System.Collections.Generic;

namespace Wizzi.Dtos.Campanias
{
    public class CampaniaDto
    {
        public string codigo { get; set; }
        public string titulo { get; set; }
        public string descripcion { get; set; }
        public decimal presupuesto { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFin { get; set; }
        public int cantidadSubCampanias { get; set; }
        public IList<SubCampaniasDto> PrimerasSubcampanias { get; set; }
    }

    public class SubCampaniasDto
    {
        public string codigo { get; set; }
        public string descripcion { get; set; }
        public string imagen { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFin { get; set; }
    }
}
