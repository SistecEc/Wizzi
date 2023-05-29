using System;

namespace Wizzi.Dtos.Subcampanias
{
    public class SubCampaniasDto
    {
        public string codigo { get; set; }
        public string descripcion { get; set; }
        public string imagen { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFin { get; set; }
    }
}
