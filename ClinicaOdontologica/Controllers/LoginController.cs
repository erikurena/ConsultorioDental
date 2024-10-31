using ClinicaOdontologica.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;


namespace ClinicaOdontologica.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(UsuarioModel objUsuario)
        {
            UsuarioModel modeloUsuario = new UsuarioModel();
           
            var usuario = modeloUsuario.ValidarUsuario(objUsuario.ApellidoPaterno, objUsuario.Password);
            if (usuario != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
                    new Claim(ClaimTypes.Name, usuario.ApellidoPaterno),
                    new Claim(ClaimTypes.Role, usuario.IdRol.ToString()),
                    new Claim(ClaimTypes.GivenName, usuario.ImgUsuario)
                }; 
              var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
              await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Home");
            }
            else
            {              
                ModelState.AddModelError(string.Empty, "Cuenta o contraseña incorrecta.");
                return View();
            }
        }
        public IActionResult MostrarImagen()
        {
            var imgUsuario = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value;

            if (string.IsNullOrEmpty(imgUsuario))
            {
                return NotFound("La imagen no existe.");
            }

            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imgUsuario);

            if (!System.IO.File.Exists(imagePath))
            {
                return NotFound("La imagen no existe.");
            }

            ViewBag.ImgUsuario = imgUsuario;
            return View();
        }

        public async Task<ActionResult> Salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }
    }
}
