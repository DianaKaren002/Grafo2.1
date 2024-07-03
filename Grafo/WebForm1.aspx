﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Grafo.WebForm1" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/vis/4.21.0/vis.min.css" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/vis/4.21.0/vis.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Grafo Libros</h1>
        </div>
        <div>
            <h2>Insertar Nodo</h2>
            <asp:TextBox ID="txtId" runat="server" Placeholder="Id del Libro" TextMode="Number"></asp:TextBox><br />
            <asp:TextBox ID="txtTitulo" runat="server" Placeholder="Título del Libro"></asp:TextBox><br />
            <asp:TextBox ID="txtAutor" runat="server" Placeholder="Autor del Libro"></asp:TextBox><br />
            
            <asp:Button ID="btnInsertarNodo" runat="server" Text="Insertar Nodo" OnClick="btnInsertarNodo_Click" /><br /><br />
            <asp:Label ID="lblResultado" runat="server" Text=""></asp:Label>

            <h2>Insertar Arco</h2>
            <asp:TextBox ID="txtOrigen" runat="server" Placeholder="Título del Nodo Origen"></asp:TextBox><br />
            <asp:TextBox ID="txtDestino" runat="server" Placeholder="Título del Nodo Destino"></asp:TextBox><br />
            <asp:Button ID="btnInsertarArco" runat="server" Text="Insertar Arco" OnClick="btnInsertarArco_Click" /><br /><br />

            <h2>Recorrido en Profundidad (DFS)</h2>
            <asp:TextBox ID="txtDFSInicio" runat="server" Placeholder="Título del Nodo Inicial DFS"></asp:TextBox><br />
            <asp:Button ID="btnDFS" runat="server" Text="Recorrido en Profundidad"  OnClick="btnDFS_Click"/><br /><br />

            <h2>Recorrido en Amplitud (BFS)</h2>
            <asp:TextBox ID="txtBFSInicio" runat="server" Placeholder="Título del Nodo Inicial BFS"></asp:TextBox><br />
            <asp:Button ID="btnBFS" runat="server" Text="Recorrido en Amplitud" OnClick="btnBFS_Click" /><br /><br />

            <h2>Camino Más Corto (Dijkstra)</h2>
            <asp:TextBox ID="txtDijkstraInicio" runat="server" Placeholder="Título del Nodo Inicial Dijkstra"></asp:TextBox><br />
            <asp:TextBox ID="txtDijkstraFin" runat="server" Placeholder="Título del Nodo Final Dijkstra"></asp:TextBox><br />
            <asp:Button ID="btnDijkstra" runat="server" Text="Camino Más Corto" OnClick="btnDijkstra_Click" /><br /><br />

            <h2>Visualización del Grafo</h2>
            <div id="mynetwork" style="width: 800px; height: 600px; border: 1px solid lightgray;"></div>
        </div>
    </form>
    
    <script>
        function insertarNodo() {
            const titulo = $('#<%= txtTitulo.ClientID %>').val();
            const autor = $('#<%= txtAutor.ClientID %>').val();
            const id = $('#<%= txtId.ClientID %>').val();

            $.ajax({
                type: "POST",
                url: "WebForm1.aspx/InsertarNodo",
                data: JSON.stringify({ Titulo: titulo, Autor: autor, Id: id }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    obtenerGrafo();
                },
                error: function (error) {
                    console.error("Error en la inserción del nodo:", error);
                }
            });
        }

        function obtenerGrafo() {
            $.ajax({
                type: "POST",
                url: "WebForm1.aspx/ObtenerGrafo",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    const grafoLibros = JSON.parse(response.d);
                    console.log("Grafo obtenido:", grafoLibros);
                    graficarGrafo(grafoLibros);
                },
                error: function (error) {
                    console.error("Error al obtener el grafo:", error);
                }
            });
        }

        let network;
let nodes = new vis.DataSet();
let edges = new vis.DataSet();

function graficarGrafo(grafoLibros) {
    const radius = 200;
    const nodesWithPosition = calculateCircularPositions(grafoLibros.nodos, radius);

    nodes.clear(); //limpia los datos y los vuelve a agregar
    nodesWithPosition.forEach(libro => {
        nodes.add({
            id: libro.Id,
            label: libro.Titulo,
            x: libro.x,
            y: libro.y,
            fixed: true
        });
    });

    // se crean aristas consecutivas cuando se va agregando los nodos
    const consecutiveEdges = [];
    for (let i = 0; i < nodesWithPosition.length - 1; i++) {
        consecutiveEdges.push({ from: nodesWithPosition[i].Id, to: nodesWithPosition[i + 1].Id });
    }

    // aqui se hacen las aristas que se crean desde el formulario  
    const formEdges = grafoLibros.aristas.map(arista => {
        const fromNode = nodes.get({
            filter: node => node.label === arista.Desde
        })[0];
        const toNode = nodes.get({
            filter: node => node.label === arista.Hasta
        })[0];
        if (fromNode && toNode) { 
            return { from: fromNode.id, to: toNode.id };
        } else {
            console.error(`Nodo no encontrado para la arista: Desde ${arista.Desde}, Hasta ${arista.Hasta}`);
            return null;
        }
    }).filter(edge => edge !== null); // filtro pra aristas invalidas

    // limpua y agrega aristas nuevamente
    edges.clear();
    edges.add([...consecutiveEdges, ...formEdges]);

    const container = document.getElementById('mynetwork');
    const data = {
        nodes: nodes,
        edges: edges
    };

    const options = {
        physics: false,
        edges: {
            color: 'red' 
    };

    if (!network) {
        network = new vis.Network(container, data, options);
    } else {
        network.setData(data);
    }
}

function calculateCircularPositions(nodes, radius) {
    const angleStep = (2 * Math.PI) / nodes.length;
    return nodes.map((node, index) => {
        const angle = index * angleStep;
        return {
            ...node,
            x: radius * Math.cos(angle),
            y: radius * Math.sin(angle)
        };
    });
}

$(document).ready(function () {
    obtenerGrafo();
});

    </script>
</body>
</html>
