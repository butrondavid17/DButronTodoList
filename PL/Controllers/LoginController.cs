using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string Email, string Password)
        {
            Dictionary<string, object> objeto = BL.Usuario.GetByEmail(Email);
            bool resulltado = (bool)objeto["Resultado"];
            if (resulltado)
            {
                ML.Usuario usuario = new ML.Usuario();
                usuario = (ML.Usuario)objeto["Usuario"];
                if (usuario.Email == Email && usuario.Password == Password)
                {
                    ViewBag.Success = true;
                    HttpContext.Session.SetString("Usuario", Newtonsoft.Json.JsonConvert.SerializeObject(usuario));
                    return RedirectToAction("GetAll", "Tarea");
                }
                else
                {
                    ViewBag.Success = false;
                    ViewBag.Mensaje = "Correo o contraseña incorrectas";
                    return View();
                }
            }
            else
            {
                ViewBag.Success = false;
                ViewBag.Mensaje = "Correo o contraseña incorrectas";
                return View();
            }
        }
    }
}
