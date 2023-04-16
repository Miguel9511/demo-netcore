namespace WebTrabajador.Models.ViewModels
{
    public class VMTrabajadores
    {
        public int Id { get; set; }

        public string? TipoDocumento { get; set; }

        public string? NumeroDocumento { get; set; }

        public string? Nombres { get; set; }

        public string? Sexo { get; set; }

        public VMSelect Departamento { get; set; }

        public VMSelect Provincia { get; set; }

        public VMSelect Distrito { get; set; }

        public VMTrabajadores()
        {
            this.Id = 0;
            this.TipoDocumento = String.Empty;
            this.NumeroDocumento = String.Empty;
            this.Nombres = String.Empty;
            this.Sexo = String.Empty;
            this.Departamento = new VMSelect();
            this.Provincia = new VMSelect();
            this.Distrito = new VMSelect();
        }
    }
}
