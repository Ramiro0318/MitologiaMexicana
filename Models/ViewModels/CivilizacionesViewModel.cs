using Mitologia.Models.Entities;
using System.Collections;

namespace Mitologia.Models.ViewModels
{
    public class CivilizacionesViewModel
    {
        public IEnumerable<CivilizacionDetalladaModel> Civilizaciones { get; set; } = null!;
        public class CivilizacionDetalladaModel  
        {
            public int IdCivilizacion { get; set; }

            public string Nombre { get; set; } = null!;
            public string Periodo
            {
                get
                {
                    return
                    $"{Math.Abs((decimal)PeriodoInicio)} {(PeriodoInicio <= 0 ? "a.C." : "d.C.")} – {Math.Abs((decimal)PeriodoFin)} {(PeriodoFin <= 0 ? "a.C." : "d.C.")}";
                }
                set { }
            }
            public int? PeriodoInicio { get; set; }

            public int? PeriodoFin { get; set; }

            public string? Region { get; set; }

            public string? Capital { get; set; }

            public string? Lengua { get; set; }

            public string? Descripcion { get; set; }

        }

    }
}
