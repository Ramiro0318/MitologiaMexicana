namespace Mitologia.Models.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<CivilizacionModel> CivilizacionesIndex { get; set; } = null!;
        public class CivilizacionModel
        {

            public int IdCivilizacion { get; set; }
            public string Nombre { get; set; } = null!;
            public string Descripcion { get; set; } = null!;
        }
    }
}
