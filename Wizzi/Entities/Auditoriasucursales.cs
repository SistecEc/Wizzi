using System;

namespace Wizzi.Entities
{
    public partial class Auditoriasucursales
    {
        public string CodigoSucursal { get; set; }
        public string EmpresasSucursal { get; set; }
        public string NombreSucursal { get; set; }
        public string DireccionSucursal { get; set; }
        public string TelefonoUnoSucursal { get; set; }
        public string TelefonoDosSucursal { get; set; }
        public string FaxSucursal { get; set; }
        public string ActivaSucursal { get; set; }
        public string MatrizSucursal { get; set; }
        public string PaisSucursal { get; set; }
        public string ProvinciaSucursal { get; set; }
        public string CiudadesSucursal { get; set; }
        public string ParroquiaSucursal { get; set; }
        public string LatitudSucursal { get; set; }
        public string LongitudSucursal { get; set; }
        public sbyte ZoomUbicacionMapaSucursal { get; set; }
        public DateTime FechaRegistroSucursal { get; set; }
        public DateTime FechaModificacionSucursal { get; set; }
        public string UsuariosRegistraSucursal { get; set; }
        public string UsuarioModificaSucursal { get; set; }
        public string TipoAccion { get; set; }
    }
}
