using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassGrafoEntidades
{
    public class ListaAristas : IEnumerable<ListaNodos>
    {
        public ListaNodos inicio = null;
        private int contElementos = 0;

        public IEnumerator<ListaNodos> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public string InsertaObjeto(int numVertices, float costo)
        {
            string msj = " ";
            ListaNodos nuevoNodo = new ListaNodos();
            nuevoNodo.NumVertice = numVertices;
            nuevoNodo.Costo = costo;
            if(inicio == null)
            {
                inicio = nuevoNodo;
                contElementos++;
                msj = "Objeto insertado";
            }
            else
            {
                ListaNodos t = inicio;
                while (t.siguiente != null)
                {
                    t = t.siguiente;
                }
                t.siguiente = nuevoNodo;
                contElementos++;
                msj = "Ya no es el primer Elemento";
            }
            return msj;
        }

       
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public string[] MostrarDatosColeccion()
        {
            string[] cads = new string[contElementos];
            ListaNodos z = inicio;
            int w = 0;
            while (z != null)
            {
                cads[w] = $"Posición enlace a:[{z.NumVertice}] Costo: {z.Costo}";
                z = z.siguiente;
                w++;
            }
            return cads;
        }
    }
}
