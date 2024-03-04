using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Estatus
    {
        public int IdEstatus { get; set; }
        public string Tipo { get; set; }
        public List<object> Estatuses { get; set; }
    }
}
