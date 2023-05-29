namespace Wizzi.Entities
{
    public partial class Usuariosmedicos
    {
        public string UsuariosUsuarioMedico { get; set; }
        public string MedicosUsuarioMedico { get; set; }

        public virtual Medicos MedicosUsuarioMedicoNavigation { get; set; }
        public virtual Empleados UsuariosUsuarioMedicoNavigation { get; set; }
    }
}
