namespace Wizzi.Entities
{
    public partial class Empleadosatiendecallcenter
    {
        public string Tiposdocumentoempleadoatiendecallcenter { get; set; }
        public string Categoriasdocumentosinstalacionesempleadoatiendecallcenter { get; set; }
        public string Empleadoempleadoatiendecallcenter { get; set; }

        public virtual Categoriastiposdocumentosinstalaciones CategoriasdocumentosinstalacionesempleadoatiendecallcenterNavigation { get; set; }
        public virtual Empleados EmpleadoempleadoatiendecallcenterNavigation { get; set; }
        public virtual Tiposdocumentosinstalaciones TiposdocumentoempleadoatiendecallcenterNavigation { get; set; }
    }
}
