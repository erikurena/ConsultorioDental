using Microsoft.AspNetCore.Mvc;
using ClinicaOdontologica.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClinicaOdontologica.Controllers
{
    public class InstruccionController : Controller
    {
        GradoInstruccionModel ObjGradoInstruccion = new GradoInstruccionModel();
        
        public ActionResult GradoInstruccion()
        {
            var GInstruccion = ObjGradoInstruccion.MostrarInstruccion();
            ViewBag.Instruccion = new SelectList(GInstruccion, "IdGradoInstruccion", "GradoInstruccion");
            return View();
        }
    }
}
