using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassGrafoEntidades
{
    public class GrafoLibros
    {
        public Dictionary<string, Nodo> nodos; //dictionary ayuda a organizar y manejar los nodos

        public GrafoLibros()
        {
            nodos = new Dictionary<string, Nodo>();
        }

        public void InsertarNodo(Libro informacion)
        {
            if (!nodos.ContainsKey(informacion.Titulo))
            {
                nodos[informacion.Titulo] = new Nodo(informacion);
            }
        }

        public void InsertarArco(string origen, string destino)
        {
            if (nodos.ContainsKey(origen) && nodos.ContainsKey(destino))
            {
                Nodo nodoOrigen = nodos[origen];
                Nodo nodoDestino = nodos[destino];
                nodoOrigen.Adyacentes.Add(nodoDestino);
            }
        }

        public List<string> DFS(string inicio)
        {
            List<string> resultado = new List<string>();
            HashSet<string> visitados = new HashSet<string>();
            Stack<Nodo> pila = new Stack<Nodo>();

            if (nodos.ContainsKey(inicio))
            {
                pila.Push(nodos[inicio]);

                while (pila.Count > 0)
                {
                    Nodo actual = pila.Pop();

                    if (!visitados.Contains(actual.Informacion.Titulo))
                    {
                        visitados.Add(actual.Informacion.Titulo);
                        resultado.Add(actual.Informacion.ToString());

                        foreach (var adyacente in actual.Adyacentes)
                        {
                            if (!visitados.Contains(adyacente.Informacion.Titulo))
                            {
                                pila.Push(adyacente);
                            }
                        }
                    }
                }
            }

            return resultado;
        }

        public List<string> BFS(string inicio)
        {
            List<string> resultado = new List<string>();
            HashSet<string> visitados = new HashSet<string>();
            Queue<Nodo> cola = new Queue<Nodo>();

            if (nodos.ContainsKey(inicio))
            {
                cola.Enqueue(nodos[inicio]);

                while (cola.Count > 0)
                {
                    Nodo actual = cola.Dequeue();

                    if (!visitados.Contains(actual.Informacion.Titulo))
                    {
                        visitados.Add(actual.Informacion.Titulo);
                        resultado.Add(actual.Informacion.ToString());

                        foreach (var adyacente in actual.Adyacentes)
                        {
                            if (!visitados.Contains(adyacente.Informacion.Titulo))
                            {
                                cola.Enqueue(adyacente);
                            }
                        }
                    }
                }
            }

            return resultado;
        }

        public List<string> Dijkstra(string inicio, string fin)
        {
            var distancias = new Dictionary<string, int>();
            var predecesores = new Dictionary<string, string>();
            var visitados = new HashSet<string>();
            var colaPrioridad = new SortedDictionary<int, string>();

            foreach (var nodo in nodos.Keys)
            {
                distancias[nodo] = int.MaxValue;
            }
            distancias[inicio] = 0;
            colaPrioridad.Add(0, inicio);

            while (colaPrioridad.Count > 0)
            {
                var actual = colaPrioridad.First().Value;
                colaPrioridad.Remove(colaPrioridad.First().Key);

                if (visitados.Contains(actual)) continue;

                visitados.Add(actual);

                foreach (var adyacente in nodos[actual].Adyacentes)
                {
                    var nuevaDistancia = distancias[actual] + 1; // Suponiendo que todos los arcos tienen peso 1

                    if (nuevaDistancia < distancias[adyacente.Informacion.Titulo])
                    {
                        distancias[adyacente.Informacion.Titulo] = nuevaDistancia;
                        predecesores[adyacente.Informacion.Titulo] = actual;

                        if (!colaPrioridad.ContainsKey(nuevaDistancia))
                        {
                            colaPrioridad.Add(nuevaDistancia, adyacente.Informacion.Titulo);
                        }
                    }
                }
            }

            var camino = new List<string>();
            var paso = fin;

            while (!paso.Equals(inicio))
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
