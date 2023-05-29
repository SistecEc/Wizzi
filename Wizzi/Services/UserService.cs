using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wizzi.Entities;
using Wizzi.Extensions;
using Wizzi.Helpers;
using Wizzi.Interfaces;

namespace Wizzi.Services
{
    public class UserService : IUserService
    {
        private DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;
        }

        public Empleados Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = _context.Empleados.SingleOrDefault(x => x.NombreUsuarioEmpleado == username);

            // check if username exists
            if (user == null)
                return null;

            // check if password is correct

            //  Usar este método cuando se actualice la forma de almacenar las contrasenias de texto plano a uso de hash
            //if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            //    return null;
            if (Encoding.ASCII.GetString(user.ClaveUsuarioEmpleado) != password)
                return null;

            // authentication successful
            return user.WithoutPassword();
        }

        public IEnumerable<Empleados> GetAll()
        {
            return _context.Empleados.WithoutPasswords();
        }

        public Empleados GetById(string id)
        {
            return _context.Empleados.Find(id).WithoutPassword();
        }

        public Empleados GetByIdUntracked(string id)
        {
            return _context.Empleados
                    .AsNoTracking()
                    .Where(e => e.CodigoEmpleado == id)
                    .FirstOrDefault()
                    .WithoutPassword();
        }

        public Empleados Create(Empleados user, string password)
        {
            // validation
            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Password is required");

            if (_context.Empleados.Any(x => x.NombreUsuarioEmpleado == user.NombreUsuarioEmpleado))
                throw new AppException("Username '" + user.NombreUsuarioEmpleado + "' is already taken");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            //  Usar esto cuando se actualice la forma de almacenar las contrasenias de texto plano a uso de hash
            //user.PasswordHash = passwordHash;
            //user.PasswordSalt = passwordSalt;
            user.ClaveUsuarioEmpleado = Encoding.ASCII.GetBytes(password);

            _context.Empleados.Add(user);
            _context.SaveChanges();

            return user;
        }

        public void Update(Empleados userParam, string password = null)
        {
            var user = _context.Empleados.Find(userParam.CodigoEmpleado);

            if (user == null)
                throw new AppException("User not found");

            if (userParam.NombreUsuarioEmpleado != user.NombreUsuarioEmpleado)
            {
                // username has changed so check if the new username is already taken
                if (_context.Empleados.Any(x => x.NombreUsuarioEmpleado == userParam.NombreUsuarioEmpleado))
                    throw new AppException("Username " + userParam.NombreUsuarioEmpleado + " is already taken");
            }

            // update user properties
            user.NombreEmpleado = userParam.NombreEmpleado;
            user.ApellidoEmpleado = userParam.ApellidoEmpleado;
            user.NombreUsuarioEmpleado = userParam.NombreUsuarioEmpleado;

            // update password if it was entered
            if (!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);

                //  Usar esto cuando se actualice la forma de almacenar las contrasenias de texto plano a uso de hash
                //user.PasswordHash = passwordHash;
                //user.PasswordSalt = passwordSalt;
                user.ClaveUsuarioEmpleado = Encoding.ASCII.GetBytes(password);
            }

            _context.Empleados.Update(user);
            _context.SaveChanges();
        }

        public void Delete(string id)
        {
            var user = _context.Empleados.Find(id);
            if (user != null)
            {
                _context.Empleados.Remove(user);
                _context.SaveChanges();
            }
        }

        // private helper methods

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}
