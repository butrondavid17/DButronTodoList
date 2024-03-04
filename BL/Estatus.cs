using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Estatus
    {
        public static Dictionary<string, object> GetAll()
        {
            ML.Estatus estatus = new ML.Estatus();
            string excepcion = "";
            Dictionary<string, object> diccionario = new Dictionary<string, object> { {"Estatus", estatus}, { "Excepcion", excepcion}, { "Resultado", false} };
            try
            {
                using (DL.DbutronTodoListContext context = new DL.DbutronTodoListContext())
                {
                    var listaEstatus = (from tablaEstatus in context.Estatuses
                                        select new { 
                                            IdEstatus = tablaEstatus.IdEstatus,
                                            Tipo = tablaEstatus.Tipo
                                        }).ToList();
                    if (listaEstatus != null)
                    {
                        estatus.Estatuses = new List<object>();
                        foreach (var registro in listaEstatus)
                        {
                            ML.Estatus status = new ML.Estatus();
                            status.IdEstatus = registro.IdEstatus;
                            status.Tipo = registro.Tipo;
                            estatus.Estatuses.Add(status);
                        }
                        diccionario["Resultado"] = true;
                        diccionario["Estatus"] = estatus;
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
