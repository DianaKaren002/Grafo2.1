using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassGrafoEntidades
{
    public class Nodo
    {
        public Libro Informacion { get; set; }
        public List<Nodo> Adyacentes { get; set; }

        public Nodo(Libro informacion)
        {
            Informacion = informacion;
            Adyacentes = new List<Nodo>();
        }

        public override string ToString()
        {
            return Informacion.ToString();
        }
    }
}
