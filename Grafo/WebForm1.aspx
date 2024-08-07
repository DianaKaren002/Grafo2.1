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
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>
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
                                

                                <h2>Insertar Arco</h2>
                                <asp:TextBox ID="txtOrigen" runat="server" CssClass="form-control" Placeholder="Id del Nodo Origen"></asp:TextBox><br />
                                <asp:TextBox ID="txtDestino" runat="server" CssClass="form-control" Placeholder="Id del Nodo Destino"></asp:TextBox><br />
                                <asp:TextBox ID="txtCosto" runat="server" CssClass="form-control" Placeholder="Costo"></asp:TextBox><br />
                                <asp:Button class="btn btn-outline-danger" ID="btnInsertarArco" runat="server" Text="Insertar Arco" OnClick="btnInsertarArco_Click" /><br />
                                <br />

                                <h2>Recorrido en Profundidad (DFS)</h2>
                                <asp:TextBox ID="txtDFSInicio" runat="server" CssClass="form-control" Placeholder="Título del Nodo Inicial DFS"></asp:TextBox><br />
                                <asp:Button class="btn btn-outline-danger" ID="btnDFS" runat="server" Text="Recorrido en Profundidad" OnClick="btnDFS_Click" /><br />
                                <asp:ListBox ID="ListDFS" runat="server"></asp:ListBox>
                                <br />

                                <h2>Recorrido en Amplitud (BFS)</h2>
                                <asp:TextBox ID="txtBFSInicio" runat="server" CssClass="form-control" Placeholder="Título del Nodo Inicial BFS"></asp:TextBox><br />
                                <asp:Button class="btn btn-outline-danger" ID="btnBFS" runat="server" Text="Recorrido en Amplitud" OnClick="btnBFS_Click" /><br />
                                <asp:ListBox ID="ListBFS" runat="server"></asp:ListBox>
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
                            <canvas id="MiCanvas" style="width: 800px; height: 600px; border: 1px solid lightgray;"></canvas>
                        </div>
                         <asp:Label ID="lblResultado" class="message" runat="server" Text=""></asp:Label>
                        <asp:Button ID="btnMostrarGrafo" class="btn btn-outline-danger" runat="server" Text="Grafica Grafo" OnClick="btnMostrarGrafo_Click" />
                    </div>
                </div>
            </div>
            <script type="text/javascript" src="JavaScript/circulo.js"></script>
            <script type="text/javascript" src="JavaScript/Radar.js"></script>
            <script type="text/javascript" src="JavaScript/Trigonometria.js"></script>
        </section>

    </form>

   
</body>
</html>
