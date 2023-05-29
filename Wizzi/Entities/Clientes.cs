using System;
using System.Collections.Generic;

namespace Wizzi.Entities
{
    public partial class Clientes
    {
        public Clientes()
        {
            Citasmedicas = new HashSet<Citasmedicas>();
            Empresasclientes = new HashSet<Empresasclientes>();
            Instalacionescabecera = new HashSet<Instalacionescabecera>();
            Ordeninstalacion = new HashSet<Ordeninstalacion>();
        }

        public string CodigoCliente { get; set; }
        public string CodigoSecuencialCliente { get; set; }
        public string TiposIdentificacionCliente { get; set; }
        public string NumeroIdentificacionCliente { get; set; }
        public string NombreComercialCliente { get; set; }
        public string PrioridadNombreComercialCliente { get; set; }
        public string CedulaRepresentanteLegalCliente { get; set; }
        public string NombreCliente { get; set; }
        public string ApellidoCliente { get; set; }
        public string CedulaConyugeCliente { get; set; }
        public string NombreConyugeCliente { get; set; }
        public string ApellidoConyugeCliente { get; set; }
        public DateTime FechaNacimientoConyugeCliente { get; set; }
        public string DireccionUnoCliente { get; set; }
        public string DireccionDosCliente { get; set; }
        public string SectorDireccionCliente { get; set; }
        public string TelefonoUnoCliente { get; set; }
        public string TelefonoDosCliente { get; set; }
        public string TelefonoTresCliente { get; set; }
        public string TelefonoCuatroCliente { get; set; }
        public string TelefonoCincoCliente { get; set; }
        public string TelefonoSeisCliente { get; set; }
        public string MailCliente { get; set; }
        public string WebCliente { get; set; }
        public string SexoCliente { get; set; }
        public string EstadoCivilCliente { get; set; }
        public DateTime FechaNacimientoCliente { get; set; }
        public string ObservacionCliente { get; set; }
        public string SolicitudesCreditoCliente { get; set; }
        public string ExigirDocumentosCliente { get; set; }
        public string CuentasContabilidadCliente { get; set; }
        public string TiposClienteCliente { get; set; }
        public string TiposNegocioCliente { get; set; }
        public string TiposNegocioSecundarioCliente { get; set; }
        public string TamanioNegociosCliente { get; set; }
        public string EstatalCliente { get; set; }
        public string RelacionCliente { get; set; }
        public string CiudadesCliente { get; set; }
        public string ZonasCliente { get; set; }
        public string TitulosCliente { get; set; }
        public string ListasPrecioCliente { get; set; }
        public string ListasPrecioMaximaCliente { get; set; }
        public string ContactoCliente { get; set; }
        public string ContribuyenteEspecialCliente { get; set; }
        public string TransportesCliente { get; set; }
        public string AplicaPromocionCliente { get; set; }
        public string EmiteRetencionCliente { get; set; }
        public string CalificacionTipoCliente { get; set; }
        public string CalificacionPagoCliente { get; set; }
        public string CalificacionUtilidadCliente { get; set; }
        public string TiposNegociosCalificacionCliente { get; set; }
        public string UsuariosCliente { get; set; }
        public string AgrocalidadCliente { get; set; }
        public string OmitirCupoCalificacionCliente { get; set; }
        public DateTime FechaRegistroCupoCalificacionCliente { get; set; }
        public DateTime FechaHastaCupoCalificacionCliente { get; set; }
        public string OmitirDescuentoCliente { get; set; }
        public string EmpleadoEmpresaCliente { get; set; }
        public string DinardapCliente { get; set; }
        public string RelacionadoEmpresaCliente { get; set; }
        public string BodegasCliente { get; set; }
        public string Tiposclientesegurocliente { get; set; }
        public string PagaChequeCliente { get; set; }
        public string PagaChequePosfechadoCliente { get; set; }
        public string PuedeTenerConsignacionCliente { get; set; }
        public double MontoMinimoCreditoCliente { get; set; }
        public decimal MontoMaximoConsignacionCliente { get; set; }
        public int DiasDemoraEntregaCliente { get; set; }
        public string RutasEntregasCliente { get; set; }
        public string EmailDespahosCliente { get; set; }
        public string TiposClienteCarteraCliente { get; set; }
        public string UsuariosModificaCliente { get; set; }

        public virtual Tiposclientescartera TiposClienteCarteraClienteNavigation { get; set; }
        public virtual Tiposidentificacion TiposIdentificacionClienteNavigation { get; set; }
        public virtual Transportes TransportesClienteNavigation { get; set; }
        public virtual Clienteslocalizaciones Clienteslocalizaciones { get; set; }
        public virtual ICollection<Citasmedicas> Citasmedicas { get; set; }
        public virtual ICollection<Empresasclientes> Empresasclientes { get; set; }
        public virtual ICollection<Instalacionescabecera> Instalacionescabecera { get; set; }
        public virtual ICollection<Ordeninstalacion> Ordeninstalacion { get; set; }
    }
}
