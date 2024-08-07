using ClassGrafoEntidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.Services;

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
            Session["grafo1"] = grafo1;
            lblResultado.Text = $"Nodo '{nuevo.Titulo}' insertado correctamente.";

            
        }
    

        protected void btnInsertarArco_Click(object sender, EventArgs e)
        {
            int origen = Convert.ToInt16(txtOrigen.Text);
            int destino = Convert.ToInt16(txtDestino.Text);
            int costo = Convert.ToInt16(txtCosto.Text);

            if(txtOrigen.Text == null && txtDestino.Text == null && txtCosto.Text == null)
            {
                lblResultado.Text = "LLena todos los datos";
            }
            else
            {
                grafo1.InsertarArco(origen, destino, costo);
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
            int inicio = Convert.ToInt16(txtDijkstraInicio.Text);
            int fin = Convert.ToInt16(txtDijkstraFin.Text);

            if (inicio != -1 && fin != -1)
            {
                List<int> resultadoIndices = grafo1.Dijkstra(inicio, fin);
                List<string> resultadoTitulos = resultadoIndices.Select(indice => grafo1.ListaAbyacente[indice].Informacion.Titulo).ToList();

                lblResultado.Text = "Camino Más Corto: " + string.Join(" -> ", resultadoTitulos);
            }
            else
            {
                lblResultado.Text = "Error: Verifique que ambos nodos existen en el grafo.";
            }
        }

        protected void btnMostrarGrafo_Click(object sender, EventArgs e)
        {
            var verticesJson = JsonConvert.SerializeObject(grafo1.ListaAbyacente.Select(nodo => new {
                numeroDato = nodo.Informacion.Id,
                nombreDato = nodo.Informacion.Titulo,
                aristas = nodo.enlaces.mostrarDatosColeccion().Select(a => new {
                    numeroDato = grafo1.ListaAbyacente[a.NumVertice].Informacion.Id,
                    nombreDato = grafo1.ListaAbyacente[a.NumVertice].Informacion.Titulo,
                    costo = a.Costo
                })
            }));

            string script = $"console.log({verticesJson}); mostrarGrafo({verticesJson});";
            ClientScript.RegisterStartupScript(this.GetType(), "MostrarGrafo", script, true);
        }
    }
}
