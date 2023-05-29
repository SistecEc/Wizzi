using System.Collections.Generic;

namespace Wizzi.Entities
{
    public partial class Empresas
    {
        public Empresas()
        {
            Empresasclientes = new HashSet<Empresasclientes>();
            Ordeninstalacion = new HashSet<Ordeninstalacion>();
            Sucursales = new HashSet<Sucursales>();
        }

        public string CodigoEmpresa { get; set; }
        public string RucEmpresa { get; set; }
        public string NombreEmpresa { get; set; }
        public string NombreEmpresaImpresion { get; set; }
        public string NombreComercialEmpresa { get; set; }
        public string CodigoDinardapEmpresa { get; set; }
        public string LogoEmpresa { get; set; }
        public int OrdenIncialEmpresa { get; set; }
        public int VisualizarNombreComercialEmpresa { get; set; }
        public string UsuariosEmpresa { get; set; }

        public virtual ICollection<Empresasclientes> Empresasclientes { get; set; }
        public virtual ICollection<Ordeninstalacion> Ordeninstalacion { get; set; }
        public virtual ICollection<Sucursales> Sucursales { get; set; }
    }
}
