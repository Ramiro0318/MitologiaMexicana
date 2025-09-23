using System;
using System.Collections.Generic;

namespace Mitologia.Models.Entities;

public partial class Civilizaciones
{
    public int IdCivilizacion { get; set; }

    public string Nombre { get; set; } = null!;

    public int? PeriodoInicio { get; set; }

    public int? PeriodoFin { get; set; }

    public string? Region { get; set; }

    public string? Capital { get; set; }

    public string? Lengua { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<CivilizacionDios> CivilizacionDios { get; set; } = new List<CivilizacionDios>();
}
