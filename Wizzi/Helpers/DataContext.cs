using Microsoft.EntityFrameworkCore;
using Wizzi.Entities;

namespace Wizzi.Helpers
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public virtual DbSet<Agendas> Agendas { get; set; }
        public virtual DbSet<Auditoriacuentascontabilidad> Auditoriacuentascontabilidad { get; set; }
        public virtual DbSet<Auditoriaempresas> Auditoriaempresas { get; set; }
        public virtual DbSet<Auditoriasclientes> Auditoriasclientes { get; set; }
        public virtual DbSet<Auditoriasempleados> Auditoriasempleados { get; set; }
        public virtual DbSet<Auditoriasucursales> Auditoriasucursales { get; set; }
        public virtual DbSet<Auditoriatiposempleados> Auditoriatiposempleados { get; set; }
        public virtual DbSet<Auditoriatiposidentificacion> Auditoriatiposidentificacion { get; set; }
        public virtual DbSet<Auditoriatitulos> Auditoriatitulos { get; set; }
        public virtual DbSet<Campanias> Campanias { get; set; }
        public virtual DbSet<Categoriaarcotel> Categoriaarcotel { get; set; }
        public virtual DbSet<Categoriasfinalizacioncallcenter> Categoriasfinalizacioncallcenter { get; set; }
        public virtual DbSet<Categoriastiposdocumentosinstalaciones> Categoriastiposdocumentosinstalaciones { get; set; }
        public virtual DbSet<Citasmedicas> Citasmedicas { get; set; }
        public virtual DbSet<Clientes> Clientes { get; set; }
        public virtual DbSet<Clienteslocalizaciones> Clienteslocalizaciones { get; set; }
        public virtual DbSet<Clientesvendedores> Clientesvendedores { get; set; }
        public virtual DbSet<Cuentascontabilidad> Cuentascontabilidad { get; set; }
        public virtual DbSet<Docspendientes> Docspendientes { get; set; }
        public virtual DbSet<Empleados> Empleados { get; set; }
        public virtual DbSet<Empleadosatiendecallcenter> Empleadosatiendecallcenter { get; set; }
        public virtual DbSet<Empresas> Empresas { get; set; }
        public virtual DbSet<Empresasclientes> Empresasclientes { get; set; }
        public virtual DbSet<Formareclamo> Formareclamo { get; set; }
        public virtual DbSet<Fuentesremision> Fuentesremision { get; set; }
        public virtual DbSet<Instalacionescabecera> Instalacionescabecera { get; set; }
        public virtual DbSet<Listasprecios> Listasprecios { get; set; }
        public virtual DbSet<Localizacionescantones> Localizacionescantones { get; set; }
        public virtual DbSet<Localizacionespaises> Localizacionespaises { get; set; }
        public virtual DbSet<Localizacionesparroquias> Localizacionesparroquias { get; set; }
        public virtual DbSet<Localizacionesprovincias> Localizacionesprovincias { get; set; }
        public virtual DbSet<Movimientocampanias> Movimientocampanias { get; set; }
        public virtual DbSet<Nivelesprioridadprocesos> Nivelesprioridadprocesos { get; set; }
        public virtual DbSet<Observacionesempleadosinstalaciones> Observacionesempleadosinstalaciones { get; set; }
        public virtual DbSet<Ordeninstalacion> Ordeninstalacion { get; set; }
        public virtual DbSet<Parametros> Parametros { get; set; }
        public virtual DbSet<Perfiles> Perfiles { get; set; }
        public virtual DbSet<Permisossucursalagendar> Permisossucursalagendar { get; set; }
        public virtual DbSet<Relacionrepresentantepaciente> Relacionrepresentantepaciente { get; set; }
        public virtual DbSet<Solicitudcitasmedicas> Solicitudcitasmedicas { get; set; }
        public virtual DbSet<Subcampanias> Subcampanias { get; set; }
        public virtual DbSet<Sucursales> Sucursales { get; set; }
        public virtual DbSet<Tiposagendas> Tiposagendas { get; set; }
        public virtual DbSet<Tiposajustes> Tiposajustes { get; set; }
        public virtual DbSet<Tiposclientescartera> Tiposclientescartera { get; set; }
        public virtual DbSet<Tiposdocumentosinstalaciones> Tiposdocumentosinstalaciones { get; set; }
        public virtual DbSet<Tiposempleados> Tiposempleados { get; set; }
        public virtual DbSet<Tiposfinalizacioncallcenter> Tiposfinalizacioncallcenter { get; set; }
        public virtual DbSet<Tiposidentificacion> Tiposidentificacion { get; set; }
        public virtual DbSet<Titulos> Titulos { get; set; }
        public virtual DbSet<Transportes> Transportes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Agendas>(entity =>
            {
                entity.HasKey(e => e.CodigoAgenda)
                    .HasName("PRIMARY");

                entity.ToTable("agendas");

                entity.HasIndex(e => e.EmpleadosAgenda)
                    .HasName("Agendas_fk0");

                entity.HasIndex(e => e.TiposAgendasAgenda)
                    .HasName("Agendas_fk1");

                entity.Property(e => e.CodigoAgenda)
                    .HasColumnType("varchar(22)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.DescripcionAgenda)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.EmpleadosAgenda)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.EsTodoElDiaAgenda).HasColumnType("int(1)");

                entity.Property(e => e.EstadoAgenda).HasColumnType("int(1)");

                entity.Property(e => e.FechaFinAgenda).HasColumnType("datetime");

                entity.Property(e => e.FechaInicioAgenda).HasColumnType("datetime");

                entity.Property(e => e.FechaRegistroAgenda).HasColumnType("datetime");

                entity.Property(e => e.FechaUltimaModificacionAgenda).HasColumnType("datetime");

                entity.Property(e => e.FechasExluidasRecurrencia)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasComment("https://tools.ietf.org/html/rfc5545#section-3.8.5.1")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ReglaRecurrenciaAgenda)
                    .HasColumnType("text")
                    .HasComment("https://tools.ietf.org/html/rfc5545#section-3.8.5.3")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TiposAgendasAgenda).HasColumnType("int(1)");

                entity.Property(e => e.TituloAgenda)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.HasOne(d => d.EmpleadosAgendaNavigation)
                    .WithMany(p => p.Agendas)
                    .HasForeignKey(d => d.EmpleadosAgenda)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Agendas_fk0");

                entity.HasOne(d => d.TiposAgendasAgendaNavigation)
                    .WithMany(p => p.Agendas)
                    .HasForeignKey(d => d.TiposAgendasAgenda)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Agendas_fk1");
            });

            modelBuilder.Entity<Auditoriacuentascontabilidad>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("auditoriacuentascontabilidad");

                entity.HasComment("Adminitracion de cuentas contailidad;like NombreCuenta; Inno");

                entity.HasIndex(e => e.CodigoCuentaContable)
                    .HasName("Index_Codigo");

                entity.HasIndex(e => e.NombreCuentaContable)
                    .HasName("Index_Nombre");

                entity.HasIndex(e => e.TipoAccion)
                    .HasName("Index_Accion");

                entity.Property(e => e.CodigoCuentaContable)
                    .IsRequired()
                    .HasColumnType("varchar(18)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NombreCuentaContable)
                    .IsRequired()
                    .HasColumnType("varchar(250)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TipoAccion)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.UsuariosCuentaContabilidad)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");
            });

            modelBuilder.Entity<Auditoriaempresas>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("auditoriaempresas");

                entity.HasComment("Administracion de Empresas;like NombreEmpresa");

                entity.HasIndex(e => e.CodigoEmpresa)
                    .HasName("Index_Codigo");

                entity.HasIndex(e => e.NombreEmpresa)
                    .HasName("Index_Nombre");

                entity.HasIndex(e => e.RucEmpresa)
                    .HasName("Index_RUC");

                entity.HasIndex(e => e.TipoAccion)
                    .HasName("Index_Accion");

                entity.Property(e => e.CodigoEmpresa)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NombreEmpresa)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.RucEmpresa)
                    .IsRequired()
                    .HasColumnType("varchar(13)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TipoAccion)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.UsuariosEmpresa)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");
            });

            modelBuilder.Entity<Auditoriasclientes>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("auditoriasclientes");

                entity.Property(e => e.AgrocalidadCliente)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ApellidoCliente)
                    .IsRequired()
                    .HasColumnType("varchar(60)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ApellidoConyugeCliente)
                    .IsRequired()
                    .HasColumnType("varchar(60)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.AplicaPromocionCliente)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasComment("0")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.BodegasCliente)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'130206666653994'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CalificacionPagoCliente)
                    .IsRequired()
                    .HasColumnType("char(1)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CalificacionTipoCliente)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'E'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CalificacionUtilidadCliente)
                    .IsRequired()
                    .HasColumnType("char(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CedulaConyugeCliente)
                    .IsRequired()
                    .HasColumnType("varchar(10)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CedulaRepresentanteLegalCliente)
                    .IsRequired()
                    .HasColumnType("varchar(10)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CiudadesCliente)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CodigoCliente)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CodigoSecuencialCliente)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ContactoCliente)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ContribuyenteEspecialCliente)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentasContabilidadCliente)
                    .IsRequired()
                    .HasColumnType("varchar(18)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.DinardapCliente)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'1'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.DireccionDosCliente)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.DireccionUnoCliente)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.EmiteRetencionCliente)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.EmpleadoEmpresaCliente)
                    .IsRequired()
                    .HasColumnType("varchar(2)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.EstadoCivilCliente)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'S'")
                    .HasComment("S SOLTERO C CASADO V VIUDO D DIVORCIADO U UNION LIBRE")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.EstatalCliente)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ExigirDocumentosCliente)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.FechaHastaCupoCalificacionCliente)
                    .HasColumnType("date")
                    .HasDefaultValueSql("'1900-01-01'");

                entity.Property(e => e.FechaNacimientoCliente)
                    .HasColumnType("date")
                    .HasDefaultValueSql("'1900-01-01'");

                entity.Property(e => e.FechaRegistro).HasColumnType("datetime");

                entity.Property(e => e.FechaRegistroCupoCalificacionCliente)
                    .HasColumnType("date")
                    .HasDefaultValueSql("'1900-01-01'");

                entity.Property(e => e.ListasPrecioCliente)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ListasPrecioMaximaCliente)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.MailCliente)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NombreCliente)
                    .IsRequired()
                    .HasColumnType("varchar(60)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NombreComercialCliente)
                    .IsRequired()
                    .HasColumnType("varchar(200)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NombreConyugeCliente)
                    .IsRequired()
                    .HasColumnType("varchar(60)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NumeroIdentificacionCliente)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ObservacionCliente)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.OmitirCupoCalificacionCliente)
                    .IsRequired()
                    .HasColumnType("varchar(5)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.OmitirDescuentoCliente)
                    .IsRequired()
                    .HasColumnType("varchar(5)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.PrioridadNombreComercialCliente)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.RelacionCliente)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.RelacionadoEmpresaCliente)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.SectorDireccionCliente)
                    .IsRequired()
                    .HasColumnType("varchar(150)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.SexoCliente)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'M'")
                    .HasComment("M MASCULINO F FEMENINO")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.SolicitudesCreditoCliente)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TamanioNegociosCliente)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TelefonoCincoCliente)
                    .IsRequired()
                    .HasColumnType("varchar(12)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TelefonoCuatroCliente)
                    .IsRequired()
                    .HasColumnType("varchar(12)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TelefonoDosCliente)
                    .IsRequired()
                    .HasColumnType("varchar(12)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TelefonoSeisCliente)
                    .IsRequired()
                    .HasColumnType("varchar(12)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TelefonoTresCliente)
                    .IsRequired()
                    .HasColumnType("varchar(12)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TelefonoUnoCliente)
                    .IsRequired()
                    .HasColumnType("varchar(12)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TipoAccion)
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TiposClienteCliente)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TiposIdentificacionCliente)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TiposNegocioCliente)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TiposNegocioSecundarioCliente)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TiposNegociosCalificacionCliente)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'1'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TitulosCliente)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TransportesCliente)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.UsuariosCliente)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.WebCliente)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ZonasCliente)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");
            });

            modelBuilder.Entity<Auditoriasempleados>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("auditoriasempleados");

                entity.Property(e => e.AdelantoQuincenalEmpleado)
                    .HasColumnType("double(16,4)")
                    .HasDefaultValueSql("'0.0000'");

                entity.Property(e => e.ApellidoEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.BonoFijoEmpleado).HasColumnType("double(16,4)");

                entity.Property(e => e.CalcularFondosReservaEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CambiadoClaveEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CambiarPrimeraVezEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CargaFamiliarEmpleado).HasColumnType("int(11)");

                entity.Property(e => e.CedulaEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(10)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ClaveCaducaEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ClaveUsuarioEmpleado)
                    .IsRequired()
                    .HasColumnType("blob");

                entity.Property(e => e.CodigoEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CostaEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentasContabilidadAnticipoEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentasContabilidadAtrazoEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentasContabilidadComisionesEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'5.01.02.005.001'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentasContabilidadEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(18)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentasContabilidadFondoReservaEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentasContabilidadGastoEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentasContabilidadHoraExtraordinariaEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentasContabilidadHoraSuplementariaEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentasContabilidadIessEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentasContabilidadIessGastoPatronalEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'5.01.02.002.002'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentasContabilidadIessPatronalEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentasContabilidadMultaEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentasContabilidadNominasEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'2.01.07.003.001'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentasContabilidadPrestamoEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentasContabilidadPrestamosIess)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'2.01.07.001.003'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentasContabilidadProvisionDecimoXiiiempleado)
                    .IsRequired()
                    .HasColumnName("CuentasContabilidadProvisionDecimoXIIIEmpleado")
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'2.01.07.001.001'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentasContabilidadProvisionDecimoXivempleado)
                    .IsRequired()
                    .HasColumnName("CuentasContabilidadProvisionDecimoXIVEmpleado")
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'2.01.07.001.002'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentasContabilidadProvisionFondoReservaEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'2.01.07.001.004'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentasContabilidadProvisionVacacionesEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'2.01.07.001.003'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentasContabilidadQuincena)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'1.01.02.010.160'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentasContabilidadRubrosEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentasContabilidadSaldoEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentasContabilidadVacacionEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentasContabilidadViaticoEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentasContabilidadXiiiEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentasContabilidadXivEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.DireccionDosEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.DireccionUnoEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.EmailEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.EmailEmpleadoPersonal)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("'NA'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.EmpresasEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.EntregarDecimoEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.EspecialidadesMedicasEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.FechaCaducaClaveEmpleado)
                    .HasColumnType("date")
                    .HasDefaultValueSql("'1900-01-01'");

                entity.Property(e => e.FechaCaducaUsuarioEmpleado)
                    .HasColumnType("date")
                    .HasDefaultValueSql("'1900-01-01'");

                entity.Property(e => e.FechaNacimientoEmpleado)
                    .HasColumnType("date")
                    .HasDefaultValueSql("'1900-01-01'");

                entity.Property(e => e.FechaRegistro).HasColumnType("datetime");

                entity.Property(e => e.FondosReservaEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.FormularioInicialEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("'frmmarcacion.aspx'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.HoraExtraEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.HoraIngresoEmpleado)
                    .HasColumnType("time")
                    .HasDefaultValueSql("'08:30:00'");

                entity.Property(e => e.HoraSalidaEmpleado)
                    .HasColumnType("time")
                    .HasDefaultValueSql("'18:00:00'");

                entity.Property(e => e.LetraCambioEmpleado).HasColumnType("double(16,2)");

                entity.Property(e => e.MarcarEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NombreEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(150)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NombreUsuarioEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NumeroDiasFinSemanaVacacionTomadosEmpleado).HasColumnType("int(3)");

                entity.Property(e => e.NumeroDiasHabilesVacacionTomadosEmpleado).HasColumnType("int(3)");

                entity.Property(e => e.PerfilesEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.PorcentajeAnticipoEmpleado)
                    .HasColumnType("double(16,4)")
                    .HasDefaultValueSql("'70.0000'");

                entity.Property(e => e.RolPagoEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.SeleccionaEmpresaEmpleado)
                    .HasColumnType("tinyint(5)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.SeleccionaSucursalEmpleado)
                    .HasColumnType("tinyint(5)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.SucursalesEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.SueldoNominalEmpleado).HasColumnType("double(16,4)");

                entity.Property(e => e.TelefonoDosEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(12)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TelefonoTresEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(12)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TelefonoUnoEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(12)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TiempoAlmuerzoEmpleado)
                    .HasColumnType("int(3)")
                    .HasDefaultValueSql("'90'");

                entity.Property(e => e.TipoAccion)
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TipoSincronizacionEmpleado)
                    .IsRequired()
                    .HasColumnType("char(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TiposEmpleadosEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TitulosEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.UsuarioCaducaEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.UsuariosEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'fmaldonado'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");
            });

            modelBuilder.Entity<Auditoriasucursales>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("auditoriasucursales");

                entity.HasIndex(e => e.CodigoSucursal)
                    .HasName("Index_Codigo");

                entity.HasIndex(e => e.EmpresasSucursal)
                    .HasName("Index_Empresa");

                entity.HasIndex(e => e.TipoAccion)
                    .HasName("Index_Accion");

                entity.HasIndex(e => e.UsuariosRegistraSucursal)
                    .HasName("Index_Usuarios");

                entity.Property(e => e.ActivaSucursal)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CiudadesSucursal)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CodigoSucursal)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.DireccionSucursal)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.EmpresasSucursal)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.FaxSucursal)
                    .IsRequired()
                    .HasColumnType("varchar(14)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.FechaModificacionSucursal).HasColumnType("datetime");

                entity.Property(e => e.FechaRegistroSucursal).HasColumnType("datetime");

                entity.Property(e => e.LatitudSucursal)
                    .IsRequired()
                    .HasColumnType("varchar(25)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.LongitudSucursal)
                    .IsRequired()
                    .HasColumnType("varchar(25)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.MatrizSucursal)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NombreSucursal)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.PaisSucursal)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ParroquiaSucursal)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ProvinciaSucursal)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TelefonoDosSucursal)
                    .IsRequired()
                    .HasColumnType("varchar(14)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TelefonoUnoSucursal)
                    .IsRequired()
                    .HasColumnType("varchar(14)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TipoAccion)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.UsuarioModificaSucursal)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.UsuariosRegistraSucursal)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ZoomUbicacionMapaSucursal).HasColumnType("tinyint(5)");
            });

            modelBuilder.Entity<Auditoriatiposempleados>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("auditoriatiposempleados");

                entity.HasIndex(e => e.CodigoTipoEmpleado)
                    .HasName("Index_Codigo");

                entity.HasIndex(e => e.NombreTipoEmpleado)
                    .HasName("Index_Nombre");

                entity.Property(e => e.CodigoTipoEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NombreTipoEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TipoAccion)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.UsuariosTipoEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");
            });

            modelBuilder.Entity<Auditoriatiposidentificacion>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("auditoriatiposidentificacion");

                entity.HasIndex(e => e.CodigoTipoIdentificacion)
                    .HasName("Index_Codigo");

                entity.HasIndex(e => e.NombreTipoIdentificacion)
                    .HasName("Index_Nombre");

                entity.HasIndex(e => e.TipoAccion)
                    .HasName("Index_Accion");

                entity.Property(e => e.CodigoTipoIdentificacion)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NombreTipoIdentificacion)
                    .IsRequired()
                    .HasColumnType("varchar(60)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NumeroCaracterTipoIdentificacion).HasColumnType("int(10) unsigned");

                entity.Property(e => e.TipoAccion)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.UsuariosTipoIdentificacion)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");
            });

            modelBuilder.Entity<Auditoriatitulos>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("auditoriatitulos");

                entity.HasIndex(e => e.CodigoTitulo)
                    .HasName("Index_Codigo");

                entity.HasIndex(e => e.NombreTitulo)
                    .HasName("Index_Nombre");

                entity.HasIndex(e => e.TipoAccion)
                    .HasName("Index_Accion");

                entity.Property(e => e.CodigoTitulo)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NombreTitulo)
                    .IsRequired()
                    .HasColumnType("varchar(60)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TipoAccion)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.UsuariosTitulo)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");
            });

            modelBuilder.Entity<Campanias>(entity =>
            {
                entity.HasKey(e => e.CodigoCampania)
                    .HasName("PRIMARY");

                entity.ToTable("campanias");

                entity.Property(e => e.CodigoCampania)
                    .HasColumnType("varchar(22)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.DescripcionCampania)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.FechaFinCampania).HasColumnType("datetime");

                entity.Property(e => e.FechaInicioCampania).HasColumnType("datetime");

                entity.Property(e => e.PresupuestoCampania).HasColumnType("decimal(16,4)");

                entity.Property(e => e.TituloCampania)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");
            });

            modelBuilder.Entity<Categoriaarcotel>(entity =>
            {
                entity.HasKey(e => e.CodigoCategoriaAlcotel)
                    .HasName("PRIMARY");

                entity.ToTable("categoriaarcotel");

                entity.Property(e => e.CodigoCategoriaAlcotel)
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.DescripcionCategoriaCategoriaArcotel)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.UsuarioCategoriaCategoriaAlcotel)
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");
            });

            modelBuilder.Entity<Categoriasfinalizacioncallcenter>(entity =>
            {
                entity.HasKey(e => e.CodigoCategoriaFinalizacionCallCenter)
                    .HasName("PRIMARY");

                entity.ToTable("categoriasfinalizacioncallcenter");

                entity.HasIndex(e => e.TiposFinalizacionCategoriaFinalizacionCallCenter)
                    .HasName("TiposFinalizacionCategoriaFinalizacionCallCenter");

                entity.Property(e => e.CodigoCategoriaFinalizacionCallCenter)
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.DescripcionCategoriaFinalizacionCallCenter)
                    .IsRequired()
                    .HasColumnType("varchar(200)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TiposFinalizacionCategoriaFinalizacionCallCenter)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.UsuariosCategoriaFinalizacionCallCenter)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.HasOne(d => d.TiposFinalizacionCategoriaFinalizacionCallCenterNavigation)
                    .WithMany(p => p.Categoriasfinalizacioncallcenter)
                    .HasForeignKey(d => d.TiposFinalizacionCategoriaFinalizacionCallCenter)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("TiposFinalizacionCategoriaFinalizacionCallCenter");
            });

            modelBuilder.Entity<Categoriastiposdocumentosinstalaciones>(entity =>
            {
                entity.HasKey(e => e.CodigoCategoriasTiposDocumentosInstalaciones)
                    .HasName("PRIMARY");

                entity.ToTable("categoriastiposdocumentosinstalaciones");

                entity.HasIndex(e => e.CodigoCategoriasTiposDocumentosInstalaciones)
                    .HasName("CodigoCategoriaTipoDocumentoInstalaciones")
                    .IsUnique();

                entity.HasIndex(e => e.TiposDocumentosInstalacionesCategoriaTiposDocumentoInstalaciones)
                    .HasName("TiposDocumentosInstalacionesCategoriaTipoDocumentoInstalaciones");

                entity.Property(e => e.CodigoCategoriasTiposDocumentosInstalaciones)
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasComment("Codigo de Categoría;text;true;false;Datos;180;left")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.DescripcionCategoriasTiposDocumentosInstalaciones)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("''")
                    .HasComment("Descripcion de Categoría;text;true;true;Datos;180;left")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.HabilitadoCategoriasTiposDocumentosInstalaciones)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'1'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.OrdenCategoriasTiposDocumentosInstalaciones).HasColumnType("int(3)");

                entity.Property(e => e.TiposDocumentosInstalacionesCategoriaTiposDocumentoInstalaciones)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasComment("Tipo de Proceso;cmb;true;true;Datos;180;left;SELECT CodigoTipoDocumentoInstalacion, DescripcionTipoDocumentoInstalacion FROM tiposdocumentosinstalaciones")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.UsuariosCategoriasTiposDocumentosInstalaciones)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.HasOne(d => d.TiposDocumentosInstalacionesCategoriaTiposDocumentoInstalacionesNavigation)
                    .WithMany(p => p.Categoriastiposdocumentosinstalaciones)
                    .HasForeignKey(d => d.TiposDocumentosInstalacionesCategoriaTiposDocumentoInstalaciones)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TipoDocumentoInstalacion_Categorias");
            });

            modelBuilder.Entity<Citasmedicas>(entity =>
            {
                entity.HasKey(e => e.CodigoCitaMedica)
                    .HasName("PRIMARY");

                entity.ToTable("citasmedicas");

                entity.HasIndex(e => e.AgendasCitaMedica)
                    .HasName("FK_Agendas_CitaMedica")
                    .IsUnique();

                entity.HasIndex(e => e.ClientesCitaMedica)
                    .HasName("CitasMedicas_fk1");

                entity.HasIndex(e => e.FuentesRemisionCitaMedica)
                    .HasName("FK_FuenteRemision_CitaMedica");

                entity.HasIndex(e => e.SolicitudesCitaMedica)
                    .HasName("CitasMedicas_fk2");

                entity.HasIndex(e => e.SubCampaniasOrigen)
                    .HasName("SubcampaniaOrigenFK_idx");

                entity.HasIndex(e => e.TipoCitaMedica)
                    .HasName("FK_CategoriaTipoDocumentoInstalacion_CitaMedica");

                entity.Property(e => e.CodigoCitaMedica)
                    .HasColumnType("varchar(22)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ActivaCitaMedica)
                    .HasColumnType("int(1)")
                    .HasDefaultValueSql("'1'")
                    .HasComment("Si la cita está activa se puede seguir agendando hasta que una agenda termine como atendida, si no está activa ya no se podrá agendar y todas las agendas deben ser canceladas");

                entity.Property(e => e.AgendasCitaMedica)
                    .IsRequired()
                    .HasColumnType("varchar(22)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ClientesCitaMedica)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CodigoGrupoCitaMedica)
                    .IsRequired()
                    .HasColumnName("codigoGrupoCitaMedica")
                    .HasColumnType("varchar(22)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.DiagnosticoCitaMedica)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.FechaRegistroCitaMedica).HasColumnType("datetime");

                entity.Property(e => e.FuentesRemisionCitaMedica)
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.PacienteLlegoCitaMedica)
                    .HasColumnName("PacienteLLegoCitaMedica")
                    .HasColumnType("int(1)");

                entity.Property(e => e.SolicitudesCitaMedica)
                    .HasColumnType("varchar(22)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.SubCampaniasOrigen)
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TipoCitaMedica)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.HasOne(d => d.AgendasCitaMedicaNavigation)
                    .WithOne(p => p.Citasmedicas)
                    .HasForeignKey<Citasmedicas>(d => d.AgendasCitaMedica)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Agendas_CitaMedica");

                entity.HasOne(d => d.ClientesCitaMedicaNavigation)
                    .WithMany(p => p.Citasmedicas)
                    .HasForeignKey(d => d.ClientesCitaMedica)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CitasMedicas_fk1");

                entity.HasOne(d => d.FuentesRemisionCitaMedicaNavigation)
                    .WithMany(p => p.Citasmedicas)
                    .HasForeignKey(d => d.FuentesRemisionCitaMedica)
                    .HasConstraintName("FK_FuenteRemision_CitaMedica");

                entity.HasOne(d => d.SolicitudesCitaMedicaNavigation)
                    .WithMany(p => p.Citasmedicas)
                    .HasForeignKey(d => d.SolicitudesCitaMedica)
                    .HasConstraintName("CitasMedicas_fk2");

                entity.HasOne(d => d.SubCampaniasOrigenNavigation)
                    .WithMany(p => p.Citasmedicas)
                    .HasForeignKey(d => d.SubCampaniasOrigen)
                    .HasConstraintName("SubcampaniaOrigen_CitaMedica_FK");

                entity.HasOne(d => d.TipoCitaMedicaNavigation)
                    .WithMany(p => p.Citasmedicas)
                    .HasForeignKey(d => d.TipoCitaMedica)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CategoriaTipoDocumentoInstalacion_CitaMedica");
            });

            modelBuilder.Entity<Clientes>(entity =>
            {
                entity.HasKey(e => e.CodigoCliente)
                    .HasName("PRIMARY");

                entity.ToTable("clientes");

                entity.HasComment("Cliente;NombreCliente,ApellidoCliente;NombreComercialCliente");

                entity.HasIndex(e => e.CodigoCliente)
                    .HasName("codigocliente")
                    .IsUnique();

                entity.HasIndex(e => e.TiposClienteCarteraCliente)
                    .HasName("FK_TiposClienteCarteraCliente");

                entity.HasIndex(e => e.TiposIdentificacionCliente)
                    .HasName("TiposIdentificacionCliente");

                entity.HasIndex(e => e.TransportesCliente)
                    .HasName("Transportes");

                entity.HasIndex(e => new { e.NumeroIdentificacionCliente, e.TiposIdentificacionCliente })
                    .HasName("Identificaion")
                    .IsUnique();

                entity.Property(e => e.CodigoCliente)
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.AgrocalidadCliente)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ApellidoCliente)
                    .IsRequired()
                    .HasColumnType("varchar(60)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ApellidoConyugeCliente)
                    .IsRequired()
                    .HasColumnType("varchar(60)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.AplicaPromocionCliente)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasComment("0")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.BodegasCliente)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'130206666653994'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CalificacionPagoCliente)
                    .IsRequired()
                    .HasColumnType("char(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CalificacionTipoCliente)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'E'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CalificacionUtilidadCliente)
                    .IsRequired()
                    .HasColumnType("char(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CedulaConyugeCliente)
                    .IsRequired()
                    .HasColumnType("varchar(10)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CedulaRepresentanteLegalCliente)
                    .IsRequired()
                    .HasColumnType("varchar(10)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CiudadesCliente)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CodigoSecuencialCliente)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ContactoCliente)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ContribuyenteEspecialCliente)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentasContabilidadCliente)
                    .IsRequired()
                    .HasColumnType("varchar(18)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.DiasDemoraEntregaCliente)
                    .HasColumnType("int(2)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.DinardapCliente)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'1'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.DireccionDosCliente)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.DireccionUnoCliente)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.EmailDespahosCliente)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.EmiteRetencionCliente)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasComment("Permite determinar si el cliente emite retenciones")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.EmpleadoEmpresaCliente)
                    .IsRequired()
                    .HasColumnType("varchar(2)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.EstadoCivilCliente)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'S'")
                    .HasComment("S SOLTERO C CASADO V VIUDO D DIVORCIADO U UNION LIBRE")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.EstatalCliente)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ExigirDocumentosCliente)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.FechaHastaCupoCalificacionCliente)
                    .HasColumnType("date")
                    .HasDefaultValueSql("'1900-01-01'");

                entity.Property(e => e.FechaNacimientoCliente)
                    .HasColumnType("date")
                    .HasDefaultValueSql("'1900-01-01'");

                entity.Property(e => e.FechaNacimientoConyugeCliente)
                    .HasColumnType("date")
                    .HasDefaultValueSql("'1900-01-01'");

                entity.Property(e => e.FechaRegistroCupoCalificacionCliente)
                    .HasColumnType("date")
                    .HasDefaultValueSql("'1900-01-01'");

                entity.Property(e => e.ListasPrecioCliente)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ListasPrecioMaximaCliente)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.MailCliente)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.MontoMaximoConsignacionCliente).HasColumnType("decimal(16,4)");

                entity.Property(e => e.MontoMinimoCreditoCliente).HasColumnType("double(16,4)");

                entity.Property(e => e.NombreCliente)
                    .IsRequired()
                    .HasColumnType("varchar(60)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NombreComercialCliente)
                    .IsRequired()
                    .HasColumnType("varchar(200)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NombreConyugeCliente)
                    .IsRequired()
                    .HasColumnType("varchar(60)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NumeroIdentificacionCliente)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ObservacionCliente)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.OmitirCupoCalificacionCliente)
                    .IsRequired()
                    .HasColumnType("varchar(5)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.OmitirDescuentoCliente)
                    .IsRequired()
                    .HasColumnType("varchar(5)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.PagaChequeCliente)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.PagaChequePosfechadoCliente)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.PrioridadNombreComercialCliente)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.PuedeTenerConsignacionCliente)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.RelacionCliente)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.RelacionadoEmpresaCliente)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasComment("indica si el cliente esta relacionado con la empresa.")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.RutasEntregasCliente)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.SectorDireccionCliente)
                    .IsRequired()
                    .HasColumnType("varchar(150)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.SexoCliente)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'M'")
                    .HasComment("M MASCULINO F FEMENINO")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.SolicitudesCreditoCliente)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TamanioNegociosCliente)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TelefonoCincoCliente)
                    .IsRequired()
                    .HasColumnType("varchar(12)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TelefonoCuatroCliente)
                    .IsRequired()
                    .HasColumnType("varchar(12)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TelefonoDosCliente)
                    .IsRequired()
                    .HasColumnType("varchar(15)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TelefonoSeisCliente)
                    .IsRequired()
                    .HasColumnType("varchar(12)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TelefonoTresCliente)
                    .IsRequired()
                    .HasColumnType("varchar(12)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TelefonoUnoCliente)
                    .IsRequired()
                    .HasColumnType("varchar(12)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TiposClienteCarteraCliente)
                    .IsRequired()
                    .HasColumnType("varchar(30)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TiposClienteCliente)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TiposIdentificacionCliente)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TiposNegocioCliente)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TiposNegocioSecundarioCliente)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TiposNegociosCalificacionCliente)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'1'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Tiposclientesegurocliente)
                    .IsRequired()
                    .HasColumnName("tiposclientesegurocliente")
                    .HasColumnType("varchar(30)")
                    .HasDefaultValueSql("'1'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TitulosCliente)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TransportesCliente)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.UsuariosCliente)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.UsuariosModificaCliente)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.WebCliente)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ZonasCliente)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.HasOne(d => d.TiposClienteCarteraClienteNavigation)
                    .WithMany(p => p.Clientes)
                    .HasForeignKey(d => d.TiposClienteCarteraCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TiposClienteCarteraCliente");

                entity.HasOne(d => d.TiposIdentificacionClienteNavigation)
                    .WithMany(p => p.Clientes)
                    .HasForeignKey(d => d.TiposIdentificacionCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TiposIdentificacion_Cliente");

                entity.HasOne(d => d.TransportesClienteNavigation)
                    .WithMany(p => p.Clientes)
                    .HasForeignKey(d => d.TransportesCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Transportes");
            });

            modelBuilder.Entity<Clienteslocalizaciones>(entity =>
            {
                entity.HasKey(e => e.ClientesClienteLocalizacion)
                    .HasName("PRIMARY");

                entity.ToTable("clienteslocalizaciones");

                entity.HasIndex(e => e.CantonesClienteLocalizacion)
                    .HasName("CantonesClienteLocalizacion");

                entity.HasIndex(e => e.PaisesClienteLocalizacion)
                    .HasName("PaisesClienteLocalizacion");

                entity.HasIndex(e => e.ParroquiasClienteLocalizacion)
                    .HasName("ParroquiasClienteLocalizacion");

                entity.HasIndex(e => e.ProvinciasClienteLocalizacion)
                    .HasName("ProvinciasClienteLocalizacion");

                entity.Property(e => e.ClientesClienteLocalizacion)
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CantonesClienteLocalizacion)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.PaisesClienteLocalizacion)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ParroquiasClienteLocalizacion)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ProvinciasClienteLocalizacion)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.HasOne(d => d.CantonesClienteLocalizacionNavigation)
                    .WithMany(p => p.Clienteslocalizaciones)
                    .HasForeignKey(d => d.CantonesClienteLocalizacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cantones_clientesLocalizaciones");

                entity.HasOne(d => d.ClientesClienteLocalizacionNavigation)
                    .WithOne(p => p.Clienteslocalizaciones)
                    .HasForeignKey<Clienteslocalizaciones>(d => d.ClientesClienteLocalizacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cliente_Localizaciones");

                entity.HasOne(d => d.PaisesClienteLocalizacionNavigation)
                    .WithMany(p => p.Clienteslocalizaciones)
                    .HasForeignKey(d => d.PaisesClienteLocalizacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Paises_clientesLocalizacion");

                entity.HasOne(d => d.ParroquiasClienteLocalizacionNavigation)
                    .WithMany(p => p.Clienteslocalizaciones)
                    .HasForeignKey(d => d.ParroquiasClienteLocalizacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Paroquias_clientesLocalizaciones");

                entity.HasOne(d => d.ProvinciasClienteLocalizacionNavigation)
                    .WithMany(p => p.Clienteslocalizaciones)
                    .HasForeignKey(d => d.ProvinciasClienteLocalizacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Provincias_clientesLocalizaciones");
            });

            modelBuilder.Entity<Clientesvendedores>(entity =>
            {
                entity.HasKey(e => new { e.ClientesClienteVendedor, e.VendedoresClienteVendedor, e.CobradoresClienteVendedor, e.EmpresasClienteVendedor })
                    .HasName("PRIMARY");

                entity.ToTable("clientesvendedores");

                entity.Property(e => e.ClientesClienteVendedor)
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.VendedoresClienteVendedor)
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CobradoresClienteVendedor)
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.EmpresasClienteVendedor)
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.EmpleadosClienteVendedor)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ListaPrecioCliente)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ListaPrecioMaximaCliente)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");
            });

            modelBuilder.Entity<Cuentascontabilidad>(entity =>
            {
                entity.HasKey(e => e.CodigoCuentaContable)
                    .HasName("PRIMARY");

                entity.ToTable("cuentascontabilidad");

                entity.HasComment("Adminitracion de cuentas contailidad;like NombreCuenta; Inno");

                entity.HasIndex(e => e.CodigoCuentaContable)
                    .HasName("CodigoCuentaContable");

                entity.HasIndex(e => e.NombreCuentaContable)
                    .HasName("Index_Nombre");

                entity.Property(e => e.CodigoCuentaContable)
                    .HasColumnType("varchar(18)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ConciliarCuentaContable)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NombreCuentaContable)
                    .IsRequired()
                    .HasColumnType("varchar(250)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.PermitirIngresarAsientoCuentaContable)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'1'")
                    .HasComment("valida si permite utilizar la cuenta en el ingreso de asientos. 1 permite, 0 no permite.")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TipoCuentaContable)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.UsuariosCuentaContabilidad)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");
            });

            modelBuilder.Entity<Docspendientes>(entity =>
            {
                entity.HasKey(e => e.CodigoDocPendiente)
                    .HasName("PRIMARY");

                entity.ToTable("docspendientes");

                entity.HasIndex(e => e.AtencionDocPendiente)
                    .HasName("AtencionDocPendiente");

                entity.HasIndex(e => e.CodigoDocPendiente)
                    .HasName("DocsPendientes")
                    .IsUnique();

                entity.HasIndex(e => e.CreacionDocPendiente)
                    .HasName("CreacionDocPendiente");

                entity.HasIndex(e => e.DocsCabeceraDocPendiente)
                    .HasName("Codigo");

                entity.HasIndex(e => e.PerfilesDocPendiente)
                    .HasName("PerfilesDocPendiente");

                entity.Property(e => e.CodigoDocPendiente)
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.AtencionDocPendiente).HasColumnType("datetime");

                entity.Property(e => e.CreacionDocPendiente).HasColumnType("datetime");

                entity.Property(e => e.DocsCabeceraDocPendiente)
                    .IsRequired()
                    .HasColumnType("varchar(36)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.EstadoDocPendiente)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.PerfilesDocPendiente)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.PerfilesMailBccdocPendiente)
                    .IsRequired()
                    .HasColumnName("PerfilesMailBCCDocPendiente")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TipoDocumentoDocPendiente)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TiposTramitesDocPendiente)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");
            });

            modelBuilder.Entity<Empleados>(entity =>
            {
                entity.HasKey(e => e.CodigoEmpleado)
                    .HasName("PRIMARY");

                entity.ToTable("empleados");

                entity.HasComment("Empleado;NombreEmpleado,ApellidoEmpleado");

                entity.HasIndex(e => e.ApellidoEmpleado)
                    .HasName("Index_Apellido");

                entity.HasIndex(e => e.CedulaEmpleado)
                    .HasName("Index_Cedula");

                entity.HasIndex(e => e.CodigoEmpleado)
                    .HasName("CodigoEmpleado");

                entity.HasIndex(e => e.NombreEmpleado)
                    .HasName("Index_Nombre");

                entity.HasIndex(e => e.NombreUsuarioEmpleado)
                    .HasName("Usuario")
                    .IsUnique();

                entity.HasIndex(e => e.PerfilesEmpleado)
                    .HasName("PerfilesEmpleado");

                entity.HasIndex(e => e.SucursalesEmpleado)
                    .HasName("sucursales_fk");

                entity.HasIndex(e => e.TipoIdentificacionReemplazaEmpleado)
                    .HasName("FK_TipoIdentificacionReemplaza_Empleado");

                entity.HasIndex(e => e.TiposEmpleadosEmpleado)
                    .HasName("FK_empleados_TipoEmpleado");

                entity.HasIndex(e => e.TiposIdentificacionEmpleado)
                    .HasName("FK_TipoIdentificacion_Empleado");

                entity.HasIndex(e => e.TitulosEmpleado)
                    .HasName("FK_empleados_Titulos");

                entity.HasIndex(e => new { e.NombreEmpleado, e.ApellidoEmpleado })
                    .HasName("NombreApellido")
                    .IsUnique();

                entity.Property(e => e.CodigoEmpleado)
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasComment("Codigo;text;true;false;Administracion;180;left")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.AdelantoQuincenalEmpleado)
                    .HasColumnType("double(16,4)")
                    .HasDefaultValueSql("'0.0000'");

                entity.Property(e => e.ApellidoEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("''")
                    .HasComment("Apellidos;text;true;true;Administracion;180;left")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.BonoAlimentacionEmpleado).HasColumnType("double(16,4)");

                entity.Property(e => e.BonoFijoEmpleado).HasColumnType("double(16,4)");

                entity.Property(e => e.BonoVariableEmpleado).HasColumnType("double(16,4)");

                entity.Property(e => e.CalcularFondosReservaEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CambiadoClaveEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("''")
                    .HasComment("0 No ha cambiado 1 Cambio clave")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CambiarPrimeraVezEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("''")
                    .HasComment("1 Cambiar 0 No Cambiar")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CargaFamiliarEmpleado)
                    .HasColumnType("int(11)")
                    .HasComment("Numero de Cargas Familiares;text;true;true;Datos Personales;180;left");

                entity.Property(e => e.CedulaEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(10)")
                    .HasDefaultValueSql("''")
                    .HasComment("Número de Cédula;text;true;true;Datos Personales;180;left")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CedulaRemplazoDiscapacidadEmplead)
                    .IsRequired()
                    .HasColumnType("varchar(10)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ClaveCaducaEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("''")
                    .HasComment("0 No Caduca 1 Si Caduca")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ClaveUsuarioEmpleado)
                    .IsRequired()
                    .HasColumnType("blob")
                    .HasComment("Clave Personal;txt;true;true;Administracion;180;left");

                entity.Property(e => e.ConvenioEvitarDobleImposicionEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(2)")
                    .HasDefaultValueSql("'NA'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CostaEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("''")
                    .HasComment("Determina si un empleado trabaja en la costa")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentaBancariaEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(30)")
                    .HasComment("numero de cta bancaria del empleado")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentasBancariasOrigenEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentasContabilidadAnticipoEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentasContabilidadAtrazoEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentasContabilidadBonoAlimenticio)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'0.00.00.000.000'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentasContabilidadBonoFijo)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'0.00.00.000.000'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentasContabilidadBonoVariable)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'0.00.00.000.000'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentasContabilidadComisionesEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'5.01.02.005.001'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentasContabilidadDesahucioEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentasContabilidadDespidoEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentasContabilidadEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(18)")
                    .HasDefaultValueSql("''")
                    .HasComment("Cuenta Contable Asignada;cmd;true;true;Contabilidad;180;left;Select CodigoCuentaContable, NombreCuentaContable from cuentascontabilidad")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentasContabilidadFondoReservaEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentasContabilidadGastoEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentasContabilidadHoraExtraordinariaEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentasContabilidadHoraJornadaNocturna)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentasContabilidadHoraSuplementariaEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentasContabilidadIessEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentasContabilidadIessGastoPatronalEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'5.01.02.002.002'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentasContabilidadIessPatronalEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentasContabilidadMultaEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentasContabilidadNominasEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'2.01.07.003.001'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentasContabilidadOtrosEgresosEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentasContabilidadOtrosIngresosEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentasContabilidadPrestamoEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentasContabilidadPrestamosIess)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'2.01.07.001.003'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentasContabilidadProvisionDecimoXiiiempleado)
                    .IsRequired()
                    .HasColumnName("CuentasContabilidadProvisionDecimoXIIIEmpleado")
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'2.01.07.001.001'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentasContabilidadProvisionDecimoXivempleado)
                    .IsRequired()
                    .HasColumnName("CuentasContabilidadProvisionDecimoXIVEmpleado")
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'2.01.07.001.002'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentasContabilidadProvisionFondoReservaEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'2.01.07.001.004'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentasContabilidadProvisionVacacionesEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'2.01.07.001.003'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentasContabilidadQuincena)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'1.01.02.010.160'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentasContabilidadRubrosEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentasContabilidadSaldoEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentasContabilidadVacacionEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentasContabilidadViaticoEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentasContabilidadXiiiEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentasContabilidadXivEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.DiasLaboraTiempoParcialEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(3)")
                    .HasDefaultValueSql("'0'")
                    .HasComment("indica los dias que el empleado labora si trabaja a tiempo parcial.")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.DireccionDosEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''")
                    .HasComment("Direccion Adicional;text;true;true;Datos Personales;180;left")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.DireccionUnoEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''")
                    .HasComment("Domicilio;text;true;true;Datos Personales;180;left")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.DiscapacidadEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(2)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.DuracionContratoEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(3)")
                    .HasComment("numero de meses que dura el contrato del empleado con la empresa")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.EmailEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''")
                    .HasComment("Email Oficina;text;true;true;Administracion;180;left")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.EmailEmpleadoPersonal)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("'NA'")
                    .HasComment("Email Personal;text;true;true;Datos Personales;180;left")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.EmpresasEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.EntregarDecimoEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasComment("indica si el empleado quiere recibir el decimo en el salario mensual. 1 mensual, 0 no mensual.")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.EntregarDecimoIvempleado)
                    .IsRequired()
                    .HasColumnName("EntregarDecimoIVEmpleado")
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.EspecialidadesMedicasEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.EstadosCivilesPersonasEmpleados)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.FechaCaducaClaveEmpleado)
                    .HasColumnType("date")
                    .HasDefaultValueSql("'1900-01-01'");

                entity.Property(e => e.FechaCaducaUsuarioEmpleado)
                    .HasColumnType("date")
                    .HasDefaultValueSql("'1900-01-01'");

                entity.Property(e => e.FechaCalculoVacacionesEmpleado)
                    .HasColumnType("date")
                    .HasDefaultValueSql("'1900-01-01'");

                entity.Property(e => e.FechaNacimientoEmpleado)
                    .HasColumnType("date")
                    .HasDefaultValueSql("'1900-01-01'")
                    .HasComment("Fecha de Nacimiento;cal;true;true;Datos Personales;180;left");

                entity.Property(e => e.FondosReservaEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("''")
                    .HasComment("Determina si a un empleado se le entrega directamente los fondo de reserva")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.FormularioInicialEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("'frmmarcacion.aspx'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.GeneroEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.HoraExtraEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.HoraIngresoEmpleado)
                    .HasColumnType("time")
                    .HasDefaultValueSql("'08:30:00'")
                    .HasComment("Almacena la hora de ingreso en la mañana");

                entity.Property(e => e.HoraSalidaEmpleado)
                    .HasColumnType("time")
                    .HasDefaultValueSql("'18:00:00'")
                    .HasComment("Almacena la hora de la salida en la tarde");

                entity.Property(e => e.IesscodigosSectorialesEmpleado)
                    .IsRequired()
                    .HasColumnName("IESSCodigosSectorialesEmpleado")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ImagenEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("'General/Avatar.svg'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.LetraCambioEmpleado).HasColumnType("double(16,2)");

                entity.Property(e => e.MarcarEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NoCalculaBeneficiosEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasComment("indica si el empleado calculo beneficios sociales")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NombreEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(150)")
                    .HasDefaultValueSql("''")
                    .HasComment("Nombres;text;true;true;Administracion;180;left")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NombreUsuarioEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasComment("Nombre de Usuario;text;true;true;Administracion;180;left")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NumeroCarnetConadisEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NumeroDiasFinSemanaVacacionTomadosEmpleado).HasColumnType("int(3)");

                entity.Property(e => e.NumeroDiasHabilesVacacionTomadosEmpleado).HasColumnType("int(3)");

                entity.Property(e => e.PaisResidenciaEmpleado)
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'593'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.PerfilesEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasComment("Perfil de Usuario;cmb;true;true;Administracion;180;left;select CodigoPerfil, NombrePerfil from perfiles")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.PermiteAgendamientoEmpleados)
                    .HasColumnType("int(1)")
                    .HasComment("Parámetro que sirve para filtrar los empleados en el módulo de agendamiento");

                entity.Property(e => e.PlantillaCuentasEmpleados)
                    .IsRequired()
                    .HasColumnType("varchar(36)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.PorcentajeAnticipoEmpleado)
                    .HasColumnType("double(16,4)")
                    .HasDefaultValueSql("'70.0000'");

                entity.Property(e => e.PorcentajeDiscapacidadEmpleado).HasColumnType("decimal(10,2)");

                entity.Property(e => e.PorcentajeIessEmpleados)
                    .HasColumnType("decimal(8,2)")
                    .HasDefaultValueSql("'9.45'");

                entity.Property(e => e.PorcentajeIessPatronoEmpleados)
                    .HasColumnType("decimal(16,2)")
                    .HasDefaultValueSql("'12.15'");

                entity.Property(e => e.PresupuestarEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasComment("indica si al empleado se le debe crear un presupuesto de ventas.")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.RepresentanteLegalEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasComment("indica si el empleado es el representante legal de la empresa")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.RolPagoEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.SeleccionaEmpresaEmpleado)
                    .HasColumnType("int(5)")
                    .HasDefaultValueSql("'1'")
                    .HasComment("Debe Elegir Empresa?;chk;true;true;Administracion;180;left");

                entity.Property(e => e.SeleccionaSucursalEmpleado)
                    .HasColumnType("int(5)")
                    .HasDefaultValueSql("'1'")
                    .HasComment("Debe elegir Sucursal?;chk;true;true;Administracion;180;left");

                entity.Property(e => e.SistemaSalarioNetoEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'1'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.SucursalesEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasComment("Sucursal de Pertenencia;cmb;true;true;Administracion;180;left;select CodigoSucursal, NombreSucursal from sucursales")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.SueldoNominalEmpleado)
                    .HasColumnType("double(16,4)")
                    .HasComment("Sueldo Nominal;text;true;true;Contabilidad;180;left");

                entity.Property(e => e.SustitutoEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasComment("indii el empleado es empleado sustituto de un miembro de la familia discapacitado")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TelefonoDosEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(12)")
                    .HasDefaultValueSql("''")
                    .HasComment("Teléfono Adicional;text;true;true;Datos Personales;180;left")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TelefonoTresEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(12)")
                    .HasDefaultValueSql("''")
                    .HasComment("Celular;text;true;true;Datos Personales;180;left")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TelefonoUnoEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(12)")
                    .HasDefaultValueSql("''")
                    .HasComment("Teléfono;text;true;true;Datos Personales;180;left")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TiempoAlmuerzoEmpleado)
                    .HasColumnType("int(3)")
                    .HasDefaultValueSql("'90'")
                    .HasComment("Almacena el numero de minutos de tiempo de almuerzo del empleado");

                entity.Property(e => e.TipoCuentaBancariaEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(2)")
                    .HasComment("indica el tipo de cta bancaria del empleado")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TipoIdentificacionReemplazaEmpleado)
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TipoResidenciaEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasDefaultValueSql("'01'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TipoSincronizacionEmpleado)
                    .IsRequired()
                    .HasColumnType("char(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TiposEmpleadosEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasComment("Cargo de Empleado;cmb;true;true;Administracion;180;left;select CodigoTipoEmpleado, NombreTipoEmpleado from tiposempleados")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TiposIdentificacionEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'1'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TiposRubroEmpleados)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TiposSangrePersonasEmpleados)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TitulosEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasComment("Titulo - Profesion;cmb;true;true;Administracion;180;left;select CodigoTitulo, NombreTitulo from titulos")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TrabajaConstruccionEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TrabajaTiempoParcialEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasComment("indica si el empleado trabaja a tiempo parcial, 1 parcial, 0 completo")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.UsuarioCaducaEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasComment("0 No Caduca 1 Caduca")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.UsuariosEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'fmaldonado'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ValorFondoReservaEmpleado).HasColumnType("double(16,4)");

                entity.Property(e => e.VerCostoEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'1'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.WebUsuarioEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.HasOne(d => d.PerfilesEmpleadoNavigation)
                    .WithMany(p => p.Empleados)
                    .HasForeignKey(d => d.PerfilesEmpleado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("empleados_ibfk_1");

                entity.HasOne(d => d.SucursalesEmpleadoNavigation)
                    .WithMany(p => p.Empleados)
                    .HasForeignKey(d => d.SucursalesEmpleado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("sucursales_fk");

                entity.HasOne(d => d.TipoIdentificacionReemplazaEmpleadoNavigation)
                    .WithMany(p => p.EmpleadosTipoIdentificacionReemplazaEmpleadoNavigation)
                    .HasForeignKey(d => d.TipoIdentificacionReemplazaEmpleado)
                    .HasConstraintName("FK_TipoIdentificacionReemplaza_Empleado");

                entity.HasOne(d => d.TiposEmpleadosEmpleadoNavigation)
                    .WithMany(p => p.Empleados)
                    .HasForeignKey(d => d.TiposEmpleadosEmpleado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TiposEmpleados_Empleado");

                entity.HasOne(d => d.TiposIdentificacionEmpleadoNavigation)
                    .WithMany(p => p.EmpleadosTiposIdentificacionEmpleadoNavigation)
                    .HasForeignKey(d => d.TiposIdentificacionEmpleado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TipoIdentificacion_Empleado");

                entity.HasOne(d => d.TitulosEmpleadoNavigation)
                    .WithMany(p => p.Empleados)
                    .HasForeignKey(d => d.TitulosEmpleado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("empleados_ibfk_2");
            });

            modelBuilder.Entity<Empleadosatiendecallcenter>(entity =>
            {
                entity.HasKey(e => new { e.Tiposdocumentoempleadoatiendecallcenter, e.Empleadoempleadoatiendecallcenter, e.Categoriasdocumentosinstalacionesempleadoatiendecallcenter })
                    .HasName("PRIMARY");

                entity.ToTable("empleadosatiendecallcenter");

                entity.HasIndex(e => e.Categoriasdocumentosinstalacionesempleadoatiendecallcenter)
                    .HasName("categoriasdocumentosinstalacionesempleadoatiendecallcenter");

                entity.HasIndex(e => e.Empleadoempleadoatiendecallcenter)
                    .HasName("perfilempleadoatiendecallcenter");

                entity.HasIndex(e => e.Tiposdocumentoempleadoatiendecallcenter)
                    .HasName("tiposdocumentoempleadoatiendecallcenter");

                entity.Property(e => e.Tiposdocumentoempleadoatiendecallcenter)
                    .HasColumnName("tiposdocumentoempleadoatiendecallcenter")
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Empleadoempleadoatiendecallcenter)
                    .HasColumnName("empleadoempleadoatiendecallcenter")
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Categoriasdocumentosinstalacionesempleadoatiendecallcenter)
                    .HasColumnName("categoriasdocumentosinstalacionesempleadoatiendecallcenter")
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.HasOne(d => d.CategoriasdocumentosinstalacionesempleadoatiendecallcenterNavigation)
                    .WithMany(p => p.Empleadosatiendecallcenter)
                    .HasForeignKey(d => d.Categoriasdocumentosinstalacionesempleadoatiendecallcenter)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CategoriaDocumentoInstalacion_atiendeCallcenter");

                entity.HasOne(d => d.EmpleadoempleadoatiendecallcenterNavigation)
                    .WithMany(p => p.Empleadosatiendecallcenter)
                    .HasForeignKey(d => d.Empleadoempleadoatiendecallcenter)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_empleado_atiendeCallcenter");

                entity.HasOne(d => d.TiposdocumentoempleadoatiendecallcenterNavigation)
                    .WithMany(p => p.Empleadosatiendecallcenter)
                    .HasForeignKey(d => d.Tiposdocumentoempleadoatiendecallcenter)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TipoDocumentoInstalacion_atiendeCallcenter");
            });

            modelBuilder.Entity<Empresas>(entity =>
            {
                entity.HasKey(e => e.CodigoEmpresa)
                    .HasName("PRIMARY");

                entity.ToTable("empresas");

                entity.HasComment("Empresa;NombreEmpresa");

                entity.HasIndex(e => e.CodigoEmpresa)
                    .HasName("CodigoEmpresa");

                entity.HasIndex(e => e.NombreComercialEmpresa)
                    .HasName("NombreComercialEmpresa_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.NombreEmpresa)
                    .HasName("Index_Nombre")
                    .IsUnique();

                entity.HasIndex(e => e.RucEmpresa)
                    .HasName("Index_Ruc")
                    .IsUnique();

                entity.Property(e => e.CodigoEmpresa)
                    .HasColumnType("varchar(20)")
                    .HasComment("Codigo de Empresa;text;true;false;Datos;120;left")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CodigoDinardapEmpresa)
                    .IsRequired()
                    .HasColumnType("varchar(7)")
                    .HasDefaultValueSql("'0000000'")
                    .HasComment("Codigo DINARDAP Empresa;text;true;True;Datos;200;left")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.LogoEmpresa)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasDefaultValueSql("'wise.png'")
                    .HasComment("Logotipo Empresa;text;true;True;Datos;200;left")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NombreComercialEmpresa)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("''")
                    .HasComment("Nombre Comercial Empresa;text;true;True;Datos;200;left")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NombreEmpresa)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasComment("Nombre Empresa;text;true;True;Datos;200;left")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NombreEmpresaImpresion)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.OrdenIncialEmpresa)
                    .HasColumnType("int(1)")
                    .HasDefaultValueSql("'1'")
                    .HasComment("Orden a presentar empresa;text;true;True;Datos;200;left");

                entity.Property(e => e.RucEmpresa)
                    .IsRequired()
                    .HasColumnType("varchar(13)")
                    .HasComment("Ruc Empresa;text;true;true;Datos;180;left")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.UsuariosEmpresa)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.VisualizarNombreComercialEmpresa)
                    .HasColumnType("int(1)")
                    .HasComment("Si está en 0 se visualiza la razón social de la empresa");
            });

            modelBuilder.Entity<Empresasclientes>(entity =>
            {
                entity.HasKey(e => new { e.EmpresasEmpresaCliente, e.ClientesEmpresaCliente })
                    .HasName("PRIMARY");

                entity.ToTable("empresasclientes");

                entity.HasIndex(e => e.ClientesEmpresaCliente)
                    .HasName("FK_empresasclientes_Cliente");

                entity.Property(e => e.EmpresasEmpresaCliente)
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ClientesEmpresaCliente)
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.BodegasEmpresaCliente)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.OmitirCupoEmpresaCliente)
                    .IsRequired()
                    .HasColumnType("varchar(2)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.OmitirDescuentoEmpresaCliente)
                    .IsRequired()
                    .HasColumnType("varchar(2)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.HasOne(d => d.ClientesEmpresaClienteNavigation)
                    .WithMany(p => p.Empresasclientes)
                    .HasForeignKey(d => d.ClientesEmpresaCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cliente_EmpresasCliente");

                entity.HasOne(d => d.EmpresasEmpresaClienteNavigation)
                    .WithMany(p => p.Empresasclientes)
                    .HasForeignKey(d => d.EmpresasEmpresaCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Empresa_EmpresasCliente");
            });

            modelBuilder.Entity<Formareclamo>(entity =>
            {
                entity.HasKey(e => e.CodigoFormaReclamo)
                    .HasName("PRIMARY");

                entity.ToTable("formareclamo");

                entity.Property(e => e.CodigoFormaReclamo)
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.DescripcionFormaReclamo)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.UsuarioReclamoFormaReclamo)
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");
            });

            modelBuilder.Entity<Fuentesremision>(entity =>
            {
                entity.HasKey(e => e.CodigoFuenteRemision)
                    .HasName("PRIMARY");

                entity.ToTable("fuentesremision");

                entity.HasIndex(e => e.CodigoFuenteRemision)
                    .HasName("CodigoFuenteRemision");

                entity.Property(e => e.CodigoFuenteRemision)
                    .HasColumnType("varchar(20)")
                    .HasComment("Codigo;text;true;false;Datos;180;left")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.DescripcionFuenteRemision)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasComment("Descripcion;text;true;true;Datos;180;left")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.UsuariosFuenteRemision)
                    .IsRequired()
                    .HasColumnName("usuariosFuenteRemision")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");
            });

            modelBuilder.Entity<Instalacionescabecera>(entity =>
            {
                entity.HasKey(e => e.CodigoInstalacionesCabecera)
                    .HasName("PRIMARY");

                entity.ToTable("instalacionescabecera");

                entity.HasIndex(e => e.BodegaOrigenInstalacionesCabecera)
                    .HasName("BodegaOrigenInstalacionesCabecera");

                entity.HasIndex(e => e.CategoriasFinalizacionInstalacionesCabecera)
                    .HasName("CategoriasFinalizacionInstalacionesCabecera");

                entity.HasIndex(e => e.CategoriasTiposDocumentosInstalacionesCabecera)
                    .HasName("CategoriasTipoFK");

                entity.HasIndex(e => e.ClienteInstalacionesCabecera)
                    .HasName("ClienteInstalacionesCabecera");

                entity.HasIndex(e => e.CodigoInstalacionesCabecera)
                    .HasName("CodigoInstalacionesCabecera");

                entity.HasIndex(e => e.EmpleadoInstalacionesCabecera)
                    .HasName("EmpleadoAsignado");

                entity.HasIndex(e => e.EstadoInstalacionInstalacionesCabecera)
                    .HasName("TiposFinalizacion");

                entity.HasIndex(e => e.NivelesPrioridadProcesosInstalacionesCabecera)
                    .HasName("NivelesPrioridadProcesosFK");

                entity.HasIndex(e => e.OrdenInstalacionInstalacionesCabecera)
                    .HasName("OrdenInstalacionInstalacionesCabecera");

                entity.HasIndex(e => e.TiposDocumentoInstalacionesCabecera)
                    .HasName("TiposDocumentoFK");

                entity.Property(e => e.CodigoInstalacionesCabecera)
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.BodegaOrigenInstalacionesCabecera)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CategoriasFinalizacionInstalacionesCabecera)
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CategoriasTiposDocumentosInstalacionesCabecera)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ClienteInstalacionesCabecera)
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CodigoPadreInstalacionesCabecera)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.DescripcionInstalacionesCabecera)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.DocumentoOrigenInstalacionesCabecera)
                    .IsRequired()
                    .HasColumnType("varchar(36)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.EmpleadoInstalacionesCabecera)
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.EmpresaInstalacionesCabecera)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.EstadoAsignacionInstalacionesCabecera)
                    .IsRequired()
                    .HasColumnType("char(1)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.EstadoAutorizacionInstalacionesCabecera)
                    .IsRequired()
                    .HasColumnType("char(1)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.EstadoInstalacionInstalacionesCabecera)
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.FechaAnulacionInstalacionesCabecera).HasColumnType("datetime");

                entity.Property(e => e.FechaAsignacionInstalacionesCabecera)
                    .HasColumnType("datetime")
                    .HasComment("Fecha de Asignacion a empleado");

                entity.Property(e => e.FechaInstalacionInstalacionesCabecera).HasColumnType("datetime");

                entity.Property(e => e.FechaInstalacionesCabecera)
                    .HasColumnType("date")
                    .HasDefaultValueSql("'2017-08-21'");

                entity.Property(e => e.FechaRegistroAsignacionInstalacionesCabecera).HasColumnType("datetime");

                entity.Property(e => e.FinalizadoMatrizInstalacionesCabecera).HasColumnType("int(1)");

                entity.Property(e => e.LatitudInstalacionesCabecera)
                    .IsRequired()
                    .HasColumnType("varchar(25)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.LocalInstalacionesCabecera)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.LongitudInstalacionesCabecera)
                    .IsRequired()
                    .HasColumnType("varchar(25)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NivelesPrioridadProcesosInstalacionesCabecera)
                    .HasColumnType("int(1)")
                    .HasComment(@"0: Baja
1: Media
2: Alta");

                entity.Property(e => e.NumeroSecuencialInstalacionesCabecera)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ObservacionInstalacionesCabecera)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.OrdenInstalacionInstalacionesCabecera)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.SubTotal0InstalacionesCabecera).HasColumnType("double(16,4)");

                entity.Property(e => e.SubTotalInstalacionesCabecera).HasColumnType("double(16,4)");

                entity.Property(e => e.SucursalInstalacionesCabecera)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TiposDocumentoInstalacionesCabecera)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'1'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.UsuarioAnulaInstalacionesCabecera)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.UsuarioAsignaInstalacionesCabecera)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.UsuarioInstalacionInstalacionesCabecera)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.HasOne(d => d.CategoriasFinalizacionInstalacionesCabeceraNavigation)
                    .WithMany(p => p.Instalacionescabecera)
                    .HasForeignKey(d => d.CategoriasFinalizacionInstalacionesCabecera)
                    .HasConstraintName("CategoriasFinalizacionInstalacionesCabecera");

                entity.HasOne(d => d.CategoriasTiposDocumentosInstalacionesCabeceraNavigation)
                    .WithMany(p => p.Instalacionescabecera)
                    .HasForeignKey(d => d.CategoriasTiposDocumentosInstalacionesCabecera)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CategoriasTipoFK");

                entity.HasOne(d => d.ClienteInstalacionesCabeceraNavigation)
                    .WithMany(p => p.Instalacionescabecera)
                    .HasForeignKey(d => d.ClienteInstalacionesCabecera)
                    .HasConstraintName("FK_Clientes_InstalacionCabecera");

                entity.HasOne(d => d.EmpleadoInstalacionesCabeceraNavigation)
                    .WithMany(p => p.Instalacionescabecera)
                    .HasForeignKey(d => d.EmpleadoInstalacionesCabecera)
                    .HasConstraintName("EmpleadoAsignado");

                entity.HasOne(d => d.EstadoInstalacionInstalacionesCabeceraNavigation)
                    .WithMany(p => p.Instalacionescabecera)
                    .HasForeignKey(d => d.EstadoInstalacionInstalacionesCabecera)
                    .HasConstraintName("TiposFinalizacion");

                entity.HasOne(d => d.NivelesPrioridadProcesosInstalacionesCabeceraNavigation)
                    .WithMany(p => p.Instalacionescabecera)
                    .HasForeignKey(d => d.NivelesPrioridadProcesosInstalacionesCabecera)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("NivelesPrioridadProcesosFK");

                entity.HasOne(d => d.OrdenInstalacionInstalacionesCabeceraNavigation)
                    .WithMany(p => p.Instalacionescabecera)
                    .HasForeignKey(d => d.OrdenInstalacionInstalacionesCabecera)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("OrdenOrigenFK");

                entity.HasOne(d => d.TiposDocumentoInstalacionesCabeceraNavigation)
                    .WithMany(p => p.Instalacionescabecera)
                    .HasForeignKey(d => d.TiposDocumentoInstalacionesCabecera)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("TiposDocumentoFK");
            });

            modelBuilder.Entity<Listasprecios>(entity =>
            {
                entity.HasKey(e => e.CodigoListaPrecio)
                    .HasName("PRIMARY");

                entity.ToTable("listasprecios");

                entity.HasComment("ListaPrecio;NombreListaPrecio");

                entity.HasIndex(e => e.CodigoListaPrecio)
                    .HasName("CodigoListaPrecio");

                entity.HasIndex(e => e.NombreListaPrecio)
                    .HasName("Index_Nombre")
                    .IsUnique();

                entity.Property(e => e.CodigoListaPrecio)
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasComment("Codigo Precio;text;true;false;Datos;180;left")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.AplicarListaPrecio)
                    .IsRequired()
                    .HasColumnType("varchar(5)")
                    .HasDefaultValueSql("'P'")
                    .HasComment("Aplica a;optbuton;true;true;Datos;180;left;;Costo,Precio")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ClienteListaPrecio)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasComment("Cliente Lista de Precio;chkbouton;true;true;Datos;180;left")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.IngresarCompraListaPrecio)
                    .HasColumnType("tinyint(3)")
                    .HasComment("Digita Precio;chkbouton;true;true;Datos;180;left");

                entity.Property(e => e.ListasPrecioProductoListaPrecio)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasComment("Lista Producto;combo;true;true;Datos;180;left;select CodigoListaPrecio,NombreListaPrecio from ListasPrecios")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NombreListaPrecio)
                    .IsRequired()
                    .HasColumnType("varchar(60)")
                    .HasDefaultValueSql("''")
                    .HasComment("Nombre Precio;text;true;true;Datos;180;left")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.PorcentajeListaPrecio)
                    .HasColumnType("double(16,4)")
                    .HasComment("Porcentaje Precio;text;true;true;Datos;180;left");

                entity.Property(e => e.SeleccionarVentaListaPrecio)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasComment("Permitido seleccionar en venta;chkSeleccionarVenta;true;true;Datos;180;left")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.UsuariosListaPrecio)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");
            });

            modelBuilder.Entity<Localizacionescantones>(entity =>
            {
                entity.HasKey(e => e.CodigoLocalizacionCanton)
                    .HasName("PRIMARY");

                entity.ToTable("localizacionescantones");

                entity.HasComment("Ciudad;NombreLocalizacionCiudad");

                entity.HasIndex(e => e.AbreviadoLocalizacionCanton)
                    .HasName("Index_Abreviado")
                    .IsUnique();

                entity.HasIndex(e => e.NombreLocalizacionCanton)
                    .HasName("Index_NombreCiudad");

                entity.HasIndex(e => e.ProvinciasLocalizacionCanton)
                    .HasName("ProvinciasLocalizacionCanton");

                entity.Property(e => e.CodigoLocalizacionCanton)
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasComment("Codigo Ciudad;text;true;false;Datos;80;left")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.AbreviadoLocalizacionCanton)
                    .IsRequired()
                    .HasColumnType("varchar(6)")
                    .HasDefaultValueSql("''")
                    .HasComment("Nombre Abreviado;text;true;true;Datos;180;left")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CodigoInecLocalizacionCanton)
                    .IsRequired()
                    .HasColumnType("varchar(3)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NombreLocalizacionCanton)
                    .IsRequired()
                    .HasColumnType("varchar(60)")
                    .HasDefaultValueSql("''")
                    .HasComment("Nombre Ciudad;text;true;true;Datos;180;left")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ProvinciasLocalizacionCanton)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasComment("Provincia Ciudad;combo;true;true;Datos;180;left; select CodigoLocalizacionProvincia, NombreLocalizacionProvincia from localizacionesprovincias")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.UsuariosLocalizacionCanton)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.HasOne(d => d.ProvinciasLocalizacionCantonNavigation)
                    .WithMany(p => p.Localizacionescantones)
                    .HasForeignKey(d => d.ProvinciasLocalizacionCanton)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LocalizacionCanton_Provincia");
            });

            modelBuilder.Entity<Localizacionespaises>(entity =>
            {
                entity.HasKey(e => e.CodigoLocalizacionPais)
                    .HasName("PRIMARY");

                entity.ToTable("localizacionespaises");

                entity.HasComment("Pais;NombreLocalizacionPais,AbreviadoLocalizacionPais");

                entity.HasIndex(e => e.AbreviadoLocalizacionPais)
                    .HasName("Index_AbreviadoPais")
                    .IsUnique();

                entity.HasIndex(e => e.NombreLocalizacionPais)
                    .HasName("Index_NombrePais")
                    .IsUnique();

                entity.Property(e => e.CodigoLocalizacionPais)
                    .HasColumnType("varchar(20)")
                    .HasComment("Codigo Pais;text;true;false;Datos;80;Left")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.AbreviadoLocalizacionPais)
                    .IsRequired()
                    .HasColumnType("varchar(6)")
                    .HasComment("Nombre Abreviado;text;true;true;Datos;180;left")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CodigoAccesoLocalizacionPais)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasComment("Codigo Acceso Pais;text;true;false;Datos;80;Left")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NombreLocalizacionPais)
                    .IsRequired()
                    .HasColumnType("varchar(60)")
                    .HasComment("Nombre Pais;text;true;true;Datos;180;left")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.UsuariosLocalizacionPaises)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");
            });

            modelBuilder.Entity<Localizacionesparroquias>(entity =>
            {
                entity.HasKey(e => e.CodigoLocalizacionParroquia)
                    .HasName("PRIMARY");

                entity.ToTable("localizacionesparroquias");

                entity.HasComment("Ciudad;NombreLocalizacionParroquia");

                entity.HasIndex(e => e.LocalizacionesCantonesLocalizacionParroquia)
                    .HasName("LocalizacionesCantonesLocalizacionParroquia");

                entity.HasIndex(e => e.NombreLocalizacionParroquia)
                    .HasName("Index_NombreParroquia");

                entity.Property(e => e.CodigoLocalizacionParroquia)
                    .HasColumnType("varchar(20)")
                    .HasComment("Codigo Ciudad;text;true;false;Datos;80;left")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CodigoInecLocalizacionParroquia)
                    .IsRequired()
                    .HasColumnType("varchar(10)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.LocalizacionesCantonesLocalizacionParroquia)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasComment("Canton Parroquia;combo;true;true;Datos;180;left; select CodigoLocalizacionProvincia, NombreLocalizacionProvincia from localizacionesprovincias")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NombreLocalizacionParroquia)
                    .IsRequired()
                    .HasColumnType("varchar(150)")
                    .HasDefaultValueSql("''")
                    .HasComment("Nombre Ciudad;text;true;true;Datos;180;left")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.UsuariosLocalizacionParroquia)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.HasOne(d => d.LocalizacionesCantonesLocalizacionParroquiaNavigation)
                    .WithMany(p => p.Localizacionesparroquias)
                    .HasForeignKey(d => d.LocalizacionesCantonesLocalizacionParroquia)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LocalizacionParroquia_Canton");
            });

            modelBuilder.Entity<Localizacionesprovincias>(entity =>
            {
                entity.HasKey(e => e.CodigoLocalizacionProvincia)
                    .HasName("PRIMARY");

                entity.ToTable("localizacionesprovincias");

                entity.HasComment("Provincia;NombreLocalizacionProvincia");

                entity.HasIndex(e => e.AbreviadoLocalizacionProvincia)
                    .HasName("Index_Abreviado")
                    .IsUnique();

                entity.HasIndex(e => e.NombreLocalizacionProvincia)
                    .HasName("Index_NombreProvincia")
                    .IsUnique();

                entity.HasIndex(e => e.PaisesLocalizacionProvincia)
                    .HasName("Paises");

                entity.Property(e => e.CodigoLocalizacionProvincia)
                    .HasColumnType("varchar(20)")
                    .HasComment("Codigo Provincia;text;true;false;Datos;80;left")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.AbreviadoLocalizacionProvincia)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasComment("Nombre Abreviado;text;true;true;Datos;120;left")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CodigoInecLocalizacionProvincia)
                    .IsRequired()
                    .HasColumnType("varchar(2)")
                    .HasDefaultValueSql("''")
                    .HasComment("Codigo Registro Provincia;text;true;true;Datos;80;left")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CodigoMatriculacionLocalizacionProvincia)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NombreLocalizacionProvincia)
                    .IsRequired()
                    .HasColumnType("varchar(60)")
                    .HasComment("Nombre Provincia;text;true;true;Datos;120;left")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.PaisesLocalizacionProvincia)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.UsuariosLocalizacionProvincia)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.HasOne(d => d.PaisesLocalizacionProvinciaNavigation)
                    .WithMany(p => p.Localizacionesprovincias)
                    .HasForeignKey(d => d.PaisesLocalizacionProvincia)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LocalizacionProvincia_Pais");
            });

            modelBuilder.Entity<Movimientocampanias>(entity =>
            {
                entity.HasKey(e => e.CodigoMovCamp)
                    .HasName("PRIMARY");

                entity.ToTable("movimientocampanias");

                entity.HasIndex(e => e.SubCampaniasMovCamp)
                    .HasName("MovimientoCampanias_fk0");

                entity.Property(e => e.CodigoMovCamp).HasColumnType("int(1)");

                entity.Property(e => e.FechaRegistroMovCamp).HasColumnType("datetime");

                entity.Property(e => e.IngresoDesdePublicidadMovCamp).HasColumnType("int(1)");

                entity.Property(e => e.RegistroCitaMovCamp).HasColumnType("int(1)");

                entity.Property(e => e.SesionMovCamp)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.SubCampaniasMovCamp)
                    .IsRequired()
                    .HasColumnType("varchar(22)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.HasOne(d => d.SubCampaniasMovCampNavigation)
                    .WithMany(p => p.Movimientocampanias)
                    .HasForeignKey(d => d.SubCampaniasMovCamp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("MovimientoCampanias_fk0");
            });

            modelBuilder.Entity<Nivelesprioridadprocesos>(entity =>
            {
                entity.HasKey(e => e.CodigoNivelPrioridadProcesos)
                    .HasName("PRIMARY");

                entity.ToTable("nivelesprioridadprocesos");

                entity.Property(e => e.CodigoNivelPrioridadProcesos).HasColumnType("int(1)");

                entity.Property(e => e.DescripcionNivelPrioridadProcesos)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");
            });

            modelBuilder.Entity<Observacionesempleadosinstalaciones>(entity =>
            {
                entity.HasKey(e => e.CodigoObservacionEmpleadoInstalacion)
                    .HasName("PRIMARY");

                entity.ToTable("observacionesempleadosinstalaciones");

                entity.HasIndex(e => e.InstalacionesCabeceraObservacionEmpleadoInstalacion)
                    .HasName("FK_InstalacionCabecera_ObservacionInstalacion");

                entity.Property(e => e.CodigoObservacionEmpleadoInstalacion)
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.EmpleadosObservacionEmpleadosInstalacion)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.EquiposEmpleadoObservacionEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.FechaObservacionEmpleado)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'1900-01-01 00:00:00'");

                entity.Property(e => e.InstalacionesCabeceraObservacionEmpleadoInstalacion)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ObservacionObservacionEmpleadoInstalacion)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.OrigenesObservacionesObservacionEmpleadosInstalacion)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'0'")
                    .HasComment(@"0: CALL CENTER
1: ASIGNACION PROCESOS
OTRO: CODIGO DEL EQUIPO")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.HasOne(d => d.InstalacionesCabeceraObservacionEmpleadoInstalacionNavigation)
                    .WithMany(p => p.Observacionesempleadosinstalaciones)
                    .HasForeignKey(d => d.InstalacionesCabeceraObservacionEmpleadoInstalacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InstalacionCabecera_ObservacionInstalacion");
            });

            modelBuilder.Entity<Ordeninstalacion>(entity =>
            {
                entity.HasKey(e => e.CodigoOrdenInstalacion)
                    .HasName("PRIMARY");

                entity.ToTable("ordeninstalacion");

                entity.HasIndex(e => e.CategoriaArcotelOrdenInstalacion)
                    .HasName("FK_CategoriaArcotel_OrdenInstalacion");

                entity.HasIndex(e => e.ClienteOrdenInstalacion)
                    .HasName("FK_Cliente_OrdenInstalacion");

                entity.HasIndex(e => e.CodigoOrdenInstalacion)
                    .HasName("CodigoOrdenInstalacion");

                entity.HasIndex(e => e.ContratoCabeceraOrdenInstalacion)
                    .HasName("ContratoCabeceraOrdenInstalacion");

                entity.HasIndex(e => e.EmpleadoRegistroOrdenInstalacion)
                    .HasName("EmpleadoRegistraFK");

                entity.HasIndex(e => e.EmpresaOrdenInstalacion)
                    .HasName("FK_Empresa_OrdenInstalacion");

                entity.HasIndex(e => e.FormaReclamoOrdenInstalacion)
                    .HasName("FK_FormaReclamo_OrdenInstalacion");

                entity.HasIndex(e => e.SucursalOrdenInstalacion)
                    .HasName("FK_Sucursal_OrdenInstalacion");

                entity.Property(e => e.CodigoOrdenInstalacion)
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CategoriaArcotelOrdenInstalacion)
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ClienteOrdenInstalacion)
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ContratoCabeceraOrdenInstalacion)
                    .IsRequired()
                    .HasColumnType("varchar(36)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.EmpleadoRegistroOrdenInstalacion)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.EmpresaOrdenInstalacion)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.EstadoOrdenInstalacion).HasColumnType("int(1)");

                entity.Property(e => e.FechaRegistroOrdenInstalacion).HasColumnType("datetime");

                entity.Property(e => e.FormaReclamoOrdenInstalacion)
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.MotivosDocumentosInstalaciones)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.SucursalOrdenInstalacion)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.UsuarioRegistroOrdenInstalacion)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.HasOne(d => d.CategoriaArcotelOrdenInstalacionNavigation)
                    .WithMany(p => p.Ordeninstalacion)
                    .HasForeignKey(d => d.CategoriaArcotelOrdenInstalacion)
                    .HasConstraintName("FK_CategoriaArcotel_OrdenInstalacion");

                entity.HasOne(d => d.ClienteOrdenInstalacionNavigation)
                    .WithMany(p => p.Ordeninstalacion)
                    .HasForeignKey(d => d.ClienteOrdenInstalacion)
                    .HasConstraintName("FK_Cliente_OrdenInstalacion");

                entity.HasOne(d => d.EmpleadoRegistroOrdenInstalacionNavigation)
                    .WithMany(p => p.Ordeninstalacion)
                    .HasForeignKey(d => d.EmpleadoRegistroOrdenInstalacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("EmpleadoRegistraFK");

                entity.HasOne(d => d.EmpresaOrdenInstalacionNavigation)
                    .WithMany(p => p.Ordeninstalacion)
                    .HasForeignKey(d => d.EmpresaOrdenInstalacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Empresa_OrdenInstalacion");

                entity.HasOne(d => d.FormaReclamoOrdenInstalacionNavigation)
                    .WithMany(p => p.Ordeninstalacion)
                    .HasForeignKey(d => d.FormaReclamoOrdenInstalacion)
                    .HasConstraintName("FK_FormaReclamo_OrdenInstalacion");

                entity.HasOne(d => d.SucursalOrdenInstalacionNavigation)
                    .WithMany(p => p.Ordeninstalacion)
                    .HasForeignKey(d => d.SucursalOrdenInstalacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Sucursal_OrdenInstalacion");
            });

            modelBuilder.Entity<Parametros>(entity =>
            {
                entity.HasKey(e => e.EmpresasParametro)
                    .HasName("PRIMARY");

                entity.ToTable("parametros");

                entity.HasIndex(e => e.CategoriaDocumentoLlamadaCallCenterParametro)
                    .HasName("FK_CategoriaDocumentoInstalacion_Llamada");

                entity.HasIndex(e => e.TipoAjusteTomaInventarioParametro)
                    .HasName("FKTipoAjusteTomaInventarioParametro");

                entity.Property(e => e.EmpresasParametro)
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ActivarCheckAnticipoPagosParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ActivarDescuentoProductoVentaEnCompraParametro)
                    .HasColumnType("int(1)")
                    .HasDefaultValueSql("'1'")
                    .HasComment("Si se activa que el descuento se aplique en la venta el momento de comprar");

                entity.Property(e => e.ActivarListaPreciosClienteFacturaSimpreParametro).HasColumnType("int(1)");

                entity.Property(e => e.ActivarTotalesClienteCarteraClienteParametro)
                    .HasColumnType("int(1)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.ActivarVentaProductosParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ActualizarValorCobroEnUnionPagoParametro).HasColumnType("int(1)");

                entity.Property(e => e.AgenteRetencionParametro).HasColumnType("int(1)");

                entity.Property(e => e.AgregarDescripcionFacturaParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.AgregarDetalleChequesReporteCarteraParametro).HasColumnType("int(1)");

                entity.Property(e => e.AgregarMarcaReporteCarteraClientesParametro).HasColumnType("int(1)");

                entity.Property(e => e.AgregarMesComisionEmpleadoParametro).HasColumnType("int(1)");

                entity.Property(e => e.AgregarProductoDescuentoNotaCreditoCompras).HasColumnType("int(1)");

                entity.Property(e => e.AlineacionHorizontalLogoRideParametro)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'centro'")
                    .HasComment("Indica la alineacion horizontal de los logo en los rides")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.AlineacionVerticalLogoRideParametro)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'centro'")
                    .HasComment("Indica la alineacion vertical de los logo en los rides")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.AltoCodigoBarraEtiquetasProductoParametro)
                    .HasColumnType("double(5,2)")
                    .HasDefaultValueSql("'9.00'");

                entity.Property(e => e.AltoCodigoBarraRideFacturaParametro)
                    .HasColumnType("double(5,2)")
                    .HasDefaultValueSql("'50.00'");

                entity.Property(e => e.AmbienteFacturacionElectronica)
                    .HasColumnType("varchar(3)")
                    .HasDefaultValueSql("'PRU'")
                    .HasComment("1 pruebas, 2 produccion")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.AmbienteRecargasElectronicasParametro)
                    .HasColumnType("int(1)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.AmbienteSincronizacionDatosApiEcommerce)
                    .HasColumnType("int(1)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.AnchoEtiquetaVentaParametro)
                    .HasColumnType("double(4,2)")
                    .HasDefaultValueSql("'52.00'");

                entity.Property(e => e.AnchoLogoRideParametro)
                    .HasColumnType("double(16,4)")
                    .HasDefaultValueSql("'100.0000'")
                    .HasComment("Indica el ancho del logo en los rides");

                entity.Property(e => e.AnioInicioSistema)
                    .HasColumnType("int(4)")
                    .HasDefaultValueSql("'2019'");

                entity.Property(e => e.AnticiposManejoCajaParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.AnularImportacionConGastoParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ApiSincronizarEcommerceParametro)
                    .IsRequired()
                    .HasColumnName("apiSincronizarEcommerceParametro")
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'STRAPI'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.AplicaInteresFacturaParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ArquearCajaSinTotalesParametro).HasColumnType("int(1)");

                entity.Property(e => e.ArrastarDepositosTransitoVariosMesesParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.AsientoDetalladoPagoProveedorParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.AutorizaComprobanteEnCobroParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.AutorizacionFacturaPorVendedorParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasComment("Se usa este parámetro para armar la secuencia de autorizacion según el vendedor (1) o según el usuario que procesa (0)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.AutorizacionOrdenPorVendedorParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasComment("Se usa este parámetro para armar la secuencia de autorizacion según el vendedor (1) o según el usuario que procesa (0)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.AutorizarChequesProtestadosFacturaParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.AutorizarCupoFacturaParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'1'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.AutorizarDiasVencidosFacturaParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'1'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.AutorizarFacturasElectronicasModuloFacturacionParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'1'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.AutorizarFacturasElectronicasModuloRestaurantParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'1'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.AutorizarHorasExtrasParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.AutorizarOmitirDescuentoCupoClienteParametro)
                    .IsRequired()
                    .HasColumnType("varchar(2)")
                    .HasDefaultValueSql("'1'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.AutorizarPlazoFacturaParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'1'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.AutorizarPrecioFacturaParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'1'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.BloqueaAnticipoCobrosParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.BloqueoActualizacionNumContratoParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'1'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.BloqueoCambioClienteContratoParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'1'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.BloqueoContratoCanceladoParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'1'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.BodegaCompraParametro)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasComment("Almacena la bodega por defecto de compra,Bodegas")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.BodegaDefectoCotizacionParametro)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.BodegaProduccionParametro)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.BodegaVentaParametro)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasComment("Alamacena la bodega por defecto de venta")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.BotonActulizarPreciosProductoParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasComment("Activar el boton para actualizar el precio desde el ingreso de compra")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.BuscarSustitutosPorDescripcionAdicionalParametro).HasColumnType("int(1)");

                entity.Property(e => e.CalcularImpuestoRentaEmpleadoParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CalificacionArtesanalParametro)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'NO'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CalificacionClienteNuevoParametro)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'6'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CambiaVendedorAutomaticoOrdenes)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'1'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CantidadCuotaPagoReconexionParametro).HasColumnType("decimal(16,4)");

                entity.Property(e => e.CantidadDecimalParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'1'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CantidadDecimalVentaParametro)
                    .HasColumnType("int(12)")
                    .HasDefaultValueSql("'6'");

                entity.Property(e => e.CantidadMaximaHilosConcurrentesParametro)
                    .HasColumnName("cantidadMaximaHilosConcurrentesParametro")
                    .HasColumnType("int(3)")
                    .HasDefaultValueSql("'5'");

                entity.Property(e => e.CantidadOrdenCompraParametro).HasColumnType("double(16,4)");

                entity.Property(e => e.CargarBodegasDetalleFacturaNotaCreditoVentaParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CargarExcelComisionesParametro).HasColumnType("int(1)");

                entity.Property(e => e.CargarExcelCrearOrdenesImportacioncesParametro).HasColumnType("int(1)");

                entity.Property(e => e.CargarExcelOrpparametro)
                    .HasColumnName("CargarExcelORPParametro")
                    .HasColumnType("int(1)");

                entity.Property(e => e.CargarExcelPromocionesParametro).HasColumnType("int(1)");

                entity.Property(e => e.CargarFobSinFleteVerImportacionesParametros).HasColumnType("int(1)");

                entity.Property(e => e.CargarHistorialClientesFactura)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CargarOrganigramaParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'1'")
                    .HasComment("valida si se debe cargar el organigrama en la empresa.")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CargarSeriesItemsFacturacionParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasComment("1 carga la series de los items en la factura, 0 no carga las series de los items en las facturacion")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CargarSucursalSessionDetalleGastoParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CargarVariosDetallesComprasNotaCreditoCompras).HasColumnType("int(1)");

                entity.Property(e => e.CargarXmldetalleCompraParametro)
                    .IsRequired()
                    .HasColumnName("CargarXMLDetalleCompraParametro")
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CategoriaDocumentoLlamadaCallCenterParametro)
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CentroCostoFacturaParametro).HasColumnType("int(1)");

                entity.Property(e => e.CentroCostosParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CerrarCajaMismoUsuarioParametro)
                    .HasColumnType("int(1)")
                    .HasDefaultValueSql("'1'")
                    .HasComment("Verifica si el usuario que abre la caja debe cerrarla");

                entity.Property(e => e.CerrarMesSaltadoConciliacionParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasComment("dfdf")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CierreCajaSinArqueoParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CirepresentanteParqueaderoCarrosParametro)
                    .IsRequired()
                    .HasColumnName("CIRepresentanteParqueaderoCarrosParametro")
                    .HasColumnType("varchar(13)")
                    .HasDefaultValueSql("'0'")
                    .HasComment("carga el ruc del representante legal del parqueadero")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CiudadReportesMatrizParametro)
                    .HasColumnType("int(1)")
                    .HasDefaultValueSql("'1'")
                    .HasComment("Define si la direccion de los reportes es de la matriz o de la sucursal individualmente");

                entity.Property(e => e.ClaveFirmaElectronicaParametro)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("'Agrota2000'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ClaveGestionRouterParametro)
                    .IsRequired()
                    .HasColumnType("blob")
                    .HasComment("Se usa para registro de usuario admin para envío de comandos desde contrato");

                entity.Property(e => e.ClaveMailCarteraParametro)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasDefaultValueSql("'agrotacartera'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ClaveMailComprasParametro)
                    .IsRequired()
                    .HasColumnType("char(20)")
                    .HasDefaultValueSql("'compras'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ClaveMailContabilidadParametro)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasDefaultValueSql("'contabilidad'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ClaveMailFacutacionElectronicaParametro)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("'agrota'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ClaveMailNovedadesParametro)
                    .IsRequired()
                    .HasColumnType("char(20)")
                    .HasDefaultValueSql("'agrota'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ClaveMailRrhhparametro)
                    .IsRequired()
                    .HasColumnName("ClaveMailRRHHParametro")
                    .HasColumnType("char(20)")
                    .HasDefaultValueSql("'rrhh@'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ClaveProduccionVariacionCostoMayorParametro)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'Agr1220'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ClienteDebeAprobarCotizacionParametro).HasColumnType("int(1)");

                entity.Property(e => e.ClienteDefectoFacturacionParametro)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CodificacionPrecioVentaParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CodigoDocumentoTxtbancoParametro)
                    .IsRequired()
                    .HasColumnName("CodigoDocumentoTXTBancoParametro")
                    .HasColumnType("varchar(50)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CodigoProductoFleteIvaParametro)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ComisionCalculadaTablaParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'1'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ComprarServiciosParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CompresionVelocidadServicioParametro).HasColumnType("int(1)");

                entity.Property(e => e.ConcatenarCodigoVentaImpresionDirectaParametro).HasColumnType("int(1)");

                entity.Property(e => e.ContabilizaAnticipoCuentaContableProveedorParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ContabilizacionPagosDetalladoParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ContabilizarDetalleSucursalGastoParametro).HasColumnType("int(1)");

                entity.Property(e => e.ContabilizarEfectivoBancoParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasComment("Identifica si se debe contabilizar con la cuenta del banco o la cuenta de destino fijo")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ContabilizarFechaPagoClienteParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasComment("1 Contabiliza con la Fecha del Pago 0 Contabiliza con la Fecha del Cheque")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ContabilizarFechaPagoParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasComment("1 Contabiliza con la Fecha del Pago 0 Contabiliza con la Fecha del Cheque")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ContabilizarIvaSustentoParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ContabilizarOtraBaseRolParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ContabilizarPrestamoIessparametro)
                    .IsRequired()
                    .HasColumnName("ContabilizarPrestamoIESSParametro")
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ContabilizarSucursalAnticiposClientesParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ContabilizarSucursalPagosClientesParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ContabilizarVacacionesParametro).HasColumnType("int(1)");

                entity.Property(e => e.ControlViajesParametro).HasColumnType("int(1)");

                entity.Property(e => e.ControlaUnCambioMismoEstadoContratoParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'1'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ControlarCupoClienteParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'1'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ControlarGastosPresupuestoParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasComment("indica si la empresa puede validar el ingreso de gastos  y que estos estan ligados con el presupuesto.")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ControlarInventarioFacturaParametro).HasColumnType("int(1)");

                entity.Property(e => e.ConvertirUnidadesCajaCompraParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CorreoRetencionParametro)
                    .IsRequired()
                    .HasColumnType("varchar(150)")
                    .HasDefaultValueSql("''")
                    .HasComment("direccion de correo para recibir las retenciones de los clientes.")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CortesBloqueReduceVelocidadParametro).HasColumnType("int(1)");

                entity.Property(e => e.CosteandoParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CreaDocPendienteDepositoCajaParametro)
                    .IsRequired()
                    .HasColumnType("varchar(3)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CreacionRapidaProductosParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CrearItemsComprasParametro).HasColumnType("int(1)");

                entity.Property(e => e.CtrlFinDeSemanaVacacionesParametro).HasColumnType("int(1)");

                entity.Property(e => e.CuadreCajaParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'1'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentaAdicionaManejoChequeParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentaCompraInventarioParametro)
                    .IsRequired()
                    .HasColumnType("varchar(18)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentaContableAlInicioParametro)
                    .IsRequired()
                    .HasColumnType("varchar(2)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentaContableNoAplica)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'0.00.00.000.000'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentaDefectoSaldarAnctipoParametros)
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentasCategorizadasEmpleadosParametros)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'1'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.DatosAdicionalesProductoParametros)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.DatosCotizacionProductoParametros)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.DecimalesLibroMayorParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'4'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.DepositarChequesInicialesPosfechadosManejoCheques)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.DepositoDirectoChequeParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.DepositoLotesCajaParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.DetalleContabilizacionParametro)
                    .IsRequired()
                    .HasColumnType("varchar(2)")
                    .HasDefaultValueSql("'1'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.DiaMaximoAnulacionFacturaParametro)
                    .IsRequired()
                    .HasColumnType("varchar(2)")
                    .HasDefaultValueSql("'09'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.DiasAnticipacionSolicitudVacacionParametro)
                    .HasColumnType("int(2)")
                    .HasDefaultValueSql("'30'")
                    .HasComment("indica el numero de dias con el cual se debe anticipar la solicitud de vacaciones");

                entity.Property(e => e.DiasAvisoConsignacionParametro)
                    .HasColumnType("int(6)")
                    .HasDefaultValueSql("'30'");

                entity.Property(e => e.DiasBloqueoCambioCalificacionFacturacion)
                    .HasColumnType("int(5)")
                    .HasComment("Determina el numero de dias de cambio de calificacion de un cliente antes de bloquear ventas");

                entity.Property(e => e.DiasBloqueoConsignacionParametro)
                    .HasColumnType("int(6)")
                    .HasDefaultValueSql("'45'");

                entity.Property(e => e.DiasCalculoAntesVencimiento).HasColumnType("int(5)");

                entity.Property(e => e.DiasGraciaVencimientoParametro).HasColumnType("double(10,2)");

                entity.Property(e => e.DiasPagoFechaEmisionFacturacion).HasColumnType("int(5)");

                entity.Property(e => e.DiasSobregiroImportacionParametro)
                    .HasColumnType("int(11)")
                    .HasComment("Almacena el numero de dias que se permite facturar una importacion sin liquidar");

                entity.Property(e => e.DiasVencimientoCreditosParametros)
                    .HasColumnType("int(3)")
                    .HasComment("Parametro de dias vencimiento en creditos");

                entity.Property(e => e.EmpleadoIessparametro)
                    .HasColumnName("EmpleadoIESSParametro")
                    .HasColumnType("double(16,4)")
                    .HasDefaultValueSql("'9.3300'");

                entity.Property(e => e.EmpleadoSinRolReporteCobros).HasColumnType("int(1)");

                entity.Property(e => e.EmpleadosJefeRrhhparametro)
                    .IsRequired()
                    .HasColumnName("EmpleadosJefeRRHHParametro")
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.EmpresaServiciosMensualesParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasComment("0 para empresas que no manejan servicios(contratos), 1 servicios")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.EmpresasGruposGastosParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.EncuestarClienteParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.EnviarMailBitacoraFacturaParametro).HasColumnType("int(1)");

                entity.Property(e => e.EnviarMailFacturaParametros)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'1'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.EnviarMailOrdenCompraPerfilAutorizaParametro)
                    .HasColumnType("int(1)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.EnviarMailOrpparametro)
                    .IsRequired()
                    .HasColumnName("EnviarMailORPParametro")
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.EnviarMailPagosParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'1'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.EnviarMailRideParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'1'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.EquiposEmpleadosMovilParametros)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.EsquemaOfflineFacturacionElectronicaParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.EstadoActivoInterfazRemotaServiciosParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ExistenciaAdminitracionProductosParametro).HasColumnType("int(1)");

                entity.Property(e => e.FacturaServiciosMesesPosterioresParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.FacturaUsuarioAperturaCajaParametro).HasColumnType("int(1)");

                entity.Property(e => e.FacturacionActivacionServicioParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'1'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.FacturacionElecronicoParametro)
                    .IsRequired()
                    .HasColumnType("varchar(2)")
                    .HasDefaultValueSql("'1'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.FacturacionServiciosXclienteParametro)
                    .IsRequired()
                    .HasColumnName("FacturacionServiciosXClienteParametro")
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.FacturarPromocionConsumidorFinalParametro)
                    .HasColumnType("int(1)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.FacturarVariasOrdenesServicioTecnicoJuntasParametro).HasColumnType("int(1)");

                entity.Property(e => e.FechaBloqueadaCobroClientesParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.FechaCargaInformacionSistemaParametro)
                    .HasColumnType("date")
                    .HasDefaultValueSql("'1900-01-01'");

                entity.Property(e => e.FechaPagoCostaXivparametro)
                    .IsRequired()
                    .HasColumnName("FechaPagoCostaXIVParametro")
                    .HasColumnType("varchar(5)")
                    .HasDefaultValueSql("''")
                    .HasComment("02-29")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.FechaPagoSierraXivparametro)
                    .IsRequired()
                    .HasColumnName("FechaPagoSierraXIVParametro")
                    .HasColumnType("varchar(5)")
                    .HasDefaultValueSql("'08-31'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.FechaPagoXiiiparametro)
                    .IsRequired()
                    .HasColumnName("FechaPagoXIIIParametro")
                    .HasColumnType("varchar(5)")
                    .HasDefaultValueSql("'12-01'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.FechaRegistroParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.FiltrarSucursalPuntosFacturaServicioTecnicoParametro)
                    .HasColumnType("int(1)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.FiltrarVendedoresPorRolPagosParametro)
                    .HasColumnType("int(1)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.FiltrarXfechaCarteraClienteParametro)
                    .HasColumnName("FiltrarXFechaCarteraClienteParametro")
                    .HasColumnType("int(1)");

                entity.Property(e => e.FiltroPlacaServicioTecnicoParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.FiltroPromedioDeudaCarteraSinDecimalesParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.FirmaRecibidoGuiaElectronicaParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.FondoReservaParametro).HasColumnType("double(16,4)");

                entity.Property(e => e.FormaPagoFacturaDefectoParametro)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'1'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.FormatoCondicionesParametro)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.FormatoDocumentoParametro)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.FraseClienteCalificaPagoA)
                    .IsRequired()
                    .HasColumnType("varchar(150)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.FraseClienteCalificaPagoB)
                    .IsRequired()
                    .HasColumnType("varchar(150)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.FraseClienteCalificaPagoC)
                    .IsRequired()
                    .HasColumnType("varchar(150)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.FraseClienteCalificaPagoD)
                    .IsRequired()
                    .HasColumnType("varchar(150)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.FraseClienteCalificaPagoE)
                    .IsRequired()
                    .HasColumnType("varchar(150)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.FraseClienteCalificaVolumeA)
                    .IsRequired()
                    .HasColumnType("varchar(150)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.FraseClienteCalificaVolumeB)
                    .IsRequired()
                    .HasColumnType("varchar(150)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.FraseClienteCalificaVolumeC)
                    .IsRequired()
                    .HasColumnType("varchar(150)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.FraseClienteCalificaVolumeD)
                    .IsRequired()
                    .HasColumnType("varchar(150)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.FraseClienteCalificaVolumeE)
                    .IsRequired()
                    .HasColumnType("varchar(150)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.FuenteImpresionSolicitudVacacionesParametro)
                    .IsRequired()
                    .HasColumnType("varchar(250)")
                    .HasDefaultValueSql("'arial.ttf'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.FuenteRideFacturaParametro)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("'unispace.ttf'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.FuenteRideGuiaParametro)
                    .IsRequired()
                    .HasColumnType("varchar(200)")
                    .HasDefaultValueSql("'unispace.ttf'")
                    .HasComment("unispace.ttf")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.FuenteRideLiquidacionCompraParametro)
                    .IsRequired()
                    .HasColumnType("varchar(250)")
                    .HasDefaultValueSql("'arial.ttf'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.FuenteRideNotaCreditoVentaParametro)
                    .IsRequired()
                    .HasColumnType("varchar(200)")
                    .HasDefaultValueSql("'unispace.ttf'")
                    .HasComment("unispace.ttf")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.FuenteRideRetencionParametro)
                    .IsRequired()
                    .HasColumnType("varchar(200)")
                    .HasDefaultValueSql("'unispace.ttf'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.GastoIvaSeparadoParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.GeneraNumeroFacturaDeudaParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.GeneraNumeroFacturacionBloqueServiciosParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.GeneraProductoCotizacionParametro).HasColumnType("int(1)");

                entity.Property(e => e.GeneraRecargoPagosParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.GenerarDebitosBancariosParametro).HasColumnType("int(1)");

                entity.Property(e => e.GenerarDespachosFacturaParametro).HasColumnType("int(1)");

                entity.Property(e => e.GenerarExcelReembolsoGastosParametro).HasColumnType("int(1)");

                entity.Property(e => e.GenerarImpresionFacturaParametros)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'1'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.GenerarNumeroNotaCreditoParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.GenerarNumeroProduccionParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.GenerarPagoDesdeContratoServicioParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.GenerarSeguimientoFacturaParametros)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'1'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.GenerarTelemarketingFacturaParametros)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'1'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.GenerarTurnoFacturaParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasComment("verifica la generarcion del turno medico en la factura")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.GenerarTxtbancarioParametro)
                    .IsRequired()
                    .HasColumnName("GenerarTXTBancarioParametro")
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Generarasientosdepreciacioncomprasactivosparametro)
                    .IsRequired()
                    .HasColumnName("generarasientosdepreciacioncomprasactivosparametro")
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'1'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.GenererarRecibirCompraParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'1'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.GestionaCientesEquifaxParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.GestionaContratoPorServiciosParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.GrabarBitacoraRepCarteraXestadoParametro)
                    .IsRequired()
                    .HasColumnName("GrabarBitacoraRepCarteraXEstadoParametro")
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.GuiaTransporteApiParametro).HasColumnType("int(1)");

                entity.Property(e => e.GuiasFacturas)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.HabilitarSecuenciaCobroMovilParametros).HasColumnType("int(1)");

                entity.Property(e => e.HorasExtrasFormatoEnteroParametro).HasColumnType("int(1)");

                entity.Property(e => e.HorasExtrasJornadaNocturnaParametro).HasColumnType("int(1)");

                entity.Property(e => e.IceParametro)
                    .HasColumnType("double(16,4)")
                    .HasComment("ice");

                entity.Property(e => e.IdEmpresaXadisParametro)
                    .IsRequired()
                    .HasColumnType("varchar(36)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ImpresionDescuentoSobrePvpordenPedidoParametro)
                    .HasColumnName("ImpresionDescuentoSobrePVPOrdenPedidoParametro")
                    .HasColumnType("int(1)");

                entity.Property(e => e.ImpresionDescuentoTotalOrdenPedidoParametro)
                    .HasColumnType("int(1)")
                    .HasDefaultValueSql("'1'")
                    .HasComment("0 identificaDescuento(PuntoColor), 1 calculoDescuentoTotal");

                entity.Property(e => e.ImpresionDetalladaManejoCajaParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'1'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ImpresionDirectaCierreCajaParametro).HasColumnType("int(1)");

                entity.Property(e => e.ImpresionDirectaFacturaParametro).HasColumnType("int(1)");

                entity.Property(e => e.ImpresionDirectaNotaCreditoParametro).HasColumnType("int(1)");

                entity.Property(e => e.ImpresionDirectaPagoParametro).HasColumnType("int(1)");

                entity.Property(e => e.ImpresionDirectaTransferenciaItemsParametro).HasColumnType("int(1)");

                entity.Property(e => e.ImpresionHorizontalChequeParametro).HasColumnType("int(1)");

                entity.Property(e => e.ImpresoraEtiquetasParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ImprimeDireccionLocalContratoPagoParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ImprimirBaseLineEtiquetaProductoParametro)
                    .HasColumnType("int(1)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.ImprimirCodigoBarraEtiquetaProductoParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'1'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ImprimirCodigoBarrasPiePaginaRideFacturaParametro).HasColumnType("int(1)");

                entity.Property(e => e.ImprimirCodigoVentaEtiquetaProductoParametro).HasColumnType("int(1)");

                entity.Property(e => e.ImprimirContratoEmpleadoParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasComment("indica si es posible realizar la impresion de los contratos de trabajo por obra cierta")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ImprimirCuotasCreditoRideparametro)
                    .HasColumnName("ImprimirCuotasCreditoRIDEparametro")
                    .HasColumnType("int(1)");

                entity.Property(e => e.ImprimirDatosLocalizacionDestinoGuiaRideParametro)
                    .HasColumnType("int(1)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.ImprimirDescuentoFacturaParametro).HasColumnType("int(1)");

                entity.Property(e => e.ImprimirDetalleDobleLineaParametro)
                    .HasColumnType("int(1)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.ImprimirEnvioRetencionFacturaParametro).HasColumnType("int(1)");

                entity.Property(e => e.ImprimirFirmaClienteInformeServicioTecnicoParametro)
                    .HasColumnType("int(1)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.ImprimirFormaPagoDigitadoVehiculosParametro).HasColumnType("int(1)");

                entity.Property(e => e.ImprimirFormaPagoFacturaParametro).HasColumnType("int(1)");

                entity.Property(e => e.ImprimirFormaPagoSriFacturaParametro).HasColumnType("int(1)");

                entity.Property(e => e.ImprimirGuiaSinSerieParametro)
                    .IsRequired()
                    .HasColumnType("char(1)")
                    .HasDefaultValueSql("'1'")
                    .HasComment("Identifica si se puede imprimir la guia de remision de las facturas que no se han tomado series")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ImprimirImagenServicioTecnicoParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ImprimirLeyendaAhorroTotalPrecioMarcadoParametro).HasColumnType("int(1)");

                entity.Property(e => e.ImprimirLineaRideDetalleParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'1'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ImprimirNombreComercialEmpresaEtiquetaProductosParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ImprimirNombreComercialFacturaTirillaParametro).HasColumnType("int(1)");

                entity.Property(e => e.ImprimirNombreEmpresaEtiquetaProductoParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'1'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ImprimirNombreEmpresaEtiquetaProductosParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ImprimirNombresImpresionFacturaParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ImprimirNombresImpresionGuiaParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ImprimirObservacionCompletaPagosParametro)
                    .HasColumnType("int(1)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.ImprimirObservacionCompletoFacturasParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ImprimirOrdenInstalacionContratoParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ImprimirPrecioVentaEtiquetaProductoParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'1'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ImprimirProvisionesRolParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasComment("parametro en 1 muestra valores con *, en cero poner 0 en los valores que no cobra")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ImprimirRecomendacionesInformeServicioTecnicoParametro).HasColumnType("int(1)");

                entity.Property(e => e.ImprimirReferenciaEtiquetaProductoParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ImprimirRideNormalCreditoParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ImprimirSaldoChequesPosfechadosParametro).HasColumnType("int(1)");

                entity.Property(e => e.ImprimirSinCabeceraRideParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ImprimirTerminosGarantiaInformeServicioTecnicoParametro).HasColumnType("int(1)");

                entity.Property(e => e.ImprimirTirillaRideParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ImprimirTotalDeudaReciboClienteParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ImprimirUbicacionEtiquetaProductoParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ImprimirValidezTributariaFacturaParametro).HasColumnType("int(1)");

                entity.Property(e => e.ImprimirVendedorFacturaParametro).HasColumnType("int(1)");

                entity.Property(e => e.IngresaComisionesParametro)
                    .IsRequired()
                    .HasColumnType("varchar(2)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.IngresaDatosArcsaParametro).HasColumnType("int(1)");

                entity.Property(e => e.IngresaFechaMovimientoBancosParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.IngresaNumeroOrdenCompraFacturaParametro).HasColumnType("int(1)");

                entity.Property(e => e.IngresaObservacionCarteraClientesParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.IngresarDetalleCargaGuiaFacturaParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.IngresarFechaDistintaRetencionparametro)
                    .IsRequired()
                    .HasColumnType("char(1)")
                    .HasDefaultValueSql("'0'")
                    .HasComment("0 puede ingresar, 1 no puede ingresar la retencino con fecha distinta.")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.IngresarPacienteFacturaParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.IngresoComisionesSinGrabarParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'1'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.IngresoCompraDirectaParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'1'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.IngresoNotaCreditoDirectaParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.InsertarDirectamenteClientesPlazosParametro).HasColumnType("int(1)");

                entity.Property(e => e.InteresxMoraCobrosParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.IpEmpresaPrincipalParametro)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'45.177.124.112'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.IpWiseParametro)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasDefaultValueSql("'192.168.0.1'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.IsdliquidarImportacionParametro)
                    .IsRequired()
                    .HasColumnName("ISDLiquidarImportacionParametro")
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'1'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Isdparametro)
                    .HasColumnName("ISDParametro")
                    .HasColumnType("double(16,4)");

                entity.Property(e => e.IvaCompraParametro).HasColumnType("double(7,4)");

                entity.Property(e => e.IvaParametro)
                    .HasColumnType("double(7,2)")
                    .HasComment("Almacena el valor del iva");

                entity.Property(e => e.LargoLogoRideParametro)
                    .HasColumnType("double(16,4)")
                    .HasDefaultValueSql("'100.0000'")
                    .HasComment("Indica el largo del logo en los rides");

                entity.Property(e => e.LectorBarraParametro)
                    .IsRequired()
                    .HasColumnType("varchar(2)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.LigarChequesPuntoFactura)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.LigarFacturasDepositoTarjetaCredito)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasComment("Parametro para Obligar a seleccionar las facturas en el proceso de deposito de  las tarjetas de credito ")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.LigarMaterialesOrdenServicioTecnicoParametro).HasColumnType("int(1)");

                entity.Property(e => e.LigarVariasOrdenesCompraParametro).HasColumnType("int(1)");

                entity.Property(e => e.LiquidacionCompraElectronicaParametro).HasColumnType("int(1)");

                entity.Property(e => e.ListaPrecioDefectoCreacionClienteRapidaParametro)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ListasPreciosFacturacionParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.LogoEmpresaParametro)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.MailCarteraParametro)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("'agrotacartera@agrota.com'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.MailComprasParametro)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("'compras@agrota.com'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.MailContabilidadParametro)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("'contabilidad@agrota.com'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.MailFacturaElectronicaParametro)
                    .IsRequired()
                    .HasColumnType("char(80)")
                    .HasDefaultValueSql("'wise@agrota.com'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.MailNovedadesParametro)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("'wise@agrota.com'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.MailRrhhparametro)
                    .IsRequired()
                    .HasColumnName("MailRRHHParametro")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("'rrhh@agrota.com'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ManejaDiaCorteContratoParametro).HasColumnType("int(1)");

                entity.Property(e => e.ManejaMotivosCategoriasCallCenterParametro).HasColumnType("int(1)");

                entity.Property(e => e.ManejaTempariosServicioTecnicoParametro).HasColumnType("int(1)");

                entity.Property(e => e.ManejoPvfcompraParametro)
                    .IsRequired()
                    .HasColumnName("ManejoPVFCompraParametro")
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.MaximoDiasFacturasXautorizar)
                    .HasColumnName("MaximoDiasFacturasXAutorizar")
                    .HasColumnType("double(10,2)")
                    .HasDefaultValueSql("'2.00'");

                entity.Property(e => e.MaximoDiasGenerarGuiaFactuaParametro)
                    .HasColumnType("int(2)")
                    .HasDefaultValueSql("'5'");

                entity.Property(e => e.MaximoMesCalculoCupo)
                    .IsRequired()
                    .HasColumnType("varchar(5)")
                    .HasDefaultValueSql("'06'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.MaximoValorAtrasoParametro)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasDefaultValueSql("'20'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.MensajeMailFacturaParametro)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.MesInicioSistema)
                    .HasColumnType("int(2)")
                    .HasDefaultValueSql("'9'");

                entity.Property(e => e.MesesAntiguedadMaximaFacturaReconexionPagoParametro).HasColumnType("int(5)");

                entity.Property(e => e.MesesTrabajadoEmpleadoAnticipoParametro)
                    .HasColumnType("double(2,0)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.MetodoCostoParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'P'")
                    .HasComment("P Promedio,F Fifo, L Lifo")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.MinutosTrabajoMesParametro).HasColumnType("int(5)");

                entity.Property(e => e.ModificaCamposAutorizacionFechaProveedorParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'1'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ModificaFechaServicioTecnicoParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ModificarIvaGastoParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasComment("indica si se puede modificar el valor del iva del gasto : 1 puede, 0 no puede.")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ModificarPrecioFacturaParametro).HasColumnType("int(1)");

                entity.Property(e => e.ModificarValoresOrpaprobadaParametro)
                    .HasColumnName("ModificarValoresORPAprobadaParametro")
                    .HasColumnType("int(1)");

                entity.Property(e => e.ModuloBuffersParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ModuloEcommerceParametro).HasColumnType("int(1)");

                entity.Property(e => e.ModuloImportacionesParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ModuloMedicoParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ModuloRefenciasImportacionParametro).HasColumnType("int(1)");

                entity.Property(e => e.MontoDescuadreGeneraAnticipoParametro)
                    .HasColumnType("decimal(16,4)")
                    .HasDefaultValueSql("'1.0000'");

                entity.Property(e => e.MostrarAnticiposCarteraParametro)
                    .HasColumnType("int(1)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.MultaMinutoRetrazoParametro).HasColumnType("double(16,4)");

                entity.Property(e => e.MultaNoMarcadoParametro).HasColumnType("double(16,4)");

                entity.Property(e => e.NotaCreditoDescuentoCostoParametro).HasColumnType("int(1)");

                entity.Property(e => e.NotaEstadoCuentaClienteParametro)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NotaImpresionFinalOrdenPedidoParametro)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NotaImpresionInicioOrdenPedidoParametro)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NotaRideFacturacionElectronica)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NotaRideRestaurantParametro)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NumDiasDescuentoReconexionGeneracionNcvparametro)
                    .HasColumnName("NumDiasDescuentoReconexionGeneracionNCVParametro")
                    .HasColumnType("int(1)");

                entity.Property(e => e.NumeracionContratoAgrupadoParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NumeracionMesualAsientoParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NumeracionPuntoCajaFacturaServiciosParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'1'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NumeracionSecuencialContratoParametro).HasColumnType("int(1)");

                entity.Property(e => e.NumeroCaracterDescripcionRideDetalleParametro).HasColumnType("int(5)");

                entity.Property(e => e.NumeroCuotasMaximoParametro)
                    .HasColumnType("int(3)")
                    .HasComment("Numero maximo de cuotas admitido para el calculo de credito");

                entity.Property(e => e.NumeroDecimalesComprasParametro)
                    .HasColumnType("int(12)")
                    .HasDefaultValueSql("'4'");

                entity.Property(e => e.NumeroDecimalesVentaParametro)
                    .IsRequired()
                    .HasColumnType("varchar(2)")
                    .HasDefaultValueSql("'4'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NumeroDiasLiquidacionParametro)
                    .HasColumnType("int(2)")
                    .HasComment("indica el numero de dias en que se debe liquidar al empleado o si debe generar rol.");

                entity.Property(e => e.NumeroFacturaGrabarParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("''")
                    .HasComment("Verifica si se obtiene el numero de factura al grabar o al hacer nuevo")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NumeroLetrasCalificacionParametro)
                    .HasColumnType("int(1)")
                    .HasDefaultValueSql("'3'");

                entity.Property(e => e.NumeroMailsParametro).HasColumnType("int(4)");

                entity.Property(e => e.NumeroMaximoItemsTranferenciaInternaParametro)
                    .HasColumnType("int(3)")
                    .HasDefaultValueSql("'20'");

                entity.Property(e => e.NumeroMesHistorialMargenComisionParametro)
                    .HasColumnType("int(5)")
                    .HasDefaultValueSql("'12'");

                entity.Property(e => e.NumeroMesHistorialVolumenComisionParametro)
                    .HasColumnType("int(5)")
                    .HasDefaultValueSql("'12'");

                entity.Property(e => e.NumeroProductosFacturaParametro)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'5'")
                    .HasComment("Almacena el numero maximo de productos en una factura");

                entity.Property(e => e.NumeroRenovacionPendientesParametro)
                    .HasColumnType("int(1)")
                    .HasComment("numero de veces que se puede renovar el pendiente de ordenes de pedido.");

                entity.Property(e => e.NumeroResolucionContribuyenteParametro)
                    .IsRequired()
                    .HasColumnType("varchar(4)")
                    .HasDefaultValueSql("'000'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NumeroRetencionAutamaticoParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'1'")
                    .HasComment("Verifica si el numero de retencion es automatico o es digitado")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NumeroUsuariosActivosParametro)
                    .HasColumnType("int(11)")
                    .HasComment("si es cero el numero de usuarios sera ilimitado");

                entity.Property(e => e.NumeroVecesMaximoDeducibleParametro)
                    .HasColumnType("double(16,4)")
                    .HasDefaultValueSql("'1.3000'");

                entity.Property(e => e.ObligaIngresaNumeroOrdenCompraFacturaParametro)
                    .HasColumnType("int(1)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.ObligadoLlevarContabilidadParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'1'")
                    .HasComment("obliga a llevar contabilidad: 1 obligado, 0 no obligado")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ObligarCobroClientesFacturaMasAntigua)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ObligarLigarOrdenesCompraParametro).HasColumnType("int(1)");

                entity.Property(e => e.ObligarSeleccionEquifaxAutorizacionContratoParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ObligarSeleccionPromocionIngresoContratoParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ObligarValorCalculadoParametro)
                    .IsRequired()
                    .HasColumnType("char(1)")
                    .HasDefaultValueSql("'1'")
                    .HasComment("1 obliga 0 no obliga a tomar el valor calculado.")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.OcultarEmpresaPantallaPagosProveedoresParametro)
                    .HasColumnType("int(1)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.OcultarLocaliacionPagosClientesParametros)
                    .HasColumnType("int(1)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.OcultarMesConsumoCobroClientesParametro)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.OcultarObservacionCobroClientesParametro)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.OcultarRucPantallaPagosProveedoresParametro)
                    .HasColumnType("int(1)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.OcultarSucursalPantallaPagosProveedoresParametro)
                    .HasColumnType("int(1)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.OmitirContratosReporteCarteraClientesParametro).HasColumnType("int(1)");

                entity.Property(e => e.OmitirEmpleadosConAvisosIessSalidaFacturaParametro).HasColumnType("int(1)");

                entity.Property(e => e.OrdenCompraReporteRotacionParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'1'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.OrdenConsignacionParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasComment("Parametro para obligar a utilizar la orden de consignacion")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.OrdenarXexistenciaBuscadorProductoParametro).HasColumnType("int(1)");

                entity.Property(e => e.OrpformaCotizacionParametro)
                    .IsRequired()
                    .HasColumnName("ORPFormaCotizacionParametro")
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.PagoQuincenaCuentaEmpleadoParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.PagoSriDefectoParametro)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.PagosClienteGeneraAnticipoParametro)
                    .HasColumnType("int(1)")
                    .HasComment("Permite que se genere un anticipo cuando se realiza un pago es los Arquitectos Moscoso");

                entity.Property(e => e.PagosManejoCajaParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'1'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.PagosProveedorCajaChicaBeneficiarioParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.PalabraCodificacionEtiquetaVentaParametro)
                    .IsRequired()
                    .HasColumnType("varchar(10)")
                    .HasDefaultValueSql("'MURCIELAGO'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ParametroActivoPromociones).HasColumnType("int(2)");

                entity.Property(e => e.ParametroDescuentoAdicionalMaximo).HasColumnType("int(2)");

                entity.Property(e => e.ParametroFactorProveedorImportacion)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ParametroFromMailParametro)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ParametroHostMailParametro)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ParametroLimiteBuscadores).HasColumnType("int(6)");

                entity.Property(e => e.ParametroLimiteBuscadoresProductos).HasColumnType("int(6)");

                entity.Property(e => e.ParametroPasswordMailParametro)
                    .IsRequired()
                    .HasColumnType("varchar(25)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ParametroPortMailParametro).HasColumnType("int(3)");

                entity.Property(e => e.ParametroUserNameMail1Parametro)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ParametroUserNameMail2Parametro)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ParametroUserNameMail3Parametro)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ParametroUserNameMail4Parametro)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ParametroUserNameMailParametro)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ParametroVerificarDatosChequesGrabarFactura)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'1'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ParametroVizualizacionDescuentoFactura)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Parametroidapple)
                    .IsRequired()
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ParqueaderoCarrosParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasComment("Identifica que la empresa manejo carros como items.")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.PatronoIessparametro)
                    .HasColumnName("PatronoIESSParametro")
                    .HasColumnType("double(16,4)")
                    .HasDefaultValueSql("'10.3500'");

                entity.Property(e => e.PedirAutorizacionFacturaParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'1'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.PedirAutorizacionOrdenParametro).HasColumnType("int(1)");

                entity.Property(e => e.PeriodoDiasRecalificacion).HasColumnType("int(5)");

                entity.Property(e => e.PeriodoUbgrupoEconomico)
                    .IsRequired()
                    .HasColumnName("PeriodoUBGrupoEconomico")
                    .HasColumnType("varchar(2)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.PermitirActualizarFechaVentaContratoParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.PermitirActualizarVendedorContratoParametro).HasColumnType("int(1)");

                entity.Property(e => e.PermitirAnularRetencionesCajaCerrada)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.PermitirCargarProductosNotaCreditoDescuentoParametro).HasColumnType("int(1)");

                entity.Property(e => e.PermitirDarBajaItemsRecetaParametro).HasColumnType("int(1)");

                entity.Property(e => e.PermitirFacturarBajoCostoParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.PermitirGenerarAnticipoNccompraParametro)
                    .HasColumnName("PermitirGenerarAnticipoNCCompraParametro")
                    .HasColumnType("int(1)");

                entity.Property(e => e.PermitirGrabarDepositoTarjetaDescuadreParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.PermitirIngresoNumeroFacturaGastoReocParametro).HasColumnType("int(1)");

                entity.Property(e => e.PermitirModificarCotizacionesDeOtroUsuario).HasColumnType("int(1)");

                entity.Property(e => e.PermitirModificarOrdenPedidoParametro).HasColumnType("int(1)");

                entity.Property(e => e.PermitirTranferenciaServiciosParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.PermitirVentaCreditoConsumidorFinalParametro).HasColumnType("int(1)");

                entity.Property(e => e.PlacaDefectoGuiasRemisionParametro)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.PlazosPorLineasClientesParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.PoderOmitirFacturasParaComisionesParametro).HasColumnType("int(1)");

                entity.Property(e => e.PonderarIceDauImportacionParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.PorRecibirConsignacionMovilParametro).HasColumnType("int(1)");

                entity.Property(e => e.PorcentajeDescontarTarjetaCreditoVentaParametro).HasColumnType("decimal(16,4)");

                entity.Property(e => e.PorcentajeDescuentoAnual).HasColumnType("double(16,4)");

                entity.Property(e => e.PorcentajeDescuentoEmisionFactura).HasColumnType("double(16,4)");

                entity.Property(e => e.PorcentajeDescuentoPrepagoFacturaParametro).HasColumnType("double(16,4)");

                entity.Property(e => e.PorcentajeDescuentoPrepagoOrdenParametro).HasColumnType("double(16,4)");

                entity.Property(e => e.PorcentajeDinardapParametro).HasColumnType("double(16,0)");

                entity.Property(e => e.PorcentajeGastoImportacionParametro)
                    .HasColumnType("double(4,2)")
                    .HasDefaultValueSql("'20.00'");

                entity.Property(e => e.PorcentajeImagenServicioTecnicoParametro)
                    .HasColumnType("double(16,4)")
                    .HasDefaultValueSql("'6.0000'");

                entity.Property(e => e.PorcentajeIncrementoHoraExtraParametro).HasColumnType("double(16,4)");

                entity.Property(e => e.PorcentajeInteresAnualCobrosParametro)
                    .HasColumnType("decimal(5,2)")
                    .HasComment("Valor como numero no porcentaje");

                entity.Property(e => e.PorcentajeInteresXmoraParametro)
                    .HasColumnName("PorcentajeInteresXMoraParametro")
                    .HasColumnType("double(16,4)");

                entity.Property(e => e.PorcentajeInventarioDisponibleEcommerceParametro)
                    .HasColumnName("porcentajeInventarioDisponibleEcommerceParametro")
                    .HasColumnType("decimal(16,4)")
                    .HasDefaultValueSql("'80.0000'");

                entity.Property(e => e.PorcentajePrecioParametro)
                    .HasColumnType("double(7,4)")
                    .HasComment("Se ingresa el porcentaje de precio de venta para calcular en caso de variacion de costo");

                entity.Property(e => e.PorcentajeSemaforoParametro).HasColumnType("double(5,2)");

                entity.Property(e => e.PorcentajeToleranciaVariacionCostoParametro)
                    .HasColumnType("decimal(16,4)")
                    .HasDefaultValueSql("'0.5000'");

                entity.Property(e => e.Porcentajemaximomultaparametro)
                    .HasColumnName("porcentajemaximomultaparametro")
                    .HasColumnType("double(4,2)");

                entity.Property(e => e.PosicionInicialXimpresionChequeParametro)
                    .HasColumnName("PosicionInicialXImpresionChequeParametro")
                    .HasColumnType("double(16,2)")
                    .HasDefaultValueSql("'150.00'");

                entity.Property(e => e.PosicionInicialXimpresionSobreVentaParametro)
                    .HasColumnName("PosicionInicialXImpresionSobreVentaParametro")
                    .HasColumnType("double(5,2)")
                    .HasDefaultValueSql("'15.00'");

                entity.Property(e => e.PosicionInicialYimpresionChequeParametro)
                    .HasColumnName("PosicionInicialYImpresionChequeParametro")
                    .HasColumnType("double(16,2)")
                    .HasDefaultValueSql("'7.50'");

                entity.Property(e => e.PosicionInicialYimpresionSobreVentaParametro)
                    .HasColumnName("PosicionInicialYImpresionSobreVentaParametro")
                    .HasColumnType("double(5,2)")
                    .HasDefaultValueSql("'20.00'");

                entity.Property(e => e.ProcesoAnulacionFacturaParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ProcesoAutorizacionSolicitudNcparametro)
                    .IsRequired()
                    .HasColumnName("ProcesoAutorizacionSolicitudNCParametro")
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ProcesoCargarExcelPrestamosIessParametro).HasColumnType("int(1)");

                entity.Property(e => e.ProcesoFacturacionCuponesParametro).HasColumnType("int(1)");

                entity.Property(e => e.ProcesoImpresionSobreVentaParametro)
                    .HasColumnType("int(1)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.ProcesoIngresarValorTarjetaCreditodeCajaParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ProcesoPrecioMarcadoParametro).HasColumnType("int(1)");

                entity.Property(e => e.ProcesoProgramacionPagosProveedoresParametro).HasColumnType("int(1)");

                entity.Property(e => e.ProcesoRecosteoIsdparametro)
                    .HasColumnName("ProcesoRecosteoISDParametro")
                    .HasColumnType("int(1)");

                entity.Property(e => e.ProcesoReemplazarCompraDetalleParametro).HasColumnType("int(1)");

                entity.Property(e => e.ProcesoVentaActivosParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ProcesoVentaReembolsoParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ProductoDefectoMemoReferenciaImportacionParametro)
                    .IsRequired()
                    .HasColumnType("varchar(30)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ProductoDescuentoNotaCreditoCompras)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ProductoGastosOrdenServicioTecnicoParametro)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ProductoIvaDescuentoNotaCreditoCompras)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ProductoTransporteEcommerceParametro)
                    .IsRequired()
                    .HasColumnName("productoTransporteEcommerceParametro")
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ProductosProduccionDetalle)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ProveedoresTipoGastoParametro)
                    .IsRequired()
                    .HasColumnType("varchar(2)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.PuertoEmpresaPrincipalParametro)
                    .HasColumnType("int(6)")
                    .HasDefaultValueSql("'120'");

                entity.Property(e => e.PuertoSalidaMailParametro)
                    .IsRequired()
                    .HasColumnType("char(6)")
                    .HasDefaultValueSql("'25'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.RealizaDeclaracionSemestralParametro).HasColumnType("int(1)");

                entity.Property(e => e.ReconexionAutomaticaPagoDeudaParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.RefrescarLuegoDeImprimirFacturaParametro).HasColumnType("int(1)");

                entity.Property(e => e.RegimendeMicroempresasParametro).HasColumnType("int(1)");

                entity.Property(e => e.RepresentanteParqueaderoCarrosParametro)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasComment("nombre del representante para impresion de documentos")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ResolucionAgenteRetencionParametro)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasDefaultValueSql("'1'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ResolucionContribuyenteEspecialParametro)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.RetencionFechaRegistroCompraProveedorParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.RetencionFechaRegistroCruceCompraProveedorParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.RetencionFechaRegistroGastoProveedorParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.SecuenciaAutomaticaChequeParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.SecuenciaNumeracionContratoXsucursalParametro)
                    .IsRequired()
                    .HasColumnName("SecuenciaNumeracionContratoXSucursalParametro")
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.SecuenciaProductoParametro).HasColumnType("double(10,0)");

                entity.Property(e => e.SegmentacionClientesParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.SeguridadSslmailParametro)
                    .IsRequired()
                    .HasColumnName("SeguridadSSLMailParametro")
                    .HasColumnType("varchar(1)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.SelCreaOrdenPedidoParametro)
                    .HasColumnType("tinyint(3)")
                    .HasDefaultValueSql("'1'")
                    .HasComment("Verifica si se puede cambiar el usuario que crea en la orden de pedido");

                entity.Property(e => e.SelCreaTransferenciaParametro)
                    .HasColumnType("tinyint(3)")
                    .HasDefaultValueSql("'1'")
                    .HasComment("Verifica si se puede cambiar el usuario que crea las transferencias");

                entity.Property(e => e.SelVendedorFacturaParamentro)
                    .HasColumnType("tinyint(3)")
                    .HasDefaultValueSql("'1'")
                    .HasComment("Verifica si se puede seleccionar el vendedor en las ventas");

                entity.Property(e => e.SeleccionBodegaMovilParametros)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.SeleccionaImpresionRideFacturaParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.SeleccionaProveedorTranferenciaInternaParametro).HasColumnType("int(1)");

                entity.Property(e => e.SeleccionarCuentaContableGatoParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.SeleccionarListaPreciosClienteFacturaParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'1'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.SeriesBilletes100Arqueo)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ServicioParametro)
                    .HasColumnType("int(2)")
                    .HasDefaultValueSql("'10'");

                entity.Property(e => e.ServicioRecargasElectronicasParametro).HasColumnType("int(1)");

                entity.Property(e => e.ServiciosManejaComboContratoParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'1'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ServidorSalidaMailParametro)
                    .IsRequired()
                    .HasColumnType("char(80)")
                    .HasDefaultValueSql("'smtpout.secureserver.net'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.SistemaMovilParametro).HasColumnType("int(1)");

                entity.Property(e => e.SolicitaNumeroReciboCobroParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'1'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.SolicitarClaveProduccionVariacionCostoMayorParametro)
                    .HasColumnType("int(1)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.SolicitudOrdenGastoParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.SucursalesParametro)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.SueldoBasicoAnioAnteriorParametro)
                    .HasColumnType("double(16,4)")
                    .HasDefaultValueSql("'354.0000'")
                    .HasComment("sueldo basico del anio pasado necesario para calculo de decimos.");

                entity.Property(e => e.SumarPagosCierreCajaParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.SustitutosRecetasParametro).HasColumnType("int(1)");

                entity.Property(e => e.TamanioFuenteComprobantePagoProveedoresParametro)
                    .HasColumnType("double(16,4)")
                    .HasDefaultValueSql("'6.0000'");

                entity.Property(e => e.TamanioFuenteImpresionInformeServicioTecnicoParametro)
                    .HasColumnType("double(5,2)")
                    .HasDefaultValueSql("'6.00'");

                entity.Property(e => e.TamanioFuenteImpresionMemoParametro)
                    .HasColumnType("double(16,4)")
                    .HasDefaultValueSql("'7.0000'");

                entity.Property(e => e.TamanioFuenteImpresionOrdenServicioTecnicoParametro)
                    .HasColumnType("double(5,2)")
                    .HasDefaultValueSql("'6.00'");

                entity.Property(e => e.TamanoFuenteImpresionSolicitudVacacionesParametro)
                    .HasColumnType("decimal(5,2)")
                    .HasDefaultValueSql("'12.00'");

                entity.Property(e => e.TamanoFuenteNombreImpresionSobreVentaParametro)
                    .HasColumnType("double(5,2)")
                    .HasDefaultValueSql("'1.50'");

                entity.Property(e => e.TamanoFuenteNombreProveedorImpresionCheque)
                    .HasColumnType("decimal(5,2)")
                    .HasDefaultValueSql("'2.10'");

                entity.Property(e => e.TamanoFuenteRideFacturaParametro)
                    .HasColumnType("double(10,2)")
                    .HasDefaultValueSql("'6.00'");

                entity.Property(e => e.TamanoFuenteRideGuiaParametro)
                    .HasColumnType("double(5,2)")
                    .HasDefaultValueSql("'6.00'");

                entity.Property(e => e.TamanoFuenteRideLiquidacionCompraParametro)
                    .IsRequired()
                    .HasColumnType("varchar(250)")
                    .HasDefaultValueSql("'arial.ttf'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TamanoFuenteRideNotaCreditoVentaParametro)
                    .HasColumnType("double(5,2)")
                    .HasDefaultValueSql("'6.00'");

                entity.Property(e => e.TamanoFuenteRideRetencionParametro)
                    .HasColumnType("decimal(5,2)")
                    .HasDefaultValueSql("'6.00'");

                entity.Property(e => e.TasaArancelariaParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TextoLeyendaAhorroTotalPrecioMarcadoParametro)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("'Su descuento en esta compra fué {0} ({1}%)'")
                    .HasComment(@"{0} valor efectivo
{1} valor en porcentaje")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Textocartavacacionparametro)
                    .IsRequired()
                    .HasColumnName("textocartavacacionparametro")
                    .HasColumnType("text")
                    .HasComment("texto de la carta de solicitud de vacaciones.")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TiempoEsperaHoraExtraParametro).HasColumnType("int(11)");

                entity.Property(e => e.TiempoEsperaRetrazoParametro).HasColumnType("int(11)");

                entity.Property(e => e.TieneCargaInicialChequesParametro)
                    .HasColumnType("int(1)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.TipoAjusteTomaInventarioParametro)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'4444'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TipoAsientoRolOtraBaseParametro)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TipoGastoSolicitarCotizacionParamentro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ToleranciaRedondeoOrdenesParametro).HasColumnType("double(16,4)");

                entity.Property(e => e.TransportistaDefectoGuiasRemisionParametro)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.UbicacionFirmaElectronicaParametro)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("'C:\\\\inetpub\\\\wwwroot\\\\Wise\\\\files\\\\FirmaElectronica\\\\daniel_ernesto_toral_valdivieso.p12'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.UnirPrefacturasEnPagoParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.UrlBaseRecursosImpresionParametro)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.UrlpublicaSistema)
                    .IsRequired()
                    .HasColumnName("URLpublicaSistema")
                    .HasColumnType("varchar(30)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.UsarCuentaTransitoriaDebitoParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.UsarDescuentosTotalesOrdenParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasComment("Parámetro para visualizar los descuentos aprovados por promoción y sacar un descuento general de la orden para la secuencia de autorización.")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.UsarFirmaElectronicaEncriptadaParametro).HasColumnType("int(1)");

                entity.Property(e => e.UsarLectorBarrasNativoFacturaParametro).HasColumnType("int(1)");

                entity.Property(e => e.UsarPlantillaCuentasContablesEmpleadoParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.UsarTablaIngresoBonosParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.UsuarioGestionRouterParametro)
                    .IsRequired()
                    .HasColumnType("varchar(30)")
                    .HasComment("Se usa para registro de usuario admin para envío de comandos desde contrato")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.UsuarioIngresaVacacionesParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.UsuariosActivosParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'1'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.UtilizarBdimpresionParametro)
                    .IsRequired()
                    .HasColumnName("UtilizarBDImpresionParametro")
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.VacioCuentaEfectivoParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ValidarAnulacionCompraParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'1'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ValidarMarcacionParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'1'")
                    .HasComment("indica si la empresa realiza la validacion de la marcacion de los empleados.")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ValidarPuntoFacturaProveedorParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'1'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ValidarSubtotalesNccomprasParametro)
                    .HasColumnName("ValidarSubtotalesNCComprasParametro")
                    .HasColumnType("int(1)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.ValorAbsolutoListaPrecioParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ValorAplicadoPromocionCorteParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'1'")
                    .HasComment("En cortes a prefacturas, el precio se reduce al parcial de la fecha de cambio, la valor base para este cálculo si está en 1 calcula con precio negociado (con promoción) en 0 precio de venta de producto")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ValorDefectoCupoClienteParametro).HasColumnType("double(6,2)");

                entity.Property(e => e.ValorIgualacionConciliacionParametro).HasColumnType("decimal(16,4)");

                entity.Property(e => e.ValorIsdparametro)
                    .HasColumnName("ValorISDParametro")
                    .HasColumnType("double(16,2)")
                    .HasDefaultValueSql("'1200.00'");

                entity.Property(e => e.ValorMaximoVentaConsumidorFinalParametro)
                    .HasColumnType("decimal(16,4)")
                    .HasDefaultValueSql("'200.0000'");

                entity.Property(e => e.ValorPagoDecimoCuartoParametro).HasColumnType("double(16,4)");

                entity.Property(e => e.ValorRecargoDevolucionChequeParametro).HasColumnType("double(16,4)");

                entity.Property(e => e.ValorRecargoProtestoParametro)
                    .HasColumnType("double(16,4)")
                    .HasDefaultValueSql("'3.0000'");

                entity.Property(e => e.ValorVariacionIvaParametro).HasColumnType("double(16,4)");

                entity.Property(e => e.ValorVariacionRetencionParametro)
                    .HasColumnType("double(16,4)")
                    .HasComment("variacion del valor de la retencion.");

                entity.Property(e => e.VariosCodigosCompraParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'1'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.VencimientoMaximoParametro)
                    .HasColumnType("varchar(255)")
                    .HasComment("Almacena el numero de dias maximo de vencimiento en ventas")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.VendedorDefectoCargarClientesCorporativosParametro)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.VendedorDefectoProtestoDevolucionChpCarIniParametro)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.VerObservacionBuscadorFacturacClientesParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'1'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.VerPrecioOrdenPedidoParametro)
                    .HasColumnType("tinyint(3)")
                    .HasDefaultValueSql("'1'")
                    .HasComment("Verifica si se ve el precio de venta en la orden de pedido");

                entity.Property(e => e.VerPrecioPactadoVentaParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.VerPrecioTrabajoLaboratorioParametro)
                    .HasColumnType("tinyint(3)")
                    .HasComment("Verifica si se ve el precio de venta en los trabajos de laboratorio");

                entity.Property(e => e.VerPrecioTransferenciaParametro)
                    .HasColumnType("tinyint(3)")
                    .HasDefaultValueSql("'1'")
                    .HasComment("Verifica si se muestra el precio de venta en las transferencias");

                entity.Property(e => e.VerResumenOrdenPedidoParametro)
                    .HasColumnType("tinyint(3)")
                    .HasDefaultValueSql("'1'")
                    .HasComment("Verifica si se muestra un resumen de existencias en la orden de pedido");

                entity.Property(e => e.VerSucursalIngresoOrdenServicioTecnico).HasColumnType("int(1)");

                entity.Property(e => e.VerUtilidadFacturaSimpleParametro).HasColumnType("int(1)");

                entity.Property(e => e.VerificaNumTransferenciaRegistradaParametro)
                    .HasColumnType("int(1)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.VerificarBloqueoFormularioParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'1'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.VerificarDepositoCajaParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.VerificarDescuentoRestarTarjetaCreditoVentaParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.VerificarFacturaMismaEmpresaParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'1'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.VerificarFirmaElectronicaParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.VerificarNoAplicaClienteFactura)
                    .IsRequired()
                    .HasColumnType("varchar(2)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.VerificarNoAplicaClienteOrdenes)
                    .IsRequired()
                    .HasColumnType("varchar(2)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.VerificarNoAplicaIngresoClienteParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.VerificarNoAplicaProductoFactura)
                    .IsRequired()
                    .HasColumnType("varchar(2)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.VerificarNoAplicaProductoOrdenes)
                    .IsRequired()
                    .HasColumnType("varchar(2)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.VerificarPendientesLiquidacionProduccionParametro)
                    .HasColumnType("int(1)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.VerificarPlazosPorLineaFacturaParametro)
                    .HasColumnType("int(1)")
                    .HasComment("Verifica si se usan los plazos por linea o por la solicitud de credito");

                entity.Property(e => e.VerificarPlazosPorLineaOrdenParametro)
                    .HasColumnType("int(1)")
                    .HasComment("Verifica si se usan los plazos por linea o por la solicitud de credito");

                entity.Property(e => e.VerificarPlazosProductoXlineaFacturaParametro).HasColumnType("int(1)");

                entity.Property(e => e.VerificarPlazosProductoXlineaOrdenParametro).HasColumnType("int(1)");

                entity.Property(e => e.VerificarValorAprobadoOrpparametro)
                    .IsRequired()
                    .HasColumnName("VerificarValorAprobadoORPParametro")
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'1'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.VisualizaValorConsignacionClienteParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.VisualizacionReportesVendedor).HasColumnType("int(1)");

                entity.Property(e => e.VisualizarCarteraDeudaMesActualParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.VisualizarClienteReporteProtestosParametro)
                    .HasColumnType("int(1)")
                    .HasComment("Visualiza el cliente en el reporte de protestos, Agrosad pidio, no debe ir activo debido a que el reporte parte de una cabecera y se puede protestar varios cheques o clientes en el mismo protesto.");

                entity.Property(e => e.VisualizarDescuentosTotalesOrdenParametro)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasComment("Parámetro para visualizar los descuentos aprovados por promoción y sacar un descuento general de la orden para la secuencia de autorización.")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.VisualizarExistenciaTodasBodegasFacturaParametro).HasColumnType("int(1)");

                entity.Property(e => e.VisualizarNumChequeReporteProtestosParametro)
                    .HasColumnType("int(1)")
                    .HasComment("Visualiza el cliente en el reporte de protestos, Agrosad pidio, no debe ir activo debido a que el reporte parte de una cabecera y se puede protestar varios cheques o clientes en el mismo protesto.");

                entity.Property(e => e.VizualizarEnCarteraNumeroChequeProtestadoParametro)
                    .HasColumnType("int(1)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.VizualizarEnPagosClienteNumeroChequeProtestadoParametro).HasColumnType("int(1)");

                entity.Property(e => e.WsxadisParametro)
                    .HasColumnName("WSXadisParametro")
                    .HasColumnType("int(1)");

                entity.HasOne(d => d.CategoriaDocumentoLlamadaCallCenterParametroNavigation)
                    .WithMany(p => p.Parametros)
                    .HasForeignKey(d => d.CategoriaDocumentoLlamadaCallCenterParametro)
                    .HasConstraintName("FK_CategoriaDocumentoInstalacion_Llamada");

                entity.HasOne(d => d.TipoAjusteTomaInventarioParametroNavigation)
                    .WithMany(p => p.Parametros)
                    .HasPrincipalKey(p => p.CodigoTipoAjuste)
                    .HasForeignKey(d => d.TipoAjusteTomaInventarioParametro)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKTipoAjusteTomaInventarioParametro");
            });

            modelBuilder.Entity<Perfiles>(entity =>
            {
                entity.HasKey(e => e.CodigoPerfil)
                    .HasName("PRIMARY");

                entity.ToTable("perfiles");

                entity.Property(e => e.CodigoPerfil)
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasComment("Codigo de Perfil;text;true;false;Datos;180;left")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NombrePerfil)
                    .IsRequired()
                    .HasColumnType("varchar(60)")
                    .HasDefaultValueSql("''")
                    .HasComment("Nombre de Perfil;text;true;true;Datos;180;left")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");
            });

            modelBuilder.Entity<Permisossucursalagendar>(entity =>
            {
                entity.HasKey(e => e.CodigoPermisoSucursalAgendar)
                    .HasName("PRIMARY");

                entity.ToTable("permisossucursalagendar");

                entity.HasIndex(e => e.EmpleadosPermisoSucursalAgendar)
                    .HasName("FK_empleados_PermisoSucursalAgendar");

                entity.HasIndex(e => e.SucursalesPermisoSucursalAgendar)
                    .HasName("FK_sucursales_PermisoSucursalAgendar");

                entity.Property(e => e.CodigoPermisoSucursalAgendar)
                    .HasColumnName("codigoPermisoSucursalAgendar")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.EmpleadosPermisoSucursalAgendar)
                    .IsRequired()
                    .HasColumnName("empleadosPermisoSucursalAgendar")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.SucursalesPermisoSucursalAgendar)
                    .IsRequired()
                    .HasColumnName("sucursalesPermisoSucursalAgendar")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.HasOne(d => d.EmpleadosPermisoSucursalAgendarNavigation)
                    .WithMany(p => p.Permisossucursalagendar)
                    .HasForeignKey(d => d.EmpleadosPermisoSucursalAgendar)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_empleados_PermisoSucursalAgendar");

                entity.HasOne(d => d.SucursalesPermisoSucursalAgendarNavigation)
                    .WithMany(p => p.Permisossucursalagendar)
                    .HasForeignKey(d => d.SucursalesPermisoSucursalAgendar)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_sucursales_PermisoSucursalAgendar");
            });

            modelBuilder.Entity<Relacionrepresentantepaciente>(entity =>
            {
                entity.HasKey(e => e.CodigoRelacionRepresentantePaciente)
                    .HasName("PRIMARY");

                entity.ToTable("relacionrepresentantepaciente");

                entity.Property(e => e.CodigoRelacionRepresentantePaciente).HasColumnType("int(2)");

                entity.Property(e => e.DescripcionRelacionRepresentantePaciente)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.UsuariosRelacionRepresentantePaciente)
                    .IsRequired()
                    .HasColumnName("usuariosRelacionRepresentantePaciente")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");
            });

            modelBuilder.Entity<Solicitudcitasmedicas>(entity =>
            {
                entity.HasKey(e => e.CodigoSoliCitaMedica)
                    .HasName("PRIMARY");

                entity.ToTable("solicitudcitasmedicas");

                entity.HasIndex(e => e.CiudadesSoliCitaMedica)
                    .HasName("CiudadSolicitudFK");

                entity.HasIndex(e => e.RelacionesRepresentantePacienteSoliCitaMedica)
                    .HasName("FK_RelacionReprePaciente_SolicitudCitaMedica");

                entity.HasIndex(e => e.SubCampaniasOrigen)
                    .HasName("SubcampaniaOrigenFK_idx");

                entity.HasIndex(e => e.SucursalesSoliCitaMedica)
                    .HasName("FK_Sucursal_SolicitudCitaMedica");

                entity.Property(e => e.CodigoSoliCitaMedica)
                    .HasColumnType("varchar(22)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ApellidoClienteSoliCitaMedica)
                    .IsRequired()
                    .HasColumnType("varchar(60)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ApellidoRepresentanteSoliCitaMedica)
                    .IsRequired()
                    .HasColumnType("varchar(60)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CelularClienteSoliCitaMedica)
                    .IsRequired()
                    .HasColumnType("varchar(10)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CelularRepresentanteSoliCitaMedica)
                    .IsRequired()
                    .HasColumnType("varchar(10)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CiudadesSoliCitaMedica)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.EmailClienteSoliCitaMedica)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.EmailRepresentanteSoliCitaMedica)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.EsPacienteSoliCitaMedica).HasColumnType("int(1)");

                entity.Property(e => e.EstadoSoliCitaMedica).HasColumnType("int(1)");

                entity.Property(e => e.FechaNacimientoClienteSoliCitaMedica).HasColumnType("date");

                entity.Property(e => e.FechaRegistroSoliCitaMedica).HasColumnType("datetime");

                entity.Property(e => e.FechaTentativaSoliCitaMedica).HasColumnType("datetime");

                entity.Property(e => e.GeneroClienteSoliCitaMedica)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NombreClienteSoliCitaMedica)
                    .IsRequired()
                    .HasColumnType("varchar(60)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NombreRepresentanteSoliCitaMedica)
                    .IsRequired()
                    .HasColumnType("varchar(60)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ObservacionSoliCitaMedica)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.RelacionesRepresentantePacienteSoliCitaMedica)
                    .HasColumnType("int(2)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.SubCampaniasOrigen)
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.SucursalesSoliCitaMedica)
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.HasOne(d => d.CiudadesSoliCitaMedicaNavigation)
                    .WithMany(p => p.Solicitudcitasmedicas)
                    .HasForeignKey(d => d.CiudadesSoliCitaMedica)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CiudadSolicitudFK");

                entity.HasOne(d => d.RelacionesRepresentantePacienteSoliCitaMedicaNavigation)
                    .WithMany(p => p.Solicitudcitasmedicas)
                    .HasForeignKey(d => d.RelacionesRepresentantePacienteSoliCitaMedica)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RelacionReprePaciente_SolicitudCitaMedica");

                entity.HasOne(d => d.SubCampaniasOrigenNavigation)
                    .WithMany(p => p.Solicitudcitasmedicas)
                    .HasForeignKey(d => d.SubCampaniasOrigen)
                    .HasConstraintName("SubcampaniaOrigen_SoliCitaMedica_FK");

                entity.HasOne(d => d.SucursalesSoliCitaMedicaNavigation)
                    .WithMany(p => p.Solicitudcitasmedicas)
                    .HasForeignKey(d => d.SucursalesSoliCitaMedica)
                    .HasConstraintName("FK_Sucursal_SolicitudCitaMedica");
            });

            modelBuilder.Entity<Subcampanias>(entity =>
            {
                entity.HasKey(e => e.CodigoSubCampania)
                    .HasName("PRIMARY");

                entity.ToTable("subcampanias");

                entity.HasIndex(e => e.CampaniasSubCampania)
                    .HasName("SubCampanias_fk0");

                entity.Property(e => e.CodigoSubCampania)
                    .HasColumnType("varchar(22)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CampaniasSubCampania)
                    .IsRequired()
                    .HasColumnType("varchar(22)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.DescripcionSubCampania)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.FechaFinSubCampania).HasColumnType("datetime");

                entity.Property(e => e.FechaInicioSubCampania).HasColumnType("datetime");

                entity.Property(e => e.ImagenSubCampania)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.HasOne(d => d.CampaniasSubCampaniaNavigation)
                    .WithMany(p => p.Subcampanias)
                    .HasForeignKey(d => d.CampaniasSubCampania)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SubCampanias_fk0");
            });

            modelBuilder.Entity<Sucursales>(entity =>
            {
                entity.HasKey(e => e.CodigoSucursal)
                    .HasName("PRIMARY");

                entity.ToTable("sucursales");

                entity.HasComment("Sucursal;NombreSucursal");

                entity.HasIndex(e => e.CiudadesSucursal)
                    .HasName("CiudadFK");

                entity.HasIndex(e => e.CodigoSucursal)
                    .HasName("CodigoSucursal")
                    .IsUnique();

                entity.HasIndex(e => e.PaisSucursal)
                    .HasName("FK_Pais_Sucursales");

                entity.HasIndex(e => e.ParroquiaSucursal)
                    .HasName("FK_Parroquias_Sucursal");

                entity.HasIndex(e => e.ProvinciaSucursal)
                    .HasName("FK_Provincia_Sucursal");

                entity.HasIndex(e => new { e.EmpresasSucursal, e.NombreSucursal })
                    .HasName("Index_NombreSucursal")
                    .IsUnique();

                entity.Property(e => e.CodigoSucursal)
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasComment("Codigo WISE de la sucursal;text;true;false;Datos;180;left")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ActivaAgendamientoSucursal).HasColumnType("int(1)");

                entity.Property(e => e.ActivaSucursal)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasComment("Desactivada Sucursal;checkbox;true;true;Datos;180;left")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CiudadesSucursal)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'091029152528233'")
                    .HasComment("Ciudad a la que pertenece la sucursal;combo;true;true;Datos;180;left;Select CodigoCiudad,NombreCiudad from ciudades ORDER BY NombreCiudad")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.DireccionSucursal)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("''")
                    .HasComment("Direccion Sucursal;text;true;true;Datos;300;left")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.EmpresasSucursal)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasComment("Empresa a la que pertenece la sucursal;combo;true;true;Datos;180;left;Select CodigoEmpresa,NombreEmpresa from Empresas")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.EstablecimientoRdepsucursal)
                    .IsRequired()
                    .HasColumnName("EstablecimientoRDEPSucursal")
                    .HasColumnType("varchar(3)")
                    .HasDefaultValueSql("'001'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.FaxSucursal)
                    .IsRequired()
                    .HasColumnType("varchar(14)")
                    .HasDefaultValueSql("''")
                    .HasComment("Fax Sucursal;text;true;true;Datos;180;left")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.FechaModificacionSucursal)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'1990-01-01 00:00:00'");

                entity.Property(e => e.FechaRegistroSucursal)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'1990-01-01 00:00:00'");

                entity.Property(e => e.LatitudSucursal)
                    .IsRequired()
                    .HasColumnType("varchar(25)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.LongitudSucursal)
                    .IsRequired()
                    .HasColumnType("varchar(25)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.MatrizSucursal)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.MidRecargasSucursal)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'015912000100004'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NombreSucursal)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("''")
                    .HasComment("Nombre Sucursal;text;true;true;Datos;180;left")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.PaisSucursal)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ParroquiaSucursal)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ProvinciaSucursal)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TelefonoDosSucursal)
                    .IsRequired()
                    .HasColumnType("varchar(14)")
                    .HasDefaultValueSql("''")
                    .HasComment("Telefono Dos Sucursal;text;true;true;Datos;180;left")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TelefonoUnoSucursal)
                    .IsRequired()
                    .HasColumnType("varchar(14)")
                    .HasDefaultValueSql("''")
                    .HasComment("Telefono Uno Sucursal;text;true;true;Datos;180;left")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.UsuarioModificaSucursal)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.UsuariosRegistraSucursal)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ZoomUbicacionMapaSucursal).HasColumnType("tinyint(5)");

                entity.HasOne(d => d.CiudadesSucursalNavigation)
                    .WithMany(p => p.Sucursales)
                    .HasForeignKey(d => d.CiudadesSucursal)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Canton_Sucursal");

                entity.HasOne(d => d.EmpresasSucursalNavigation)
                    .WithMany(p => p.Sucursales)
                    .HasForeignKey(d => d.EmpresasSucursal)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("EmpresaFK");

                entity.HasOne(d => d.PaisSucursalNavigation)
                    .WithMany(p => p.Sucursales)
                    .HasForeignKey(d => d.PaisSucursal)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pais_Sucursales");

                entity.HasOne(d => d.ParroquiaSucursalNavigation)
                    .WithMany(p => p.Sucursales)
                    .HasForeignKey(d => d.ParroquiaSucursal)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Parroquias_Sucursal");

                entity.HasOne(d => d.ProvinciaSucursalNavigation)
                    .WithMany(p => p.Sucursales)
                    .HasForeignKey(d => d.ProvinciaSucursal)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Provincia_Sucursal");
            });

            modelBuilder.Entity<Tiposagendas>(entity =>
            {
                entity.HasKey(e => e.CodigoTipoAgenda)
                    .HasName("PRIMARY");

                entity.ToTable("tiposagendas");

                entity.Property(e => e.CodigoTipoAgenda).HasColumnType("int(1)");

                entity.Property(e => e.DescripcionTipoAgenda)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");
            });

            modelBuilder.Entity<Tiposajustes>(entity =>
            {
                entity.HasKey(e => new { e.CodigoTipoAjuste, e.NombreTipoAjuste })
                    .HasName("PRIMARY");

                entity.ToTable("tiposajustes");

                entity.HasComment("Tipos de Ajustes de Inventarios;NombreTipoAjuste");

                entity.HasIndex(e => e.CodigoTipoAjuste)
                    .HasName("Codigo")
                    .IsUnique();

                entity.HasIndex(e => e.CuentaContableTipoAjuste)
                    .HasName("FKCuentaContableTipoAjuste");

                entity.HasIndex(e => e.NombreTipoAjuste)
                    .HasName("NombreTipoAjuste")
                    .IsUnique();

                entity.Property(e => e.CodigoTipoAjuste)
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasComment("Codigo TipoAjuste;text;true;false;Datos;180;left")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NombreTipoAjuste)
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("''")
                    .HasComment("Nombre TipoAjuste;text;true;true;Datos;180;left")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.AfectaCostoTipoAjuste)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasComment("Afecta al Costo Tipo Ajuste;chk;true;true;Datos;180;left")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CuentaContableTipoAjuste)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'0'")
                    .HasComment("Cuenta Contable TipoAjuste;cmb;true;true;Datos;auto;left;Select CodigoCuentaContable, concat(NombreCuentaContable,CodigoCuentaContable) from cuentascontabilidad Where length(CodigoCuentaContable)=(select DigitosUltimoNivel from nivelesplancuentas)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.UsuariosTipoAjuste)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.HasOne(d => d.CuentaContableTipoAjusteNavigation)
                    .WithMany(p => p.Tiposajustes)
                    .HasForeignKey(d => d.CuentaContableTipoAjuste)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKCuentaContableTipoAjuste");
            });

            modelBuilder.Entity<Tiposclientescartera>(entity =>
            {
                entity.HasKey(e => e.CodTipoClienteCartera)
                    .HasName("PRIMARY");

                entity.ToTable("tiposclientescartera");

                entity.HasIndex(e => e.CodTipoClienteCartera)
                    .HasName("CodTipoClienteCartera");

                entity.Property(e => e.CodTipoClienteCartera)
                    .HasColumnType("varchar(30)")
                    .HasDefaultValueSql("''")
                    .HasComment("Codigo Tipo Cliente Cartera;text;true;false;Datos;180;left")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.DescripcionTipoClienteCartera)
                    .IsRequired()
                    .HasColumnType("varchar(30)")
                    .HasComment("Descripción Tipo Cliente Cartera;text;true;true;Datos;180;left")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.UsuariosTipoClienteCartera)
                    .IsRequired()
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");
            });

            modelBuilder.Entity<Tiposdocumentosinstalaciones>(entity =>
            {
                entity.HasKey(e => e.CodigoTipoDocumentoInstalacion)
                    .HasName("PRIMARY");

                entity.ToTable("tiposdocumentosinstalaciones");

                entity.Property(e => e.CodigoTipoDocumentoInstalacion)
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.BloqueadoTipoDocumentoInstalacion)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'1'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.DescripcionTipoDocumentoInstalacion)
                    .IsRequired()
                    .HasColumnType("varchar(60)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.HabilitadoTipoDocumentoInstalacion)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.SecuenciaTipoDocumentoInstalacion)
                    .HasColumnType("int(20)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.UsuariosTipoDocumentoInstalacion)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");
            });

            modelBuilder.Entity<Tiposempleados>(entity =>
            {
                entity.HasKey(e => e.CodigoTipoEmpleado)
                    .HasName("PRIMARY");

                entity.ToTable("tiposempleados");

                entity.HasComment("TipoEmpleado;NombreTipoEmpleado");

                entity.HasIndex(e => e.CodigoTipoEmpleado)
                    .HasName("Index_CodigoTipoEmpleado")
                    .IsUnique();

                entity.HasIndex(e => e.NombreTipoEmpleado)
                    .HasName("Index_Nombres");

                entity.Property(e => e.CodigoTipoEmpleado)
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasComment("Codigo Tipo Empleado;text;true;false;Datos;180;left")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.EsVendedorTipoEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'1'")
                    .HasComment("Visualiza en listado de vendedores;checkbox;true;true;Datos;180;left")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NombreTipoEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("''")
                    .HasComment("Nombre Tipo Empleado;text;true;true;Datos;180;left")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.UsuariosTipoEmpleado)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");
            });

            modelBuilder.Entity<Tiposfinalizacioncallcenter>(entity =>
            {
                entity.HasKey(e => e.CodigoTipoFinalizacionCallCenter)
                    .HasName("PRIMARY");

                entity.ToTable("tiposfinalizacioncallcenter");

                entity.Property(e => e.CodigoTipoFinalizacionCallCenter)
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.DescripcionTipoFinalizacionCallCenter)
                    .IsRequired()
                    .HasColumnType("varchar(200)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.GeneraDocPendienteTipoFinalizacionCallCenter).HasColumnType("int(1)");

                entity.Property(e => e.UsuariosTipoFinalizacionCallCenter)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");
            });

            modelBuilder.Entity<Tiposidentificacion>(entity =>
            {
                entity.HasKey(e => e.CodigoTipoIdentificacion)
                    .HasName("PRIMARY");

                entity.ToTable("tiposidentificacion");

                entity.HasComment("TipoIdentificacion;NombreTipoIdentificacion");

                entity.HasIndex(e => e.CodigoTipoIdentificacion)
                    .HasName("CodigoTipoIdentificacion");

                entity.HasIndex(e => e.NombreTipoIdentificacion)
                    .HasName("Index_Nombre");

                entity.Property(e => e.CodigoTipoIdentificacion)
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasComment("Codigo Tipo Identificacion;text;true;false;Datos;180;left")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CodigoSrianexoTipoIdentificacion)
                    .IsRequired()
                    .HasColumnName("CodigoSRIAnexoTipoIdentificacion")
                    .HasColumnType("varchar(2)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CodigoSricompraTipoIdentificacion)
                    .IsRequired()
                    .HasColumnName("CodigoSRICompraTipoIdentificacion")
                    .HasColumnType("varchar(2)")
                    .HasDefaultValueSql("'01'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CodigoSriventaTipoIdentificacion)
                    .IsRequired()
                    .HasColumnName("CodigoSRIVentaTipoIdentificacion")
                    .HasColumnType("varchar(2)")
                    .HasDefaultValueSql("'04'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NombreTipoIdentificacion)
                    .IsRequired()
                    .HasColumnType("varchar(60)")
                    .HasDefaultValueSql("''")
                    .HasComment("Nombre Tipo Identificacion;text;true;true;Datos;180;left")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NumeroCaracterTipoIdentificacion)
                    .HasColumnType("int(10) unsigned")
                    .HasComment("#Caracteres Tipo Identificacion;text;true;false;Datos;180;left");

                entity.Property(e => e.UsuariosTipoIdentificacion)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");
            });

            modelBuilder.Entity<Titulos>(entity =>
            {
                entity.HasKey(e => e.CodigoTitulo)
                    .HasName("PRIMARY");

                entity.ToTable("titulos");

                entity.HasComment("Titulo;NombreTitulo");

                entity.HasIndex(e => e.NombreTitulo)
                    .HasName("Index_Nombre")
                    .IsUnique();

                entity.Property(e => e.CodigoTitulo)
                    .HasColumnType("varchar(20)")
                    .HasComment("Codigo Titulo;text;true;false;datos;120;left")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NombreTitulo)
                    .IsRequired()
                    .HasColumnType("varchar(60)")
                    .HasComment("Nombre Titulo;text;true;true;datos;200;left")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.UsuariosTitulo)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");
            });

            modelBuilder.Entity<Transportes>(entity =>
            {
                entity.HasKey(e => e.CodigoTransporte)
                    .HasName("PRIMARY");

                entity.ToTable("transportes");

                entity.HasIndex(e => e.CodigoTransporte)
                    .HasName("CodigoTransporte")
                    .IsUnique();

                entity.Property(e => e.CodigoTransporte)
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasComment("Codigo de Transporte;text;true;false;Datos;120;left")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.HabilitadoTransporte)
                    .IsRequired()
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'1'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NombreTransporte)
                    .IsRequired()
                    .HasColumnType("varchar(150)")
                    .HasDefaultValueSql("''")
                    .HasComment("Nombre de Transporte;text;true;true;Datos;180;left")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ParametrosTransporte)
                    .IsRequired()
                    .HasColumnName("parametrosTransporte")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.RucTransporte)
                    .IsRequired()
                    .HasColumnType("varchar(13)")
                    .HasDefaultValueSql("''")
                    .HasComment("RUC;text;true;true;Datos;180;left")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TelefonoTransporte)
                    .IsRequired()
                    .HasColumnType("varchar(12)")
                    .HasDefaultValueSql("''")
                    .HasComment("Telefono de Transporte;text;true;true;Datos;180;left")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TieneApiTransporte).HasColumnType("int(1)");

                entity.Property(e => e.UsuariosTipoAsiento)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'fmaldonado'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");
            });
        }
    }
}
