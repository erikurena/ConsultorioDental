using ClinicaOdontologica.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClinicaOdontologica.Controllers
{
    public class EstadoCivilController : Controller
    {

        public ActionResult EstadoCivil()
        {
            var ECivil = new EstadoCivilModel().MostrarEstadoCivil();
            ViewBag.EstadoCivil = new SelectList(ECivil, "IdEstadoCivil", "DescripcionEstCivil");
            return View();
        }

    }
}
