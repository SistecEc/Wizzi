using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wizzi.Models;

namespace Wizzi.Extensions
{
    public static class IqueryableExtensions
    {
        public static ResultadoPaginado<T> GetPaged<T>(this IQueryable<T> query, int pagina, int tamanioPagina) where T : class
        {
            var result = new ResultadoPaginado<T>
            {
                PaginaActual = pagina,
                TamanioPagina = tamanioPagina,
                TotalRegistros = query.Count()
            };

            var pageCount = (double)result.TotalRegistros / tamanioPagina;
            result.CantidadPaginas = (int)Math.Ceiling(pageCount);

            var skip = (pagina - 1) * tamanioPagina;
            result.Resultados = query.Skip(skip)
                                  .Take(tamanioPagina)
                                  .ToList();

            return result;
        }

        public static async Task<ResultadoPaginado<T>> GetPagedAsync<T>(this IQueryable<T> query, int pagina, int tamanioPagina) where T : class
        {
            var result = new ResultadoPaginado<T>
            {
                PaginaActual = pagina,
                TamanioPagina = tamanioPagina,
                TotalRegistros = await query.CountAsync()
            };

            var pageCount = (double)result.TotalRegistros / tamanioPagina;
            result.CantidadPaginas = (int)Math.Ceiling(pageCount);

            var skip = (pagina - 1) * tamanioPagina;
            result.Resultados = await query.Skip(skip)
                                        .Take(tamanioPagina)
                                        .ToListAsync();

            return result;
        }

        public static ResultadoPaginado<U> GetPaged<T, U>(this IQueryable<T> query, int pagina, int tamanioPagina, IMapper mapper) where U : class
        {
            var result = new ResultadoPaginado<U>();
            result.PaginaActual = pagina;
            result.TamanioPagina = tamanioPagina;
            result.TotalRegistros = query.Count();

            var pageCount = (double)result.TotalRegistros / tamanioPagina;
            result.CantidadPaginas = (int)Math.Ceiling(pageCount);

            var skip = (pagina - 1) * tamanioPagina;
            result.Resultados = query.Skip(skip)
                                  .Take(tamanioPagina)
                                  .ProjectTo<U>(mapper.ConfigurationProvider)
                                  .ToList();
            return result;
        }

        public static ResultadoPaginado<U> GetPaged<T, U>(this IQueryable<T> query, int pagina, int tamanioPagina, IMapper mapper, Func<T, U> converitdorXregistro) where U : class
        {
            var result = new ResultadoPaginado<U>();
            result.PaginaActual = pagina;
            result.TamanioPagina = tamanioPagina;
            result.TotalRegistros = query.Count();

            var pageCount = (double)result.TotalRegistros / tamanioPagina;
            result.CantidadPaginas = (int)Math.Ceiling(pageCount);

            var skip = (pagina - 1) * tamanioPagina;

            IList<T> lresultados = query.Skip(skip)
                                  .Take(tamanioPagina)
                                  .ToList();

            lresultados.ForEach(r => result.Resultados.Add(converitdorXregistro(r)));
            //foreach (T item in lresultados)
            //{
            //    U itemConvertido = converitdorXregistro(item);
            //    result.Resultados.Add(itemConvertido);
            //}

            //result.Resultados = query.Skip(skip)
            //                      .Take(tamanioPagina)
            //                      .Select(r => converitdorXregistro(r))
            //                      .ToList();
            return result;
        }

        public static async Task<ResultadoPaginado<U>> GetPagedAsync<T, U>(this IQueryable<T> query, int pagina, int tamanioPagina, IMapper mapper) where U : class
        {
            var result = new ResultadoPaginado<U>();
            result.PaginaActual = pagina;
            result.TamanioPagina = tamanioPagina;
            result.TotalRegistros = await query.CountAsync();

            var pageCount = (double)result.TotalRegistros / tamanioPagina;
            result.CantidadPaginas = (int)Math.Ceiling(pageCount);

            var skip = (pagina - 1) * tamanioPagina;
            result.Resultados = await query.Skip(skip)
                                        .Take(tamanioPagina)
                                        .ProjectTo<U>(mapper.ConfigurationProvider)
                                        .ToListAsync();
            return result;
        }

    }
}
