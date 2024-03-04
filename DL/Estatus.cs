using System;
using System.Collections.Generic;

namespace DL;

public partial class Estatus
{
    public int IdEstatus { get; set; }

    public string Tipo { get; set; } = null!;

    public virtual ICollection<Tarea> Tareas { get; set; } = new List<Tarea>();
}
