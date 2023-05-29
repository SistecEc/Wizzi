namespace Wizzi.Entities
{
    public partial class Clienteslocalizaciones
    {
        public string ClientesClienteLocalizacion { get; set; }
        public string PaisesClienteLocalizacion { get; set; }
        public string ProvinciasClienteLocalizacion { get; set; }
        public string CantonesClienteLocalizacion { get; set; }
        public string ParroquiasClienteLocalizacion { get; set; }

        public virtual Localizacionescantones CantonesClienteLocalizacionNavigation { get; set; }
        public virtual Clientes ClientesClienteLocalizacionNavigation { get; set; }
        public virtual Localizacionespaises PaisesClienteLocalizacionNavigation { get; set; }
        public virtual Localizacionesparroquias ParroquiasClienteLocalizacionNavigation { get; set; }
        public virtual Localizacionesprovincias ProvinciasClienteLocalizacionNavigation { get; set; }
    }
}
