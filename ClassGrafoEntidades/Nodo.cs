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
        public ListaAristas enlaces { get; set; }
        

        public Nodo(Libro informacion)
        {
            Informacion = informacion;
            enlaces = new ListaAristas();
        }

        public string InfoLibro()
        {
            return $"I{Informacion.Id}, Titulo: {Informacion.Titulo}, Autor: {Informacion.Autor}";
        }

        public string InsertaArista(int numNodo, float costo1)
        {
            return enlaces.InsertaObjeto(numNodo, costo1);
        }
        
        public string[] MuestraAristas()
        {
            return enlaces.MostrarDatosColeccion();
        }
    }
}
