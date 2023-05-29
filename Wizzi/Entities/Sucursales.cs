using System;
using System.Collections.Generic;

namespace Wizzi.Entities
{
    public partial class Sucursales
    {
        public Sucursales()
        {
            Empleados = new HashSet<Empleados>();
            Ordeninstalacion = new HashSet<Ordeninstalacion>();
            Permisossucursalagendar = new HashSet<Permisossucursalagendar>();
            Solicitudcitasmedicas = new HashSet<Solicitudcitasmedicas>();
        }

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
        public string MidRecargasSucursal { get; set; }
        public string EstablecimientoRdepsucursal { get; set; }
        public int ActivaAgendamientoSucursal { get; set; }
        public DateTime FechaRegistroSucursal { get; set; }
        public DateTime FechaModificacionSucursal { get; set; }
        public string UsuariosRegistraSucursal { get; set; }
        public string UsuarioModificaSucursal { get; set; }

        public virtual Localizacionescantones CiudadesSucursalNavigation { get; set; }
        public virtual Empresas EmpresasSucursalNavigation { get; set; }
        public virtual Localizacionespaises PaisSucursalNavigation { get; set; }
        public virtual Localizacionesparroquias ParroquiaSucursalNavigation { get; set; }
        public virtual Localizacionesprovincias ProvinciaSucursalNavigation { get; set; }
        public virtual ICollection<Empleados> Empleados { get; set; }
        public virtual ICollection<Ordeninstalacion> Ordeninstalacion { get; set; }
        public virtual ICollection<Permisossucursalagendar> Permisossucursalagendar { get; set; }
        public virtual ICollection<Solicitudcitasmedicas> Solicitudcitasmedicas { get; set; }
    }
}
