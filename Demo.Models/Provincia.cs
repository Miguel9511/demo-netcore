using System;
using System.Collections.Generic;

namespace Demo.Models;

public partial class Provincia
{
    public int Id { get; set; }

    public int? IdDepartamento { get; set; }

    public string? NombreProvincia { get; set; }

    public virtual ICollection<Distrito> Distrito { get; set; } = new List<Distrito>();

    public virtual Departamento? IdDepartamentoNavigation { get; set; }

    public virtual ICollection<Trabajadores> Trabajadores { get; set; } = new List<Trabajadores>();
}
