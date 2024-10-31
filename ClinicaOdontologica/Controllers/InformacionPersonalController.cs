using Microsoft.AspNetCore.Mvc;
using ClinicaOdontologica.Models;
using Microsoft.AspNetCore.Authorization;

namespace ClinicaOdontologica.Controllers
{
    [Authorize]
    public class InformacionPersonalController : Controller
    {
        InfoPersonalPacienteModel objPaciente = new InfoPersonalPacienteModel();
        public IActionResult ListaInformacion()
        {
            //muestra la lista de informacion personal del paciente
            var objLista = objPaciente.ListaInformacion();
            return View(objLista);
        }
        public IActionResult Guardar()
        {
            //devuelve la lista 
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Guardar(InfoPersonalPacienteModel objInformacionPersonal)
        {
            //recibe un objeto y lo guarda en la bd
            var respuesta = await objPaciente.GuardarInfoPersonal(objInformacionPersonal);
            if (respuesta)
                return RedirectToAction("ListaInformacion");
            else
                return View();
        }
        public IActionResult Editar(int IdInfoPaciente)
        {
            //devuelve la lista 
            var objInfoPaciente = objPaciente.InfoPersonal(IdInfoPaciente);
            return View(objInfoPaciente);
        }
        [HttpPost]
        public IActionResult Editar(InfoPersonalPacienteModel objInformacionPersonal)
        {
                return View();
        }
        public IActionResult Eliminar(int IdInfoPaciente)
        {
            //devuelve la lista 
            var objInfoPaciente = objPaciente.InfoPersonal(IdInfoPaciente);
            return View(objInfoPaciente);
        }
        [HttpPost]
        public IActionResult Eliminar(InfoPersonalPacienteModel objInformacionPersonal)
        {
            var respuesta = objPaciente.EliminarInfoPersonal(objInformacionPersonal.IdInformacionPersonal);
            if (respuesta)
                return RedirectToAction("ListaInformacion");
            else
                return View();
        }

    }
}
