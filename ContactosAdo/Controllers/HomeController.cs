using CrudAdoNet.Models;
using ContactoAdo.datos;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ContactosAdo.Controllers
{
    public class MantenedorController : Controller
    {
        ContactoDatos _contactoDatos = new ContactoDatos();
        public IActionResult Listar()
        {
            var lista = _contactoDatos=new ContactoDatos();
            //mostrar una lista de contactos
            return View(lista);
        }
        [HttpGet]

        public IActionResult Guardar()
        {
            //para mostar el formulario
            return View();
        }
        [HttpPost]
        public IActionResult Guardar(ContactoModel model)
        {
            if (ModelState.IsValid)
            {
                return View();
            }
            //para obtener los datos del formulario y mandarlo a la base de datos
            bool respuesta = _contactoDatos.GuardarContacto(model);
            if (respuesta)
            {
                return RedirectToAction("Listar");

            }
            else
            {
                return View();
            }
        }
    }
}