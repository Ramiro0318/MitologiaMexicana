using Mitologia.Models.Entities;

namespace Mitologia.Models.ViewModels
{
    public class DiosesViewModel
    {
        public string NombreCivilizacion { get; set; } = null!;
        public IEnumerable<CivilizacionModel> Civilizaciones { get; set; } = null!;
        public IEnumerable<CivilizacionDiosModel>? CivilizacionDioses { get; set; }
        public IEnumerable<DiosesModel>? Dioses { get; set; }

    }
    public class CivilizacionDiosModel
    {
        public int Id { get; set; }
        public string NombreLocal { get; set; } = null!;
          
    }
    public class CivilizacionModel 
    {
        public int IdCivilizacion { get; set; }
        public string Nombre { get; set; } = null!;
           

    }
    public class DiosesModel 
    {
        public int Id { get; set; }
        public string NombreGeneral { get; set; } = null!;
        public string? Genero { get; set; }
        public string? Dominio { get; set; }
        public string? Descripcion { get; set; }

    }
}
