using System;
using System.Collections.Generic;

namespace Wizzi.Entities
{
    public partial class Instalacionescabecera
    {
        public Instalacionescabecera()
        {
            Observacionesempleadosinstalaciones = new HashSet<Observacionesempleadosinstalaciones>();
        }

        public string CodigoInstalacionesCabecera { get; set; }
        public string NumeroSecuencialInstalacionesCabecera { get; set; }
        public string OrdenInstalacionInstalacionesCabecera { get; set; }
        public string EmpresaInstalacionesCabecera { get; set; }
        public string SucursalInstalacionesCabecera { get; set; }
        public string EmpleadoInstalacionesCabecera { get; set; }
        public string ClienteInstalacionesCabecera { get; set; }
        public DateTime FechaInstalacionesCabecera { get; set; }
        public string EstadoAsignacionInstalacionesCabecera { get; set; }
        public string EstadoInstalacionInstalacionesCabecera { get; set; }
        public string CategoriasFinalizacionInstalacionesCabecera { get; set; }
        public int FinalizadoMatrizInstalacionesCabecera { get; set; }
        public string LatitudInstalacionesCabecera { get; set; }
        public string LongitudInstalacionesCabecera { get; set; }
        public string BodegaOrigenInstalacionesCabecera { get; set; }
        public double SubTotalInstalacionesCabecera { get; set; }
        public double SubTotal0InstalacionesCabecera { get; set; }
        public string DescripcionInstalacionesCabecera { get; set; }
        public string ObservacionInstalacionesCabecera { get; set; }
        public string LocalInstalacionesCabecera { get; set; }
        public string EstadoAutorizacionInstalacionesCabecera { get; set; }
        public DateTime FechaAsignacionInstalacionesCabecera { get; set; }
        public DateTime FechaInstalacionInstalacionesCabecera { get; set; }
        public DateTime FechaAnulacionInstalacionesCabecera { get; set; }
        public string TiposDocumentoInstalacionesCabecera { get; set; }
        public string CategoriasTiposDocumentosInstalacionesCabecera { get; set; }
        public int NivelesPrioridadProcesosInstalacionesCabecera { get; set; }
        public string CodigoPadreInstalacionesCabecera { get; set; }
        public DateTime FechaRegistroAsignacionInstalacionesCabecera { get; set; }
        public string DocumentoOrigenInstalacionesCabecera { get; set; }
        public string UsuarioAsignaInstalacionesCabecera { get; set; }
        public string UsuarioInstalacionInstalacionesCabecera { get; set; }
        public string UsuarioAnulaInstalacionesCabecera { get; set; }

        public virtual Categoriasfinalizacioncallcenter CategoriasFinalizacionInstalacionesCabeceraNavigation { get; set; }
        public virtual Categoriastiposdocumentosinstalaciones CategoriasTiposDocumentosInstalacionesCabeceraNavigation { get; set; }
        public virtual Clientes ClienteInstalacionesCabeceraNavigation { get; set; }
        public virtual Empleados EmpleadoInstalacionesCabeceraNavigation { get; set; }
        public virtual Tiposfinalizacioncallcenter EstadoInstalacionInstalacionesCabeceraNavigation { get; set; }
        public virtual Nivelesprioridadprocesos NivelesPrioridadProcesosInstalacionesCabeceraNavigation { get; set; }
        public virtual Ordeninstalacion OrdenInstalacionInstalacionesCabeceraNavigation { get; set; }
        public virtual Tiposdocumentosinstalaciones TiposDocumentoInstalacionesCabeceraNavigation { get; set; }
        public virtual ICollection<Observacionesempleadosinstalaciones> Observacionesempleadosinstalaciones { get; set; }
    }
}
