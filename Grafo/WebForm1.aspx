<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Grafo.WebForm1" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GRAFO</title>
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous" />

    <link href="https://cdnjs.cloudflare.com/ajax/libs/vis/4.21.0/vis.min.css" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/vis/4.21.0/vis.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <link rel="stylesheet" href="../CSS/Estilos.css" />
</head>
<body>

    <form id="form1" runat="server">
        <section class="fondo-grafo">
            <div>
                <h1 class="title-user">GRAFOS<span class="landing-user">LIBROS</span> </h1>
            </div>
            <div class="container">
                <div class="row">
                    <div class="col-md-6">
                        <div class="container">
                            <div class="list">
                                <h2>Insertar Nodo</h2>
                                <asp:TextBox ID="txtId" runat="server" CssClass="form-control" Placeholder="Id del Libro" TextMode="Number"></asp:TextBox><br />

                                <asp:TextBox ID="txtTitulo" runat="server" CssClass="form-control" Placeholder="Título del Libro"></asp:TextBox><br />

                                <asp:TextBox ID="txtAutor" runat="server" CssClass="form-control" Placeholder="Autor del Libro"></asp:TextBox><br />

                                <asp:Button class="btn btn-outline-danger" ID="btnInsertarNodo" runat="server" Text="Insertar Nodo" OnClick="btnInsertarNodo_Click" /><br />
                                <br />
                                <asp:Label ID="lblResultado" runat="server" Text=""></asp:Label>

                                <h2>Insertar Arco</h2>
                                <asp:TextBox ID="txtOrigen" runat="server" CssClass="form-control" Placeholder="Título del Nodo Origen"></asp:TextBox><br />
                                <asp:TextBox ID="txtDestino" runat="server" CssClass="form-control" Placeholder="Título del Nodo Destino"></asp:TextBox><br />
                                <asp:Button class="btn btn-outline-danger" ID="btnInsertarArco" runat="server" Text="Insertar Arco" OnClick="btnInsertarArco_Click" /><br />
                                <br />

                                <h2>Recorrido en Profundidad (DFS)</h2>
                                <asp:TextBox ID="txtDFSInicio" runat="server" CssClass="form-control" Placeholder="Título del Nodo Inicial DFS"></asp:TextBox><br />
                                <asp:Button class="btn btn-outline-danger" ID="btnDFS" runat="server" Text="Recorrido en Profundidad" OnClick="btnDFS_Click" /><br />
                                <br />

                                <h2>Recorrido en Amplitud (BFS)</h2>
                                <asp:TextBox ID="txtBFSInicio" runat="server" CssClass="form-control" Placeholder="Título del Nodo Inicial BFS"></asp:TextBox><br />
                                <asp:Button class="btn btn-outline-danger" ID="btnBFS" runat="server" Text="Recorrido en Amplitud" OnClick="btnBFS_Click" /><br />
                                <br />

                                <h2>Camino Más Corto (Dijkstra)</h2>
                                <asp:TextBox ID="txtDijkstraInicio" runat="server" CssClass="form-control" Placeholder="Título del Nodo Inicial Dijkstra"></asp:TextBox><br />
                                <asp:TextBox ID="txtDijkstraFin" runat="server" CssClass="form-control" Placeholder="Título del Nodo Final Dijkstra"></asp:TextBox><br />
                                <asp:Button class="btn btn-outline-danger" ID="btnDijkstra" runat="server" Text="Camino Más Corto" OnClick="btnDijkstra_Click" /><br />
                                <br />
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <h1 class="title-user">visualizacion <span class="landing-user">del Grafo</span> </h1>
                        <div class="d-flex justify-content-center contenerdor-img-user">
                            <div id="mynetwork" style="width: 800px; height: 600px; border: 1px solid lightgray;"></div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
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

            nodes.clear(); // Limpia los datos y los vuelve a agregar
            nodesWithPosition.forEach(libro => {
                nodes.add({
                    id: libro.Id,
                    label: libro.Titulo,
                    x: libro.x,
                    y: libro.y,
                    fixed: true
                });
            });

            // Se crean aristas consecutivas cuando se van agregando los nodos
            const consecutiveEdges = [];
            for (let i = 0; i < nodesWithPosition.length - 1; i++) {
                consecutiveEdges.push({ from: nodesWithPosition[i].Id, to: nodesWithPosition[i + 1].Id });
            }

            // Aquí se hacen las aristas que se crean desde el formulario  
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
            }).filter(edge => edge !== null); // Filtro para aristas inválidas

            // Limpia y agrega aristas nuevamente
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
                }
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
