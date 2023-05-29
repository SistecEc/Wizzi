using System.Collections.Generic;
using Wizzi.Entities;

namespace Wizzi.Interfaces
{
    public interface IUserService
    {
        Empleados Authenticate(string username, string password);
        IEnumerable<Empleados> GetAll();
        Empleados GetById(string id);
        Empleados GetByIdUntracked(string id);
        Empleados Create(Empleados user, string password);
        void Update(Empleados user, string password = null);
        void Delete(string id);
    }
}
