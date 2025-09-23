using System;
using System.Collections.Generic;

namespace Mitologia.Models.Entities;

public partial class CivilizacionDios
{
    public int Id { get; set; }

    public int IdCivilizacion { get; set; }

    public int IdDios { get; set; }

    public string NombreLocal { get; set; } = null!;

    public virtual Civilizaciones IdCivilizacionNavigation { get; set; } = null!;

    public virtual Dioses IdDiosNavigation { get; set; } = null!;
}
