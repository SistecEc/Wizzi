using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Wizzi.Entities;
using Wizzi.Enums;
using Wizzi.Extensions;
using Wizzi.Helpers;
using Wizzi.Interfaces;
using Wizzi.Models;

namespace Wizzi.Services
{
    public class ClienteService : IClienteService
    {
        private DataContext _context;

        public ClienteService(DataContext context)
        {
            _context = context;
        }

        public List<Clientes> Buscar(string query)
        {
            string[] palabrasBuscar = HttpUtility.UrlDecode(query).Split(" ");

            Expression<Func<Clientes, bool>> condicionTipoIdentificacionCliente = c => c.TiposIdentificacionCliente != ((int)TipoIdentificacion.POTENCIAL).ToString();

            Expression<Func<Clientes, bool>> condicionNumeroIdentificacionCliente = null;
            foreach (var palabra in palabrasBuscar)
            {
                if (palabra.Length > 3)
                {
                    Expression<Func<Clientes, bool>> e1 = c => c.NumeroIdentificacionCliente.Contains(palabra, StringComparison.CurrentCultureIgnoreCase);
                    condicionNumeroIdentificacionCliente = condicionNumeroIdentificacionCliente == null ? e1 : condicionNumeroIdentificacionCliente.And(e1);
                }
            }

            Expression<Func<Clientes, bool>> condicionNombreCliente = null;
            foreach (var palabra in palabrasBuscar)
            {
                if (palabra.Length > 3)
                {
                    Expression<Func<Clientes, bool>> e1 = c => c.NombreCliente.Contains(palabra, StringComparison.CurrentCultureIgnoreCase);
                    condicionNombreCliente = condicionNombreCliente == null ? e1 : condicionNombreCliente.And(e1);
                }
            }

            Expression<Func<Clientes, bool>> condicionApellidoCliente = null;
            foreach (var palabra in palabrasBuscar)
            {
                if (palabra.Length > 3)
                {
                    Expression<Func<Clientes, bool>> e1 = c => c.ApellidoCliente.Contains(palabra, StringComparison.CurrentCultureIgnoreCase);
                    condicionApellidoCliente = condicionApellidoCliente == null ? e1 : condicionApellidoCliente.And(e1);
                }
            }

            Expression<Func<Clientes, bool>> condicionNombreComercialCliente = null;
            foreach (var palabra in palabrasBuscar)
            {
                if (palabra.Length > 3)
                {
                    Expression<Func<Clientes, bool>> e1 = c => c.NombreComercialCliente.Contains(palabra, StringComparison.CurrentCultureIgnoreCase);
                    condicionNombreComercialCliente = condicionNombreComercialCliente == null ? e1 : condicionNombreComercialCliente.And(e1);
                }
            }

            Expression<Func<Clientes, bool>> condicionNombreCompleto = null;
            foreach (var palabra in palabrasBuscar)
            {
                if (palabra.Length > 3)
                {
                    Expression<Func<Clientes, bool>> e1 = c => (c.NombreCliente + " " + c.ApellidoCliente).Contains(palabra, StringComparison.CurrentCultureIgnoreCase);
                    condicionNombreCompleto = condicionNombreCompleto == null ? e1 : condicionNombreCompleto.And(e1);
                }
            }

            List<Clientes> clientes = _context.Clientes
                                                .Include(c => c.TiposIdentificacionClienteNavigation)
                                                .Include(c => c.Clienteslocalizaciones)
                                                    .ThenInclude(ci => ci.PaisesClienteLocalizacionNavigation)
                                                .Include(c => c.Clienteslocalizaciones)
                                                    .ThenInclude(ci => ci.ProvinciasClienteLocalizacionNavigation)
                                                .Include(c => c.Clienteslocalizaciones)
                                                    .ThenInclude(ci => ci.CantonesClienteLocalizacionNavigation)
                                                .Include(c => c.Clienteslocalizaciones)
                                                    .ThenInclude(ci => ci.ParroquiasClienteLocalizacionNavigation)
                                                .Where(condicionTipoIdentificacionCliente
                                                        .And(condicionNumeroIdentificacionCliente
                                                        .Or(condicionNombreCliente)
                                                        .Or(condicionNombreCliente)
                                                        .Or(condicionApellidoCliente)
                                                        .Or(condicionNombreComercialCliente)
                                                        .Or(condicionNombreCompleto)))
                                                .ToList();
            return clientes;
        }

        public ResultadoPaginado<Clientes> BuscarPaginado(string query, int p = 1, int tp = 10)
        {
            throw new NotImplementedException();
        }
    }
}
