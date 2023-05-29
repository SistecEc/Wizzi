using System;
using System.Collections.Generic;

namespace Wizzi.Entities
{
    public partial class Ordeninstalacion
    {
        public Ordeninstalacion()
        {
            Instalacionescabecera = new HashSet<Instalacionescabecera>();
        }

        public string CodigoOrdenInstalacion { get; set; }
        public string EmpresaOrdenInstalacion { get; set; }
        public string SucursalOrdenInstalacion { get; set; }
        public string EmpleadoRegistroOrdenInstalacion { get; set; }
        public string ContratoCabeceraOrdenInstalacion { get; set; }
        public string ClienteOrdenInstalacion { get; set; }
        public DateTime FechaRegistroOrdenInstalacion { get; set; }
        public string MotivosDocumentosInstalaciones { get; set; }
        public int EstadoOrdenInstalacion { get; set; }
        public string UsuarioRegistroOrdenInstalacion { get; set; }
        public string FormaReclamoOrdenInstalacion { get; set; }
        public string CategoriaArcotelOrdenInstalacion { get; set; }

        public virtual Categoriaarcotel CategoriaArcotelOrdenInstalacionNavigation { get; set; }
        public virtual Clientes ClienteOrdenInstalacionNavigation { get; set; }
        public virtual Empleados EmpleadoRegistroOrdenInstalacionNavigation { get; set; }
        public virtual Empresas EmpresaOrdenInstalacionNavigation { get; set; }
        public virtual Formareclamo FormaReclamoOrdenInstalacionNavigation { get; set; }
        public virtual Sucursales SucursalOrdenInstalacionNavigation { get; set; }
        public virtual ICollection<Instalacionescabecera> Instalacionescabecera { get; set; }
    }
}
