using System.Collections.Generic;

namespace Wizzi.Entities
{
    public partial class Nivelesprioridadprocesos
    {
        public Nivelesprioridadprocesos()
        {
            Instalacionescabecera = new HashSet<Instalacionescabecera>();
        }

        public int CodigoNivelPrioridadProcesos { get; set; }
        public string DescripcionNivelPrioridadProcesos { get; set; }

        public virtual ICollection<Instalacionescabecera> Instalacionescabecera { get; set; }
    }
}
