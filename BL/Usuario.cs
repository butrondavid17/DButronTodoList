using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario
    {
        public static Dictionary<string, object> GetById(int IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            string excepcion = "";
            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Usuario", usuario}, {"Excepcion", excepcion}, {"Resultado", false} };
            try
            {
                using (DL.DbutronTodoListContext context = new DL.DbutronTodoListContext())
                {
                    var objetoUsuario = (from tablaUsuario in context.Usuarios
                                         select new {
                                            IdUsuario = tablaUsuario.IdUsuario,
                                            Username = tablaUsuario.Username,
                                            Email = tablaUsuario.Email,
                                            Password = tablaUsuario.Password
                                         }).FirstOrDefault();
                    if (objetoUsuario != null)
                    {
                        usuario.IdUsuario = objetoUsuario.IdUsuario;
                        usuario.Username = objetoUsuario.Username;
                        usuario.Email = objetoUsuario.Email;
                        usuario.Password = objetoUsuario.Password;
                        diccionario["Resultado"] = true;
                        diccionario["Usuario"] = usuario; 
                    }
                    else
                    {
                        diccionario["Resultado"] = false;
                    }
                }
            }
            catch (Exception ex)
            {
                diccionario["Resultado"] = false;
                diccionario["Excepcion"] = ex.Message;
            }
            return diccionario;
        }
        public static Dictionary<string, object> GetByEmail(string Email)
        {
            ML.Usuario usuario = new ML.Usuario();
            string excepcion = "";
            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Usuario", usuario }, { "Excepcion", excepcion }, { "Resultado", false } };
            try
            {
                using (DL.DbutronTodoListContext context = new DL.DbutronTodoListContext())
                {
                    var objetoUsuario = (from tablaUsuario in context.Usuarios
                                         where tablaUsuario.Email == Email
                                         select new
                                         {
                                             IdUsuario = tablaUsuario.IdUsuario,
                                             Username = tablaUsuario.Username,
                                             Email = tablaUsuario.Email,
                                             Password = tablaUsuario.Password
                                         }).FirstOrDefault();
                    if (objetoUsuario != null)
                    {
                        usuario.IdUsuario = objetoUsuario.IdUsuario;
                        usuario.Username = objetoUsuario.Username;
                        usuario.Email = objetoUsuario.Email;
                        usuario.Password = objetoUsuario.Password;
                        diccionario["Resultado"] = true;
                        diccionario["Usuario"] = usuario;
                    }
                    else
                    {
                        diccionario["Resultado"] = false;
                    }
                }
            }
            catch (Exception ex)
            {
                diccionario["Resultado"] = false;
                diccionario["Excepcion"] = ex.Message;
            }
            return diccionario;
        }
    }
}
