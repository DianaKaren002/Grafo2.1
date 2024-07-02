<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Grafo.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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

            
        </div>
    </form>
</body>
</html>
