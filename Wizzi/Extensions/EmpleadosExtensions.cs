using System.Collections.Generic;
using System.Linq;
using Wizzi.Entities;

namespace Wizzi.Extensions
{
    public static class EmpleadosExtensions
    {
        public static IEnumerable<Empleados> WithoutPasswords(this IEnumerable<Empleados> users)
        {
            return users.Select(x => x.WithoutPassword());
        }

        public static Empleados WithoutPassword(this Empleados user)
        {
            user.ClaveUsuarioEmpleado = null;
            return user;
        }

    }
}
