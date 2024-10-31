using ClinicaOdontologica.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace ClinicaOdontologica.Controllers
{
    [Authorize]
    public class AgendarCitaController : Controller
    {

        // GET: AgendarCitaController/Create
        public ActionResult GuardarCita()
        {            
            MostrarDoctor();
            ConsultorioMostrar();
            ConsultorioGuardar();
            AgendaPorConsultorio();
            if (TempData["Events"] != null)
            {
                ViewBag.Events = TempData["Events"];
            }           
            return View();
        }

        // POST: AgendarCitaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GuardarCita(AgendarCitaModel objAgendarCita)
        {
            new AgendarCitaModel().GuardarCita(objAgendarCita);
            MostrarDoctor();
            ConsultorioMostrar();
            ConsultorioGuardar();
            AgendaPorConsultorio();
            if (TempData["Events"] != null)
            {
                ViewBag.Events = TempData["Events"];
            }            
            return View ();           
        }

        // GET: AgendarCitaController/Delete/5
        public ActionResult Delete()
        {
            return View();
        }

        // POST: AgendarCitaController/Delete/5
        [HttpPost]       
        public ActionResult Delete(int id)
        {
             new AgendarCitaModel().EliminarCita(id);
            return RedirectToAction("GuardarCita");
        }
        public ActionResult MostrarDoctor()
        {
            var objDoctor = new AgendarCitaModel().ListaDoctor();
            ViewBag.MostrarDoctor = new SelectList(objDoctor, "IdUsuario", "NombreUsurio");
            return View();
        }
        public ActionResult ConsultorioMostrar()
        {
            var objConsultorio = new AgendarCitaModel().ListaConsultorio();
            ViewBag.ConsultorioMostrar = new SelectList(objConsultorio, "IdConsultorioMostrar", "NombreConsultorio");
            return View();
        }
        public ActionResult ConsultorioGuardar()
        {
            var objConsultorio = new AgendarCitaModel().ListaConsultorio();
            ViewBag.ConsultorioGuardar = new SelectList(objConsultorio, "IdConsultorioMostrar", "NombreConsultorio");
            return View();
        }
        public ActionResult MostrarAgenda()
        {
            return View();
        }
        [HttpPost]
        public ActionResult MostrarAgenda(AgendarCitaModel model)
        {

            var objAgenda = model.ListaConsulta(model);
            var events = objAgenda.Select(a => new
            {
                id = a.IdAgendarCita,
                title = a.TipoConsulta,
                start = a.Fecha.ToString("yyyy-MM-dd") + "T" + a.Horario.ToString("HH:mm:ss"),
                end = a.Fecha.ToString("yyyy-MM-dd") + "T" + a.Horario.AddHours(1).ToString("HH:mm:ss"), 
                nombrePaciente = a.NombrePaciente,
                tipoConsulta = a.TipoConsulta,
                horario = a.Horario.ToString("HH:mm"),
                fecha = a.Fecha.ToString("yyyy-MM-dd")              
            }).ToList();
            ViewBag.Events = JsonConvert.SerializeObject(events);
            
            TempData["Events"] = JsonConvert.SerializeObject(events);
            return View();
        }

        public ActionResult AgendaPorConsultorio()
        {
            return View(); 
        }
        [HttpPost]
        public JsonResult AgendaPorConsultorio(AgendarCitaModel model)
        {
            var objAgenda = model.ListaConsulta(model);
            var EventoCalendario = objAgenda.Select(AgendaModel => new
            {
                id = AgendaModel.IdAgendarCita,
                title = AgendaModel.TipoConsulta,
                start = AgendaModel.Fecha.ToString("yyyy-MM-dd") + "T" + AgendaModel.Horario.ToString("HH:mm:ss"),
                end = AgendaModel.Fecha.ToString("yyyy-MM-dd") + "T" + AgendaModel.Horario.AddHours(1).ToString("HH:mm:ss"),
                nombrePaciente = AgendaModel.NombrePaciente,
                tipoConsulta = AgendaModel.TipoConsulta,
                horario = AgendaModel.Horario.ToString("HH:mm"),
                fecha = AgendaModel.Fecha.ToString("yyyy-MM-dd")
            }).ToList();
            return Json(EventoCalendario);
        }
        
    }
}
