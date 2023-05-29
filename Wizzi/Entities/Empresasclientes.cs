namespace Wizzi.Entities
{
    public partial class Empresasclientes
    {
        public string EmpresasEmpresaCliente { get; set; }
        public string ClientesEmpresaCliente { get; set; }
        public string OmitirCupoEmpresaCliente { get; set; }
        public string OmitirDescuentoEmpresaCliente { get; set; }
        public string BodegasEmpresaCliente { get; set; }

        public virtual Clientes ClientesEmpresaClienteNavigation { get; set; }
        public virtual Empresas EmpresasEmpresaClienteNavigation { get; set; }
    }
}
