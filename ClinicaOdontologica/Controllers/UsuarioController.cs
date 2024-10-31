using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ClinicaOdontologica.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using System.IO;


namespace ClinicaOdontologica.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        public readonly string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img");
        UsuarioModel objUsuarioModel = new UsuarioModel();
        // GET: UsuarioController
        public ActionResult Index()
        {
            var objLista = objUsuarioModel.ListaInformacionUsuario();
            return View(objLista);
        }

        // GET: UsuarioController/Create
        public ActionResult GuardarUsuario()
        {
            MostrarRol();
            return View();
        }
        
        // POST: UsuarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> GuardarUsuario(UsuarioModel objUsuario, IFormFile ImgUsuario)
        {
            if (ImgUsuario != null && ImgUsuario.Length > 0)
            {
                var fileName = Path.GetFileName(ImgUsuario.FileName);

                var filePath = Path.Combine(imagePath, fileName);

                if (!Directory.Exists(imagePath))
                {
                    Directory.CreateDirectory(imagePath);
                }

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImgUsuario.CopyToAsync(stream);
                }

                objUsuario.ImgUsuario   = $"/img/{fileName}";
            }
            try
            {
                objUsuarioModel.GuardarUsuarioModel(objUsuario);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(objUsuario);
            }
        }               
        public ActionResult ModificarUsuario(int idUsuario)
        {
            MostrarRol();
            var objUsuario = objUsuarioModel.InfoUsuario(idUsuario);
            return View(objUsuario);
        }       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ModificarUsuario(UsuarioModel objUsuario, IFormFile ImgUsuario)
        {
            if (ImgUsuario != null && ImgUsuario.Length > 0)
            {
                var fileName = Path.GetFileName(ImgUsuario.FileName);
                var filePath = Path.Combine(imagePath, fileName);

                if (!Directory.Exists(imagePath))
                {
                    Directory.CreateDirectory(imagePath);
                }

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImgUsuario.CopyToAsync(stream);
                }

                objUsuario.ImgUsuario = $"/img/{fileName}";
            }
            try
            {
                objUsuarioModel.ModificarUsuario(objUsuario);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        

        // POST: UsuarioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EliminarUsuario(UsuarioModel objUsuario)
        {
            try
            {
                objUsuarioModel.EliminarUsuario(objUsuario);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                 return RedirectToAction(nameof(Index));
            }
        }
        public ActionResult MostrarRol()
        {
            var objRol = new RolModel().ListaRol();
            ViewBag.MostrarRol = new SelectList(objRol, "IdRol", "TipoRol");
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        
    }
}
