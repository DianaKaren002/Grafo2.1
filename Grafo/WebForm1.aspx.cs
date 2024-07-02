using ClassGrafoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Grafo
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        GrafoLibros grafo1 = new GrafoLibros();
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

        protected void btnInsertarNodo_Click(object sender, EventArgs e)
        {
            Libro nuevo = new Libro
            {
                Titulo=txtTitulo.Text,
                Autor=txtAutor.Text,
                Id = Convert.ToInt16(txtId.Text)
            };
            grafo1.InsertarNodo(nuevo);
            Session["grafo1"] = grafo1;
            lblResultado.Text = $"Nodo '{nuevo.Titulo}' insertado correctamente.";
        }

        protected void btnInsertarArco_Click(object sender, EventArgs e)
        {
            string origen = txtOrigen.Text;
            string destino = txtDestino.Text;

            if (grafo1.nodos.ContainsKey(origen) && grafo1.nodos.ContainsKey(destino))
            {
                grafo1.InsertarArco(origen, destino);

                // Actualizar la variable de sesión
                Session["grafo1"] = grafo1;

                txtOrigen.Text = string.Empty;
                txtDestino.Text = string.Empty;

                lblResultado.Text = $"Arco de '{origen}' a '{destino}' insertado correctamente.";
            }
            else
            {
                lblResultado.Text = "Error: Verifique que ambos nodos existen en el grafo.";
            }
        }

        protected void btnDFS_Click(object sender, EventArgs e)
        {
            string inicio = txtDFSInicio.Text;
            if (grafo1.nodos.ContainsKey(inicio))
            {
                List<string> resultado = grafo1.DFS(inicio);

                lblResultado.Text = "Recorrido en Profundidad: " + string.Join(" -> ", resultado);
            }
            else
            {
                lblResultado.Text = "Error: Nodo inicial no encontrado.";
            }
        }

        protected void btnBFS_Click(object sender, EventArgs e)
        {
            string inicio = txtBFSInicio.Text;
            if (grafo1.nodos.ContainsKey(inicio))
            {
                List<string> resultado = grafo1.BFS(inicio);

                lblResultado.Text = "Recorrido en Amplitud: " + string.Join(" -> ", resultado);
            }
            else
            {
                lblResultado.Text = "Error: Nodo inicial no encontrado.";
            }
        }

        protected void btnDijkstra_Click(object sender, EventArgs e)
        {
            string inicio = txtDijkstraInicio.Text;
            string fin = txtDijkstraFin.Text;

            if (grafo1.nodos.ContainsKey(inicio) && grafo1.nodos.ContainsKey(fin))
            {
                List<string> resultado = grafo1.Dijkstra(inicio, fin);

                lblResultado.Text = "Camino Más Corto: " + string.Join(" -> ", resultado);
            }
            else
            {
                lblResultado.Text = "Error: Verifique que ambos nodos existen en el grafo.";
            }
        }
    }
}