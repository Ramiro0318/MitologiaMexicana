using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mitologia.Models.Entities;
using Mitologia.Models.ViewModels;
using System.Linq;
using static Mitologia.Models.ViewModels.CivilizacionesViewModel;
using static Mitologia.Models.ViewModels.DiosesViewModel;
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

            vm.CivilizacionesIndex = context.Civilizaciones.OrderBy(y => y.IdCivilizacion)
                .Select(y => new IndexViewModel.CivilizacionModel
                {
                    IdCivilizacion = y.IdCivilizacion,
                    Nombre = y.Nombre,
                    Descripcion = y.Descripcion
                }
                ).OrderBy(x => EF.Functions.Random()).Take(3);

            int max = vm.CivilizacionesIndex.Count();

            //vm.CivilizacionesIndex = vm.CivilizacionesIndex
            //.OrderBy(x => r.Next(0, max))
            //.Take(3);

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
     
        public IActionResult Dioses(string? Id)
        {
            MitologiaMexicanaContext context = new();

            var data = context.Civilizaciones
            .Select(c => new
            {
                c.IdCivilizacion,
                c.Nombre,
                Dioses = c.CivilizacionDios.Where( Id != null ? x=> c.Nombre == Id : x => c.Nombre == context.Civilizaciones.OrderBy(c => c.IdCivilizacion)
                .First().Nombre).Select(cd => new
                {
                    cd.Id,
                    cd.NombreLocal,
                    cd.IdDiosNavigation.NombreGeneral,
                    cd.IdDiosNavigation.Genero,
                    cd.IdDiosNavigation.Dominio,
                    cd.IdDiosNavigation.Descripcion
                })
            }).ToList();

            var vm = new DiosesViewModel
            {
                Civilizaciones = data.Select(c => new Models.ViewModels.CivilizacionModel
                {
                    Nombre = c.Nombre

                })
            };

            var civ = data.FirstOrDefault(c => c.Nombre == Id);
            if (civ != null)
            {
                vm.NombreCivilizacion = civ.Nombre;
                vm.CivilizacionDioses = civ.Dioses.Select(cd => new CivilizacionDiosModel
                {
                    Id = cd.Id,
                    NombreLocal = cd.NombreLocal
                });

                vm.Dioses = civ.Dioses.Select(cd => new DiosesModel
                {
                    Id = cd.Id,
                    NombreGeneral = cd.NombreGeneral,
                    Genero = cd.Genero,
                    Dominio = cd.Dominio,
                    Descripcion = cd.Descripcion
                });
            }
            return View(vm);
        }
    }
}
