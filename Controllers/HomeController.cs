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


            //vm.Civilizaciones = context.Civilizaciones.OrderBy(y => y.IdCivilizacion == r.Next(0, 9))
            //    .Select(y => new CivilizacionModel
            //    {
            //        IdCivilizacion = y.IdCivilizacion,
            //        Nombre = y.Nombre,
            //        Descripcion = y.Descripcion
            //    }
            //    );


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

        //public IActionResult Dioses(string id)
        //{
        //    MitologiaMexicanaContext context = new();
        //    //DiosesViewModel vm = new();

        //    //var vm = context.Civilizaciones.Include(x => x.CivilizacionDios).ThenInclude(y => y.IdDiosNavigation)
        //    //    .Where(x => id == x.Nombre)
        //    //    .Select(x => new Civilizaciones
        //    //    {
        //    //        Nombre = x.Nombre,
        //    //        CivilizacionDios = x.CivilizacionDios.Select(y => new Dioses
        //    //        {
        //    //        }


        //    //        );

        //    //var vm = context.Civilizaciones.Include(c => c.CivilizacionDios)
        //    //    .ThenInclude(cd => cd.IdDiosNavigation)
        //    //    .Select(c => new DiosesViewModel.CivilizacionModel
        //    //    {
        //    //        IdCivilizacion = c.IdCivilizacion,
        //    //        Nombre = c.Nombre,
        //    //        Dioses = c.CivilizacionDios.Select(cd => new DiosesModel
        //    //        {
        //    //            Id = cd.IdDios,
        //    //            NombreGeneral = cd.IdDiosNavigation.NombreGeneral,
        //    //            Genero = cd.IdDiosNavigation.Genero,
        //    //            Dominio = cd.IdDiosNavigation.Dominio,
        //    //            Descripcion = cd.IdDiosNavigation.Descripcion,

        //    //        }).ToList()
        //    //    }).ToList();

        //    //           var vm = new DiosesViewModel
        //    //{
        //    //               CivilizacionDioses = context.CivilizacionDios
        //    //        .Include(cd => cd.IdCivilizacionNavigation)
        //    //        .Include(cd => cd.IdDiosNavigation)
        //    //        .Select(cd => new DiosesViewModel.CivilizacionDiosModel
        //    //        {
        //    //            Id = cd.Id,
        //    //            IdCivilizacion = cd.IdCivilizacion,
        //    //            IdDios = cd.IdDios,
        //    //            NombreLocal = cd.NombreLocal
        //    //        }).ToList(),

        //    //               Civilizaciones = context.CivilizacionDios
        //    //        .Include(cd => cd.IdCivilizacionNavigation)
        //    //        .Include(cd => cd.IdDiosNavigation)
        //    //        .GroupBy(cd => cd.IdCivilizacionNavigation)
        //    //        .Select(g => new DiosesViewModel.CivilizacionModel
        //    //        {
        //    //            IdCivilizacion = g.Key.IdCivilizacion,
        //    //            Nombre = g.Key.Nombre,

        //    //            Dioses = g.Select(cd => new DiosesViewModel.DiosesModel
        //    //            {
        //    //                Id = cd.IdDios,
        //    //                NombreGeneral = cd.IdDiosNavigation.NombreGeneral,
        //    //                Genero = cd.IdDiosNavigation.Genero,
        //    //                Dominio = cd.IdDiosNavigation.Dominio,
        //    //                Descripcion = cd.IdDiosNavigation.Descripcion,
        //    //                IdCivilizacionNavigation = cd.IdCivilizacionNavigation,
        //    //                IdDiosNavigation = cd.IdDiosNavigation
        //    //            }).ToList()
        //    //        }).ToList()
        //    //};


        //    var vm = new DiosesViewModel
        //    {
        //        CivilizacionDioses = context.CivilizacionDios
        //            .Where(cd => cd.IdCivilizacionNavigation.Nombre == id)
        //            .Select(cd => new DiosesViewModel.CivilizacionDiosModel
        //            {
        //                Id = cd.Id,
        //                IdCivilizacion = cd.IdCivilizacion,
        //                IdDios = cd.IdDios,
        //                NombreLocal = cd.NombreLocal
        //            }).ToList(),

        //        Civilizaciones = context.CivilizacionDios

        //            .GroupBy(cd => new
        //            {
        //                cd.IdCivilizacionNavigation.IdCivilizacion,
        //                cd.IdCivilizacionNavigation.Nombre
        //            })
        //            .Select(g => new DiosesViewModel.CivilizacionModel
        //            {
        //                IdCivilizacion = g.Key.IdCivilizacion,
        //                Nombre = g.Key.Nombre,
        //                Dioses = g.Select(cd => new DiosesViewModel.DiosesModel
        //                {
        //                    Id = cd.IdDios,
        //                    NombreGeneral = cd.IdDiosNavigation.NombreGeneral,
        //                    Genero = cd.IdDiosNavigation.Genero,
        //                    Dominio = cd.IdDiosNavigation.Dominio,
        //                    Descripcion = cd.IdDiosNavigation.Descripcion,
        //                    IdCivilizacionNavigation = cd.IdCivilizacionNavigation,
        //                    IdDiosNavigation = cd.IdDiosNavigation
        //                }).ToList()
        //            }).ToList()
        //    };


        //    //vm.CivilizacionDioses = context.CivilizacionDios.Include(x => x.dio).OrderBy(x => x.Id)
        //    //    .Select(x => new CivilizacionDiosModel { 
        //    //        Id = x.Id,
        //    //        IdCivilizacion = x.IdCivilizacion,
        //    //        IdDios = x.IdDios,
        //    //        NombreLocal = x.NombreLocal,
        //    //        Dioses = x.NombreLocal.GroupBy(y => y.)
        //    //    });

        //    //vm.Dioses = context.Dioses.OrderBy(x => x.Id)
        //    //    .Select( x => new DiosesModel 
        //    //    {
        //    //        Id = x.Id,
        //    //        NombreGeneral = x.NombreGeneral,
        //    //        Genero = x.Genero,
        //    //        Dominio = x.Dominio,
        //    //        Descripcion = x.Descripcion,

        //    //    });

        //    return View(vm);
        //}



        //public IActionResult Dioses(string Id)
        //{
        //    MitologiaMexicanaContext context = new();
        //var datos = context.CivilizacionDios
        //    .Where(cd => civilizacionNombre == null || cd.IdCivilizacionNavigation.Nombre == civilizacionNombre)
        //    .Select(cd => new DiosesViewModel.DiosesModel
        //    {
        //        Id = cd.IdDios,
        //        NombreGeneral = cd.IdDiosNavigation.NombreGeneral,
        //        Genero = cd.IdDiosNavigation.Genero,
        //        Dominio = cd.IdDiosNavigation.Dominio,
        //        Descripcion = cd.IdDiosNavigation.Descripcion,
        //        //NombreLocal = cd.NombreLocal,
        //        //CivilizacionNombre = cd.IdCivilizacionNavigation.Nombre
        //    }).ToList();

        //var vm = new DiosesViewModel
        //{
        //    Civilizaciones = context.Civilizaciones
        //        .Select(c => new DiosesViewModel.CivilizacionModel
        //        {
        //            IdCivilizacion = c.IdCivilizacion,
        //            Nombre = c.Nombre
        //        }).ToList(),

        //    Dioses = datos,
        //    Seleccionado = diosId.HasValue ? datos.FirstOrDefault(d => d.Id == diosId.Value) : null
        //};

        //return View(vm);

        //    var vm = new DiosesViewModel();
        //    var civilizacionesConDioses = context.Civilizaciones
        //    .OrderBy(c => c.Nombre)
        //    .Select(c => new CivilizacionModel
        //    {
        //        IdCivilizacion = c.IdCivilizacion,
        //        Nombre = c.Nombre,
        //        // proyectamos la colección de dioses relacionados (subconsulta correlacionada)
        //        Dioses = c.CivilizacionDios 
        //                 .OrderBy(cd => cd.IdDiosNavigation.NombreGeneral)
        //                 .Select(cd => new DiosesModel
        //                 {
        //                     Id = cd.IdDiosNavigation.Id,
        //                     NombreGeneral = cd.IdDiosNavigation.NombreGeneral,
        //                     Genero = cd.IdDiosNavigation.Genero,
        //                     Dominio = cd.IdDiosNavigation.Dominio,
        //                     Descripcion = cd.IdDiosNavigation.Descripcion,

        //                 }).ToList()
        //    }).ToList();

        //    vm.Civilizaciones = civilizacionesConDioses;

        //    vm.Dioses = civilizacionesConDioses
        //    .Where(c => c.Nombre == Id)
        //    .SelectMany(c => c.Dioses ?? Enumerable.Empty<DiosesModel>())
        //    .GroupBy(d => d.Id)
        //    .Select(g => g.First())
        //    .OrderBy(d => d.NombreGeneral)
        //    .ToList();

        //    vm.CivilizacionDioses = civilizacionesConDioses
        //    .SelectMany(c => (c.Dioses ?? Enumerable.Empty<DiosesModel>())
        //    .Select(d => new CivilizacionDiosModel
        //    {
        //        Id = 0, // si necesitas el Id original de la intermedia, proyectalo en la consulta anterior
        //        IdCivilizacion = c.IdCivilizacion,
        //        IdDios = d.Id,
        //        NombreLocal = d.NombreGeneral ?? string.Empty
        //    }))
        //.ToList();

        //    return View(vm);






        // Por algun motivo no obtiene los datos correctamente, pero no me permite mostrarlos
        //DiosesViewModel vm = new();


        //var vm = context.Civilizaciones.Select(x => new Models.ViewModels.CivilizacionModel
        //{
        //   IdCivilizacion = x.IdCivilizacion,
        //   Nombre = x.Nombre,
        //   CivilizacionDioses = x.CivilizacionDios.Select(d => new CivilizacionDiosModel
        //   {
        //       Id = d.Id,
        //       IdCivilizacion = d.IdCivilizacion,
        //       IdDios = d.IdDios,
        //       NombreLocal = d.NombreLocal,
        //       //Dioses = context.Dioses.Select(z => new DiosesModel
        //       //{
        //       //    Id = z.Id,
        //       //    NombreGeneral = z.NombreGeneral,
        //       //    Genero = z.Genero,
        //       //    Dominio = z.Dominio,
        //       //    Descripcion = z.Descripcion,
        //       //})
        //   }),
        //    Dioses = x.CivilizacionDios.Select(d => new DiosesModel
        //    {
        //        Id = d.IdDiosNavigation.Id,
        //        NombreGeneral = d.IdDiosNavigation.NombreGeneral,
        //        Genero = d.IdDiosNavigation.Genero,
        //        Dominio = d.IdDiosNavigation.Dominio,
        //        Descripcion = d.IdDiosNavigation.Descripcion,
        //    })
        //});





        //return View(vm);
        //}

        public IActionResult Dioses(string? Id)
        {
            MitologiaMexicanaContext context = new();

            // Una sola consulta que trae todo lo necesario
            var data = context.Civilizaciones
                .Select(c => new
                {
                    c.IdCivilizacion,
                    c.Nombre,
                    Dioses = c.CivilizacionDios.Select(cd => new
                    {
                        cd.Id,
                        cd.NombreLocal,
                        //cd.IdDiosNavigation.Id,
                        cd.IdDiosNavigation.NombreGeneral,
                        cd.IdDiosNavigation.Genero,
                        cd.IdDiosNavigation.Dominio,
                        cd.IdDiosNavigation.Descripcion
                    })
                })
                .ToList();

            var vm = new DiosesViewModel
            {
                Civilizaciones = data.Select(c => new Models.ViewModels.CivilizacionModel
                {
                    IdCivilizacion = c.IdCivilizacion,
                    Nombre = c.Nombre
                })
            };

            //if (!string.IsNullOrEmpty(Id))
            {
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
            }

            return View(vm);
        }



    }
}
