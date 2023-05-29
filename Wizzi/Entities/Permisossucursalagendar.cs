namespace Wizzi.Entities
{
    public partial class Permisossucursalagendar
    {
        public string CodigoPermisoSucursalAgendar { get; set; }
        public string EmpleadosPermisoSucursalAgendar { get; set; }
        public string SucursalesPermisoSucursalAgendar { get; set; }

        public virtual Empleados EmpleadosPermisoSucursalAgendarNavigation { get; set; }
        public virtual Sucursales SucursalesPermisoSucursalAgendarNavigation { get; set; }
    }
}
