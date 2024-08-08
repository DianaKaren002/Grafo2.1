using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassGrafoEntidades
{
    public class GrafoLibros
    {
        public List<Nodo> ListaAbyacente = new List<Nodo>();

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

        public List<int> DFS(int inicio)
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

        public List<int> BFS(int inicio)
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

        public List<int> Dijkstra(int inicio, int fin)
        {
            var distancias = new Dictionary<int, float>();
            var predecesores = new Dictionary<int, int>();
            var visitados = new HashSet<int>();
            var colaPrioridad = new SortedDictionary<float, int>();

            for (int i = 0; i < ListaAbyacente.Count; i++)
            {
                distancias[i] = float.MaxValue;
            }
            distancias[inicio] = 0;
            colaPrioridad.Add(0, inicio);

            while (colaPrioridad.Count > 0)
            {
                var actual = colaPrioridad.First().Value;
                colaPrioridad.Remove(colaPrioridad.First().Key);

                if (visitados.Contains(actual)) continue;

                visitados.Add(actual);

                foreach (var arista in ListaAbyacente[actual].enlaces)
                {
                    var nuevaDistancia = distancias[actual] + arista.Costo;

                    if (nuevaDistancia < distancias[arista.NumVertice])
                    {
                        distancias[arista.NumVertice] = nuevaDistancia;
                        predecesores[arista.NumVertice] = actual;

                        if (!colaPrioridad.ContainsKey(nuevaDistancia))
                        {
                            colaPrioridad.Add(nuevaDistancia, arista.NumVertice);
                        }
                    }
                }
            }

            var camino = new List<int>();
            var paso = fin;

            while (paso != inicio)
            {
                camino.Add(paso);
                paso = predecesores[paso];
            }

            camino.Add(inicio);
            camino.Reverse();
            return camino;
        }
    }
}
