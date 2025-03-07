using Microsoft.AspNetCore.Mvc;
using MvcNetCorePracticaZapatillas.Models;
using MvcNetCorePracticaZapatillas.Repository;

namespace MvcNetCorePracticaZapatillas.Controllers
{
    public class ZapatillasController : Controller
    {
        private RepositoryZapatillas repo;

        public ZapatillasController(RepositoryZapatillas repo)
        {
            this.repo = repo;
        }

        public async Task<IActionResult> Zapatillas()
        {
            List<Zapatilla> zapatillas = await this.repo.GetZapatillasAsync();
            return View(zapatillas);
        }

        public async Task<IActionResult> Details(int? posicion, int idproducto)
        {
            if(posicion == null)
            {
                posicion = 1;
            }

            ModelZapatillasImagenes model = await this.repo.GetZapatillasImagenesOutAsync(posicion.Value, idproducto);    
            Zapatilla zapatilla = await this.repo.FindZapatillasAsync(idproducto);
           
            ViewData["ZAPATILLASELECT"] = zapatilla;
            ViewData["REGISTROS"] = model.NumeroRegistro;
            ViewData["ZAPATILLAS"] = idproducto;

            int siguiente = posicion.Value + 1;
            if(siguiente > model.NumeroRegistro)
            {
                siguiente = model.NumeroRegistro;
            } 
            int anterior = posicion.Value - 1;
            if(anterior < 1)
            {
                anterior = 1;
            }

            ViewData["ULTIMO"] = model.NumeroRegistro;
            ViewData["SIGUIENTE"] = siguiente;
            ViewData["ANTERIOR"] = anterior;
            ViewData["POSICION"] = posicion;

            return View(model.ImagenesZapatillas);
        }

        public async Task<IActionResult> _ZapatillaPartial(int? posicion, int idproducto)
        {
            if(posicion == null)
            {
                posicion = 1;
                return View();
            }
            else
            {
                ModelZapatillasImagenes model = await this.repo.GetZapatillasImagenesOutAsync(posicion.Value, idproducto);
                Zapatilla zapatilla = await this.repo.FindZapatillasAsync(idproducto);

                ViewData["ZAPATILLASELECCIONADA"] = zapatilla;
                ViewData["REGISTROS"] = model.NumeroRegistro;
                ViewData["IDPRODUCTO"] = idproducto;

                int siguiente = posicion.Value + 1;
                if (siguiente > model.NumeroRegistro)
                {
                    siguiente = model.NumeroRegistro;
                }
                int anterior = posicion.Value - 1;
                if (anterior < 1)
                {
                    anterior = 1;
                }

                ViewData["ULTIMO"] = model.NumeroRegistro;
                ViewData["SIGUIENTE"] = siguiente;
                ViewData["ANTERIOR"] = anterior;
                ViewData["POSICION"] = posicion;

                return PartialView("_ZapatillaPartial", model.ImagenesZapatillas);
            }
        }
    }
}
