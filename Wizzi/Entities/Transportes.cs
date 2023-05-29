using System.Collections.Generic;

namespace Wizzi.Entities
{
    public partial class Transportes
    {
        public Transportes()
        {
            Clientes = new HashSet<Clientes>();
        }

        public string CodigoTransporte { get; set; }
        public string NombreTransporte { get; set; }
        public string TelefonoTransporte { get; set; }
        public string HabilitadoTransporte { get; set; }
        public string RucTransporte { get; set; }
        public int TieneApiTransporte { get; set; }
        public string ParametrosTransporte { get; set; }
        public string UsuariosTipoAsiento { get; set; }

        public virtual ICollection<Clientes> Clientes { get; set; }
    }
}
