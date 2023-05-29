using System.Collections.Generic;
using Wizzi.Entities;
using Wizzi.Models;

namespace Wizzi.Interfaces
{
    public interface IClienteService
    {
        List<Clientes> Buscar(string query);
        ResultadoPaginado<Clientes> BuscarPaginado(string query, int p = 1, int tp = 10);
    }
}
