using Microsoft.AspNetCore.Http;
using System;

namespace Wizzi.Dtos.Subcampanias
{
    public class ManejoSubCampaniasDto
    {
        public string codigo { get; set; }
        public string codigoCampania { get; set; }
        public string descripcion { get; set; }
        public IFormFile imagen { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFin { get; set; }
    }
}
