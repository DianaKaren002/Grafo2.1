using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassGrafoEntidades
{
    public class ListaNodos
    {
        public int NumVertice = 1;
        public float Costo { get; set; }
        public ListaNodos siguiente = null;
    }
}
