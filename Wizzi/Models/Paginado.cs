using System;
using System.Collections.Generic;

namespace Wizzi.Models
{
    public abstract class BaseResultadoPaginado
    {
        public int PaginaActual { get; set; }
        public int CantidadPaginas { get; set; }
        public int TamanioPagina { get; set; }
        public int TotalRegistros { get; set; }

        public int primerRegistroPagina
        {

            get { return (PaginaActual - 1) * TamanioPagina + 1; }
        }

        public int UltimoRegistroPagina
        {
            get { return Math.Min(PaginaActual * TamanioPagina, TotalRegistros); }
        }
    }

    public class ResultadoPaginado<T> : BaseResultadoPaginado where T : class
    {
        public IList<T> Resultados { get; set; }

        public ResultadoPaginado()
        {
            Resultados = new List<T>();
        }
    }
}
