using Microsoft.AspNetCore.Mvc;
using Mitologia.Models.Entities;
using Mitologia.Models.ViewModels;
using static Mitologia.Models.ViewModels.CivilizacionesViewModel;
using static Mitologia.Models.ViewModels.IndexViewModel;

namespace Mitologia.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            MitologiaMexicanaContext context = new();
            IndexViewModel vm = new();
            Random r = new();


            //vm.Civilizaciones = context.Civilizaciones.OrderBy(y => y.IdCivilizacion == r.Next(0, 9))
            //    .Select(y => new CivilizacionModel
            //    {
            //        IdCivilizacion = y.IdCivilizacion,
            //        Nombre = y.Nombre,
            //        Descripcion = y.Descripcion
            //    }
            //    );


            vm.CivilizacionesIndex = context.Civilizaciones.OrderBy(y => y.IdCivilizacion)
                .Select(y => new CivilizacionModel
                {
                    IdCivilizacion = y.IdCivilizacion,
                    Nombre = y.Nombre,
                    Descripcion = y.Descripcion
                }
                );

            int max = vm.CivilizacionesIndex.Count();

            vm.CivilizacionesIndex = vm.CivilizacionesIndex
            .OrderBy(x => r.Next(0, max))
            .Take(3);


            return View(vm);
        }

        public IActionResult Civilizaciones() 
        {
            MitologiaMexicanaContext context = new();
            CivilizacionesViewModel vm = new();

            vm.Civilizaciones = context.Civilizaciones.OrderBy(x => x.IdCivilizacion)
                .Select(x => new CivilizacionDetalladaModel
                {
                    IdCivilizacion = x.IdCivilizacion,
                    Nombre = x.Nombre,
                    PeriodoInicio = x.PeriodoInicio,
                    PeriodoFin = x.PeriodoFin,
                    Region = x.Region,
                    Capital = x.Capital,
                    Lengua = x.Lengua,
                    Descripcion = x.Descripcion,
                }
                );              
                

            return View(vm);
        }
    }
}
