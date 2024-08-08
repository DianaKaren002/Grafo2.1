using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassGrafoEntidades
{
    public class GrafoLibros
    {
        public Dictionary<int, Nodo> nodos; 
        public List<Nodo> ListaAbyacente = new List<Nodo>();
        public List<Arista> Adyacentes { get; set; }//no se ocupa
        public GrafoLibros()
        {
            nodos = new Dictionary<int, Nodo>(); // Inicialización de nodos
        }

        public void InsertarNodo(Libro informacion)
        {
            ListaAbyacente.Add(new Nodo(informacion));
        }

        public string InsertarArco(int origen, int destino, float costo)
        {
            string msj = "";
            if (origen >= 0 && origen <= (ListaAbyacente.Count - 1))
            {
                if (destino >= 0 && destino <= (ListaAbyacente.Count - 1))
                {
                    ListaAbyacente[origen].InsertaArista(destino, costo);
                    msj = "Arista agregada";
                }
                else
                    msj = "La posición del Nodo Destino no existe en la Lista de Adyacencia";
            }
            else
            {
                msj = "La posición del Nodo Origen no existe en la Lista de Adyacencia";
            }
            return msj;
        }

        public List<string> MuestraAristasVertice(int posiVert, ref string msg)
        {

            ListaNodos temp = null;


            List<string> salida = new List<string>();
            if (posiVert >= 0 && posiVert <= (ListaAbyacente.Count - 1))
            {
                temp = ListaAbyacente[posiVert].enlaces.inicio;

                while (temp != null)
                {
                    salida.Add(
                        $"Vertive destino: {ListaAbyacente[temp.NumVertice].Informacion.Titulo} " +
                        $"Posicion enlace a:[{temp.NumVertice}] " +
                        $"Costo: {temp.Costo}");
                    temp = temp.siguiente;
                }
                msg = "Los datos se muestran";
            }
            else
            {
                msg = "La posición del nodo no existe en la Lista de Adyacencia";
            }
            return salida;
        }

        public string[] MuestraNodos()
        {
            string[] cads = new string[ListaAbyacente.Count];
            int h = 0;
            for (h = 0; h <= ListaAbyacente.Count - 1; h++)
            {
                cads[h] = ListaAbyacente[h].ToString();
            }
            return cads;
        }

        public List<int> DFS(int inicio) //busqueda a profundidad 
        {
            List<int> resultado = new List<int>();
            bool[] visitado = new bool[ListaAbyacente.Count];
            Stack<int> stack = new Stack<int>();
            stack.Push(inicio);

            while (stack.Count > 0)
            {
                int v = stack.Pop();

                if (!visitado[v])
                {
                    visitado[v] = true;
                    resultado.Add(v);

                    foreach (var arista in ListaAbyacente[v].enlaces.Reverse())
                    {
                        if (!visitado[arista.NumVertice])
                        {
                            stack.Push(arista.NumVertice);
                        }
                    }
                }
            }

            return resultado;
        }

        public List<int> BFS(int inicio)//busqueda en amplitud 
        {
            List<int> resultado = new List<int>();
            bool[] visitado = new bool[ListaAbyacente.Count];
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(inicio);
            visitado[inicio] = true;

            while (queue.Count > 0)
            {
                int v = queue.Dequeue();
                resultado.Add(v);

                foreach (var arista in ListaAbyacente[v].enlaces)
                {
                    if (!visitado[arista.NumVertice])
                    {
                        queue.Enqueue(arista.NumVertice);
                        visitado[arista.NumVertice] = true;
                    }
                }
            }

            return resultado;
        }

        public List<int> Dijkstra(int inicioId, int finId)  //hacer recorridos cortos entre nodos 
        {
            var distancias = new Dictionary<int, int>();  //almacena la distancia mas corta para poderla mostar
            var predecesores = new Dictionary<int, int>();
            var visitados = new HashSet<int>();
            var colaPrioridad = new SortedDictionary<int, int>(); //almacena las distancias 

            // Inicializa las distancias para todos los nodos
            foreach (var nodo in nodos.Values)
            {
                distancias[nodo.Informacion.Id] = int.MaxValue;
            }
            distancias[inicioId] = 0;
            colaPrioridad.Add(0, inicioId);

            while (colaPrioridad.Count > 0)
            {
                var actualId = colaPrioridad.First().Value;
                colaPrioridad.Remove(colaPrioridad.First().Key);

                if (visitados.Contains(actualId)) continue;

                visitados.Add(actualId);

                var nodoActual = ListaAbyacente[actualId];
                foreach (var enlace in nodoActual.enlaces)
                {
                    // Asegúrate de que la clave exista en el diccionario antes de acceder
                    if (!distancias.ContainsKey(enlace.NumVertice))
                    {
                        distancias[enlace.NumVertice] = int.MaxValue;
                    }

                    var nuevaDistancia = distancias[actualId] + 1;
                    if (nuevaDistancia < distancias[enlace.NumVertice])
                    {
                        distancias[enlace.NumVertice] = nuevaDistancia;
                        predecesores[enlace.NumVertice] = actualId;

                        if (!colaPrioridad.ContainsKey(nuevaDistancia))
                        {
                            colaPrioridad.Add(nuevaDistancia, enlace.NumVertice);
                        }
                    }
                }
            }

            var camino = new List<int>();
            var paso = finId;

            while (paso != inicioId)
            {
                camino.Add(paso);
                if (!predecesores.ContainsKey(paso))
                {
                    // No se encontró un camino
                    return new List<int>();
                }
                paso = predecesores[paso];
            }

            camino.Add(inicioId);
            camino.Reverse();
            return camino;
        }

    }
}
