using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Reflection.Metadata;

namespace BL
{
    public class Tarea
    {
        public static Dictionary<string, object> Add(ML.Tarea tarea)
        {
            string excepcion = "";
            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Excepcion", excepcion }, { "Resultado", false } };
            try
            {
                using (DL.DbutronTodoListContext context = new DL.DbutronTodoListContext())
                {
                    int filasAfectadas = context.Database.ExecuteSqlRaw($"AddTarea '{tarea.Titulo}', '{tarea.Descripcion}', '{tarea.FechaVencimiento}', {tarea.Estatus.IdEstatus}, {tarea.Usuario.IdUsuario}");
                    if (filasAfectadas > 0)
                    {
                        diccionario["Resultado"] = true;
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
        public static Dictionary<string, object> Update(ML.Tarea tarea)
        {
            string excepcion = "";
            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Excepcion", excepcion }, { "Resultado", false } };
            try
            {
                using (DL.DbutronTodoListContext context = new DL.DbutronTodoListContext())
                {
                    int filasAfectadas = context.Database.ExecuteSqlRaw($"UpdateTarea {tarea.IdTarea}, '{tarea.Titulo}', '{tarea.Descripcion}', {tarea.Estatus.IdEstatus}");
                    if (filasAfectadas > 0)
                    {
                        diccionario["Resultado"] = true;
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
        public static Dictionary<string, object> Delete(int IdTarea)
        {
            string excepcion = "";
            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Excepcion", excepcion }, { "Resultado", false } };
            try
            {
                using (DL.DbutronTodoListContext context = new DL.DbutronTodoListContext())
                {
                    int filasAfectadas = context.Database.ExecuteSqlRaw($"DeleteTarea {IdTarea}");
                    if (filasAfectadas > 0)
                    {
                        diccionario["Resultado"] = true;
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
        public static Dictionary<string, object> GetAll(int IdUsuario)
        {
            ML.Tarea tarea = new ML.Tarea();
            string excepcion = "";
            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Tarea", tarea }, { "Excepcion", excepcion }, { "Resultado", false } };
            try
            {
                using (DL.DbutronTodoListContext context = new DL.DbutronTodoListContext())
                {
                    var listaTareas = (from tablaTarea in context.Tareas
                                       join tablaEstatus in context.Estatuses on tablaTarea.IdEstatus equals tablaEstatus.IdEstatus
                                       join tablaUsuario in context.Usuarios on tablaTarea.IdUsuario equals tablaUsuario.IdUsuario
                                       where tablaTarea.IdUsuario == IdUsuario
                                       select new
                                       {
                                           IdTarea = tablaTarea.IdTarea,
                                           Titulo = tablaTarea.Titulo,
                                           Descripcion = tablaTarea.Descripcion,
                                           FechaVencimiento = tablaTarea.FechaVencimiento,
                                           IdEstatus = tablaTarea.IdEstatus,
                                           Tipo = tablaEstatus.Tipo,
                                           IdUsuario = tablaUsuario.IdUsuario,
                                           Username = tablaUsuario.Username
                                       }).ToList();
                    if (listaTareas != null)
                    {
                        tarea.Tareas = new List<object>();
                        foreach (var registro in listaTareas)
                        {
                            ML.Tarea task = new ML.Tarea();
                            task.IdTarea = registro.IdTarea;
                            task.Titulo = registro.Titulo;
                            task.Descripcion = registro.Descripcion;
                            task.FechaVencimiento = registro.FechaVencimiento;
                            task.Estatus = new ML.Estatus();
                            task.Estatus.IdEstatus = registro.IdEstatus;
                            task.Estatus.Tipo = registro.Tipo;
                            task.Usuario = new ML.Usuario();
                            task.Usuario.IdUsuario = registro.IdUsuario;
                            task.Usuario.Username = registro.Username;
                            tarea.Tareas.Add(task);
                        }
                        diccionario["Resultado"] = true;
                        diccionario["Tarea"] = tarea;
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
        public static Dictionary<string, object> GetById(int IdTarea)
        {
            ML.Tarea tarea = new ML.Tarea();
            string excepcion = "";
            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Tarea", tarea }, { "Excepcion", excepcion }, { "Resultado", false } };
            try
            {
                using (DL.DbutronTodoListContext context = new DL.DbutronTodoListContext())
                {
                    var objetoTarea = (from tablaTarea in context.Tareas
                                       join tablaEstatus in context.Estatuses on tablaTarea.IdEstatus equals tablaEstatus.IdEstatus
                                       join tablaUsuario in context.Usuarios on tablaTarea.IdUsuario equals tablaUsuario.IdUsuario
                                       where tablaTarea.IdTarea == IdTarea
                                       select new
                                       {
                                           IdTarea = tablaTarea.IdTarea,
                                           Titulo = tablaTarea.Titulo,
                                           Descripcion = tablaTarea.Descripcion,
                                           FechaVencimiento = tablaTarea.FechaVencimiento,
                                           IdEstatus = tablaTarea.IdEstatus,
                                           Tipo = tablaEstatus.Tipo,
                                           IdUsuario = tablaUsuario.IdUsuario,
                                           Username = tablaUsuario.Username
                                       }).FirstOrDefault();
                    if (objetoTarea != null)
                    {
                        tarea.IdTarea = objetoTarea.IdTarea;
                        tarea.Titulo = objetoTarea.Titulo;
                        tarea.Descripcion = objetoTarea.Descripcion;
                        tarea.FechaVencimiento = objetoTarea.FechaVencimiento;
                        tarea.Estatus = new ML.Estatus();
                        tarea.Estatus.IdEstatus = objetoTarea.IdEstatus;
                        tarea.Usuario = new ML.Usuario();
                        tarea.Usuario.IdUsuario = objetoTarea.IdUsuario;
                        tarea.Usuario.Username = objetoTarea.Username;
                        diccionario["Resultado"] = true;
                        diccionario["Tarea"] = tarea;
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