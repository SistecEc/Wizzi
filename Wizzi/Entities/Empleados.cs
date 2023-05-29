using System;
using System.Collections.Generic;

namespace Wizzi.Entities
{
    public partial class Empleados
    {
        public Empleados()
        {
            Agendas = new HashSet<Agendas>();
            Empleadosatiendecallcenter = new HashSet<Empleadosatiendecallcenter>();
            Instalacionescabecera = new HashSet<Instalacionescabecera>();
            Ordeninstalacion = new HashSet<Ordeninstalacion>();
            Permisossucursalagendar = new HashSet<Permisossucursalagendar>();
        }

        public string CodigoEmpleado { get; set; }
        public string NombreEmpleado { get; set; }
        public string ApellidoEmpleado { get; set; }
        public string EmailEmpleado { get; set; }
        public string TiposEmpleadosEmpleado { get; set; }
        public string PerfilesEmpleado { get; set; }
        public string NombreUsuarioEmpleado { get; set; }
        public byte[] ClaveUsuarioEmpleado { get; set; }
        public string CambiarPrimeraVezEmpleado { get; set; }
        public string ClaveCaducaEmpleado { get; set; }
        public DateTime FechaCaducaClaveEmpleado { get; set; }
        public string UsuarioCaducaEmpleado { get; set; }
        public DateTime FechaCaducaUsuarioEmpleado { get; set; }
        public int SeleccionaEmpresaEmpleado { get; set; }
        public int SeleccionaSucursalEmpleado { get; set; }
        public string EmpresasEmpleado { get; set; }
        public string SucursalesEmpleado { get; set; }
        public string TiposIdentificacionEmpleado { get; set; }
        public string CedulaEmpleado { get; set; }
        public string TitulosEmpleado { get; set; }
        public string EspecialidadesMedicasEmpleado { get; set; }
        public string DireccionUnoEmpleado { get; set; }
        public string DireccionDosEmpleado { get; set; }
        public string TelefonoUnoEmpleado { get; set; }
        public string TelefonoDosEmpleado { get; set; }
        public string TelefonoTresEmpleado { get; set; }
        public string EmailEmpleadoPersonal { get; set; }
        public DateTime? FechaNacimientoEmpleado { get; set; }
        public int CargaFamiliarEmpleado { get; set; }
        public string CuentasContabilidadEmpleado { get; set; }
        public string CuentasContabilidadAnticipoEmpleado { get; set; }
        public string CuentasContabilidadViaticoEmpleado { get; set; }
        public string CuentasContabilidadPrestamoEmpleado { get; set; }
        public string CuentasContabilidadSaldoEmpleado { get; set; }
        public string CuentasContabilidadGastoEmpleado { get; set; }
        public string CuentasContabilidadIessEmpleado { get; set; }
        public string CuentasContabilidadIessPatronalEmpleado { get; set; }
        public string CuentasContabilidadIessGastoPatronalEmpleado { get; set; }
        public string CuentasContabilidadXiiiEmpleado { get; set; }
        public string CuentasContabilidadXivEmpleado { get; set; }
        public string CuentasContabilidadFondoReservaEmpleado { get; set; }
        public string CuentasContabilidadHoraJornadaNocturna { get; set; }
        public string CuentasContabilidadHoraSuplementariaEmpleado { get; set; }
        public string CuentasContabilidadHoraExtraordinariaEmpleado { get; set; }
        public string CuentasContabilidadRubrosEmpleado { get; set; }
        public string CuentasContabilidadAtrazoEmpleado { get; set; }
        public string CuentasContabilidadMultaEmpleado { get; set; }
        public string CuentasContabilidadVacacionEmpleado { get; set; }
        public string CuentasContabilidadComisionesEmpleado { get; set; }
        public string CuentasContabilidadNominasEmpleado { get; set; }
        public string CuentasContabilidadProvisionFondoReservaEmpleado { get; set; }
        public string CuentasContabilidadProvisionDecimoXiiiempleado { get; set; }
        public string CuentasContabilidadProvisionDecimoXivempleado { get; set; }
        public string CuentasContabilidadProvisionVacacionesEmpleado { get; set; }
        public string CuentasContabilidadPrestamosIess { get; set; }
        public string CuentasContabilidadQuincena { get; set; }
        public string CuentasContabilidadBonoFijo { get; set; }
        public string CuentasContabilidadBonoVariable { get; set; }
        public string CuentasContabilidadBonoAlimenticio { get; set; }
        public double SueldoNominalEmpleado { get; set; }
        public double? AdelantoQuincenalEmpleado { get; set; }
        public TimeSpan HoraIngresoEmpleado { get; set; }
        public TimeSpan HoraSalidaEmpleado { get; set; }
        public int TiempoAlmuerzoEmpleado { get; set; }
        public double LetraCambioEmpleado { get; set; }
        public string CostaEmpleado { get; set; }
        public string FondosReservaEmpleado { get; set; }
        public string RolPagoEmpleado { get; set; }
        public string HoraExtraEmpleado { get; set; }
        public string MarcarEmpleado { get; set; }
        public string CalcularFondosReservaEmpleado { get; set; }
        public string CambiadoClaveEmpleado { get; set; }
        public string FormularioInicialEmpleado { get; set; }
        public double PorcentajeAnticipoEmpleado { get; set; }
        public int NumeroDiasHabilesVacacionTomadosEmpleado { get; set; }
        public int NumeroDiasFinSemanaVacacionTomadosEmpleado { get; set; }
        public string TipoSincronizacionEmpleado { get; set; }
        public string EntregarDecimoEmpleado { get; set; }
        public string EntregarDecimoIvempleado { get; set; }
        public double BonoFijoEmpleado { get; set; }
        public double BonoVariableEmpleado { get; set; }
        public double BonoAlimentacionEmpleado { get; set; }
        public double ValorFondoReservaEmpleado { get; set; }
        public string TipoResidenciaEmpleado { get; set; }
        public string PaisResidenciaEmpleado { get; set; }
        public string DiscapacidadEmpleado { get; set; }
        public string ConvenioEvitarDobleImposicionEmpleado { get; set; }
        public decimal PorcentajeDiscapacidadEmpleado { get; set; }
        public string TipoIdentificacionReemplazaEmpleado { get; set; }
        public string CedulaRemplazoDiscapacidadEmplead { get; set; }
        public string SistemaSalarioNetoEmpleado { get; set; }
        public string TrabajaTiempoParcialEmpleado { get; set; }
        public string PresupuestarEmpleado { get; set; }
        public string RepresentanteLegalEmpleado { get; set; }
        public string CuentaBancariaEmpleado { get; set; }
        public string TipoCuentaBancariaEmpleado { get; set; }
        public string DuracionContratoEmpleado { get; set; }
        public string NoCalculaBeneficiosEmpleado { get; set; }
        public string DiasLaboraTiempoParcialEmpleado { get; set; }
        public string VerCostoEmpleado { get; set; }
        public string ImagenEmpleado { get; set; }
        public string NumeroCarnetConadisEmpleado { get; set; }
        public string SustitutoEmpleado { get; set; }
        public string UsuariosEmpleado { get; set; }
        public string IesscodigosSectorialesEmpleado { get; set; }
        public string EstadosCivilesPersonasEmpleados { get; set; }
        public string TiposSangrePersonasEmpleados { get; set; }
        public string GeneroEmpleado { get; set; }
        public string CuentasBancariasOrigenEmpleado { get; set; }
        public DateTime FechaCalculoVacacionesEmpleado { get; set; }
        public string CuentasContabilidadDesahucioEmpleado { get; set; }
        public string CuentasContabilidadDespidoEmpleado { get; set; }
        public string CuentasContabilidadOtrosIngresosEmpleado { get; set; }
        public string CuentasContabilidadOtrosEgresosEmpleado { get; set; }
        public string WebUsuarioEmpleado { get; set; }
        public string TrabajaConstruccionEmpleado { get; set; }
        public string TiposRubroEmpleados { get; set; }
        public decimal PorcentajeIessEmpleados { get; set; }
        public decimal PorcentajeIessPatronoEmpleados { get; set; }
        public int PermiteAgendamientoEmpleados { get; set; }
        public string PlantillaCuentasEmpleados { get; set; }

        public virtual Perfiles PerfilesEmpleadoNavigation { get; set; }
        public virtual Sucursales SucursalesEmpleadoNavigation { get; set; }
        public virtual Tiposidentificacion TipoIdentificacionReemplazaEmpleadoNavigation { get; set; }
        public virtual Tiposempleados TiposEmpleadosEmpleadoNavigation { get; set; }
        public virtual Tiposidentificacion TiposIdentificacionEmpleadoNavigation { get; set; }
        public virtual Titulos TitulosEmpleadoNavigation { get; set; }
        public virtual ICollection<Agendas> Agendas { get; set; }
        public virtual ICollection<Empleadosatiendecallcenter> Empleadosatiendecallcenter { get; set; }
        public virtual ICollection<Instalacionescabecera> Instalacionescabecera { get; set; }
        public virtual ICollection<Ordeninstalacion> Ordeninstalacion { get; set; }
        public virtual ICollection<Permisossucursalagendar> Permisossucursalagendar { get; set; }
    }
}
