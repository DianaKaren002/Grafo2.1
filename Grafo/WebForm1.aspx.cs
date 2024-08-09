using ClassGrafoEntidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Grafo
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private static GrafoLibros grafo1 = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                grafo1 = new GrafoLibros();
                Session["grafo1"] = grafo1;
            }
            else
            {
                grafo1 = (GrafoLibros)Session["grafo1"];
            }
            
        }

        

        [WebMethod]
        public static void InsertarNodo(string Titulo, string Autor, int Id)
        {
            Libro nuevo = new Libro
            {
                Titulo = Titulo,
                Autor = Autor,
                Id = Id
            };
            grafo1.InsertarNodo(nuevo);
        }

        [WebMethod]
        public static string ObtenerGrafo()
        {
            var nodos = grafo1.MuestraNodos();

            var grafoLibros = new
            {
                nodos = nodos
            };

            return new JavaScriptSerializer().Serialize(grafoLibros);
        }

        protected void btnInsertarNodo_Click(object sender, EventArgs e)
        {
            Libro nuevo = new Libro
            {
                Titulo = txtTitulo.Text,
                Autor = txtAutor.Text,
                Id = Convert.ToInt16(txtId.Text)
            };
            grafo1.InsertarNodo(nuevo);
            string[] vertex = grafo1.MuestraNodos();

            if (vertex != null && vertex.Length > 0)//PARA VER SI TIENE DATOS EL ARRAY
            {
                DropDijkstraInicio.Items.Clear();
                DropDijkstraFin.Items.Clear();

                foreach (string s in vertex)
                {
                    DropDijkstraInicio.Items.Add(s);//LOS INSERTA 
                    DropDijkstraFin.Items.Add(s);
                }
            }
            else
            {
                Label1.Text = "No hay datos";
            }
            Session["grafo1"] = grafo1;
            lblResultado.Text = $"Nodo '{nuevo.Titulo}' insertado correctamente.";

            var verticesJson = JsonConvert.SerializeObject(grafo1.ListaAbyacente.Select(nodo => new {
                nodo.Informacion.Id,
                nodo.Informacion.Titulo,
                aristas = nodo.enlaces.mostrarDatosColeccion().Select(a => new {
                    IdLibro = grafo1.ListaAbyacente[a.NumVertice].Informacion.Id,
                    TituloLibro = grafo1.ListaAbyacente[a.NumVertice].Informacion.Titulo,
                    costo = a.Costo
                })
            }));

            string script = $"console.log({verticesJson}); mostrarGrafo({verticesJson});";
            ClientScript.RegisterStartupScript(this.GetType(), "MostrarGrafo", script, true);
        }


        protected void btnInsertarArco_Click(object sender, EventArgs e)
        {
            int origen = Convert.ToInt16(txtOrigen.Text) - 1;
            int destino = Convert.ToInt16(txtDestino.Text) - 1;
            int costo = Convert.ToInt16(txtCosto.Text);

            if (txtOrigen.Text == null && txtDestino.Text == null && txtCosto.Text == null)
            {
                lblResultado.Text = "LLena todos los datos";
            }
            else
            {
                grafo1.InsertarArco(origen, destino, costo);
                lblResultado.Text = "Arco insertado";
                var verticesJson = JsonConvert.SerializeObject(grafo1.ListaAbyacente.Select(nodo => new {
                    nodo.Informacion.Id,
                    nodo.Informacion.Titulo,
                    aristas = nodo.enlaces.mostrarDatosColeccion().Select(a => new {
                        IdLibro = grafo1.ListaAbyacente[a.NumVertice].Informacion.Id,
                        TituloLibro = grafo1.ListaAbyacente[a.NumVertice].Informacion.Titulo,
                        costo = a.Costo
                    })
                }));

                string script = $"console.log({verticesJson}); mostrarGrafo({verticesJson});";
                ClientScript.RegisterStartupScript(this.GetType(), "MostrarGrafo", script, true);
            }
        }
        protected void btnDFS_Click(object sender, EventArgs e)
        {
            int inicio = Convert.ToInt16(txtDFSInicio.Text);
            if (inicio != 0)
            {
                List<int> resultado = grafo1.DFS(inicio);

                lblResultado.Text = "Recorrido en Profundidad: " + string.Join(" -> ", resultado);
                ListDFS.Items.Clear();
                foreach (int v in resultado)
                {
                    ListDFS.Items.Add(v.ToString());
                }
            }
            else
            {
                lblResultado.Text = "Error: Nodo inicial no encontrado.";
            }
        }

        protected void btnBFS_Click(object sender, EventArgs e)
        {
            int inicio = Convert.ToInt16(txtBFSInicio.Text);
            if (inicio !=0)
            {
                List<int> resultado = grafo1.BFS(inicio);

                lblResultado.Text = "Recorrido en Amplitud: " + string.Join(" -> ", resultado);
                ListBFS.Items.Clear();
                foreach (int v in resultado)
                {
                    ListDFS.Items.Add(v.ToString());
                }
            }
            else
            {
                lblResultado.Text = "Error: Nodo inicial no encontrado.";
            }
        }

        protected void btnDijkstra_Click(object sender, EventArgs e)
        {
            // Convertir los valores seleccionados en los DropDownLists a IDs de nodo
            int inicio = Convert.ToInt32(DropDijkstraInicio.SelectedIndex.ToString());
            int fin = Convert.ToInt32(DropDijkstraFin.SelectedIndex.ToString());

            // Ajustar los índices
            int inicio2 = inicio ;
            int fin2 = fin ;

            Label1.Text = $"{inicio2}  {fin2}";

            // Verificar si los IDs existen en la lista de adyacencia del grafo
            if (inicio2 < 0 || inicio2 >= grafo1.ListaAbyacente.Count)
            {
                lblResultado.Text = $"Error: El nodo de inicio con ID '{inicio2}' no existe en el grafo.";
                return;
            }

            if (fin2 < 0 || fin2 >= grafo1.ListaAbyacente.Count)
            {
                lblResultado.Text = $"Error: El nodo de destino con ID '{fin2}' no existe en el grafo.";
                return;
            }

            // Llamar al método Dijkstra y obtener el camino más corto en títulos
            List<string> titulosCamino = grafo1.Dijkstra(inicio, fin);

            if (titulosCamino.Count < 0)
            {
                lblResultado.Text = "No se encontró un camino entre los nodos especificados.";
            }
            else
            {
                // Mostrar la ruta más corta en el label
                lblResultado.Text = "Camino Más Corto: " + string.Join(" -> ", titulosCamino);

                // Mostrar el grafo en el cliente
                var verticesJson = JsonConvert.SerializeObject(grafo1.ListaAbyacente.Select(nodo => new {
                    nodo.Informacion.Id,
                    nodo.Informacion.Titulo,
                    aristas = nodo.enlaces.mostrarDatosColeccion().Select(a => new {
                        IdLibro = grafo1.ListaAbyacente[a.NumVertice].Informacion.Id,
                        TituloLibro = grafo1.ListaAbyacente[a.NumVertice].Informacion.Titulo,
                        costo = a.Costo
                    })
                }));

                string script = $"console.log({verticesJson}); mostrarGrafo({verticesJson});";
                ClientScript.RegisterStartupScript(this.GetType(), "MostrarGrafo", script, true);
                Session["grafo1"] = grafo1;
            }
        }


        protected void btnMostrarGrafo_Click(object sender, EventArgs e)
        {
            var verticesJson = JsonConvert.SerializeObject(grafo1.ListaAbyacente.Select(nodo => new {
                nodo.Informacion.Id,
                nodo.Informacion.Titulo,
                aristas = nodo.enlaces.mostrarDatosColeccion().Select(a => new {
                    IdLibro = grafo1.ListaAbyacente[a.NumVertice].Informacion.Id,
                    TituloLibro = grafo1.ListaAbyacente[a.NumVertice].Informacion.Titulo,
                    costo = a.Costo
                })
            }));

            string script = $"console.log({verticesJson}); mostrarGrafo({verticesJson});";
            ClientScript.RegisterStartupScript(this.GetType(), "MostrarGrafo", script, true);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string[] vertex = grafo1.MuestraNodos();

            if (vertex != null && vertex.Length > 0)
            {
                DropDijkstraInicio.Items.Clear();
                DropDijkstraFin.Items.Clear();

                foreach (string s in vertex)
                {
                    DropDijkstraInicio.Items.Add(s);
                    DropDijkstraFin.Items.Add(s);
                }
            }
            else
            {
                Label1.Text = "No hay datos";
            }
        }
    }
}
