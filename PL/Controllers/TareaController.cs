using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class TareaController : Controller
    {
        [HttpGet]
        public IActionResult GetAll(int? IdTarea)
        {
            ML.Tarea tarea = new ML.Tarea();
            var usuarioSession = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(HttpContext.Session.GetString("Usuario"));
            if (IdTarea != null)
            {
                Dictionary<string, object> objeto = BL.Tarea.GetById(IdTarea.Value);
                bool resultado = (bool)objeto["Resultado"];
                if (resultado)
                {
                    tarea = (ML.Tarea)objeto["Tarea"];
                    Dictionary<string, object> objetoEstatus = BL.Estatus.GetAll();
                    ML.Estatus estatus = (ML.Estatus)objetoEstatus["Estatus"];
                    tarea.Estatus.Estatuses = estatus.Estatuses;
                    return View(tarea);
                }
                else
                {
                    string excepcion = (string)objeto["Excepcion"];
                    ViewBag.Mensaje = "Ocurrio un error al consultar la informacion " + excepcion;
                    return PartialView("Modal");
                }
            }
            else
            {
                Dictionary<string, object> objeto = BL.Tarea.GetAll(usuarioSession.IdUsuario);
                bool resultado = (bool)objeto["Resultado"];
                if (resultado)
                {
                    tarea = (ML.Tarea)objeto["Tarea"];
                    tarea.Estatus = new ML.Estatus();
                    Dictionary<string, object> resultadoEstatus = BL.Estatus.GetAll();
                    ML.Estatus estatus = (ML.Estatus)resultadoEstatus["Estatus"];
                    tarea.Estatus.Estatuses = estatus.Estatuses;
                    return View(tarea);
                }
                else
                {
                    string excepcion = (string)objeto["Excepcion"];
                    ViewBag.Mensaje = "Ocurrio un error " + excepcion;
                    return PartialView("Modal");
                }
               
            }
            
        }
        [HttpPost]
        public IActionResult Form(ML.Tarea tarea)
        {
            var usuarioSession = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(HttpContext.Session.GetString("Usuario"));
            if (tarea.IdTarea > 0)
            {
                tarea.Usuario.IdUsuario = usuarioSession.IdUsuario;
                Dictionary<string, object> objeto = BL.Tarea.Update(tarea);
                bool resultado = (bool)objeto["Resultado"];
                if (resultado)
                {
                    ViewBag.Mensaje = "La tarea ha sido actualizada";
                    return PartialView("Modal");
                }
                else
                {
                    string excepcion = (string)objeto["Excepcion"];
                    ViewBag.Mensaje = "Ocurrio un error al intentar actualizar la tarea " + excepcion;
                    return PartialView("Modal");
                }
            }
            else
            {
                tarea.Usuario.IdUsuario = usuarioSession.IdUsuario;
                Dictionary<string, object> objeto = BL.Tarea.Add(tarea);
                bool resultado = (bool)objeto["Resultado"];
                if (resultado)
                {
                    ViewBag.Mensaje = "La tarea ha sido agregada con exito";
                    return PartialView("Modal");
                }
                else
                {
                    string excepcion = (string)objeto["Excepcion"];
                    ViewBag.Mensaje = "Ocurrio un error al agregar la tarea " + excepcion;
                    return PartialView("Modal");
                }
            }
        }
        [HttpGet]
        public IActionResult Delete(int IdTarea)
        {
            Dictionary<string, object> objeto = BL.Tarea.Delete(IdTarea);
            bool resultado = (bool)objeto["Resultado"];
            if (resultado)
            {
                ViewBag.Mensaje = "La tarea se eliminó exitosamente";
                return PartialView("Modal");
            }
            else
            {
                string excepcion = (string)objeto["Excepcion"];
                ViewBag.Mensaje = "Ocurrio un error al eliminar la tarea " + excepcion;
                return PartialView("Modal");
            }
        }
    }
}
