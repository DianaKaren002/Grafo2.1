
function mostrarGrafo(vertices) {
    var canvas = document.getElementById("miCanvas");
    var context = canvas.getContext('2d');
    context.clearRect(0, 0, canvas.width, canvas.height); // Limpiar el canvas

    // obtener las dimensiones completas del área de dibujo
    let tx = canvas.width;
    let ty = canvas.height;
    let xc = tx / 2;
    let yc = ty / 2;


    // vamos a generar todos los puntos a lo largo de una circunferencia
    let grados = 0;
    let inc_grados = 360 / vertices.length; // Ajuste automático del incremento de grados
    let hipote = 280;
    let radiocir = 50;
    let trigo2 = new Trigonometria(grados, hipote);
    let uncirculo = new Circulo();
    let radianes = 0;

    // variables para obtener los valores de x y y con respecto a la forma circular 
    let xxc = 0;
    let yyc = 0;
    let circulos = [];

    // Dibujar los vértices y sus líneas al centro
    for (let i = 0; i < vertices.length; i++) {
        grados = i * inc_grados;
        // convertir a radianes
        radianes = (2 * Math.PI * grados) / 360;
        trigo2.angulo = radianes;
        // calcular los catetos
        xxc = xc + trigo2.obtenerAdyacente();
        yyc = yc - trigo2.obtenerOpuesto();
        uncirculo.posiX = xxc;
        uncirculo.posiY = yyc;
        uncirculo.radio = radiocir;
        uncirculo.color = '#2E69D2';
        uncirculo.draw(context);

        circulos.push({ x: xxc, y: yyc, radio: radiocir, Titulo: vertices[i].Titulo, aristas: vertices[i].aristas });

        // Cambios
        context.textAlign = "center";
        context.textBaseline = "middle";
        context.font = "18px Candara";
        context.fillText(vertices[i].Titulo, xxc, yyc);
    }

    for (let i = 0; i < circulos.length; i++) {
        let circActual = circulos[i];
        for (let j = 0; j < circActual.aristas.length; j++) {
            let arista = circActual.aristas[j];
            let circDestino = circulos.find(c => c.Titulo == arista.TituloLibro);
            if (circDestino) {
                drawArrow(context, circActual.x, circActual.y, circDestino.x, circDestino.y, circActual.radio, circDestino.radio);
            }
        }
    }
    // Dibujar las líneas entre los vértices según las aristas


}

function drawArrow(context, fromX, fromY, toX, toY, fromRadius, toRadius) {
    let headLength = 10; // longitud de la cabeza de la flecha
    let angle = Math.atan2(toY - fromY, toX - fromX);

    // Calcular los puntos finales de la línea ajustando por los radios
    let startX = fromX + Math.cos(angle) * fromRadius;
    let startY = fromY + Math.sin(angle) * fromRadius;
    let endX = toX - Math.cos(angle) * toRadius;
    let endY = toY - Math.sin(angle) * toRadius;

    // Dibujar la línea
    context.strokeStyle = '#1c646d';
    context.beginPath();
    context.moveTo(startX, startY);
    context.lineTo(endX, endY);
    context.stroke();

    // Dibujar la cabeza de la flecha
    context.beginPath();
    context.moveTo(endX, endY);
    context.lineTo(endX - headLength * Math.cos(angle - Math.PI / 6), endY - headLength * Math.sin(angle - Math.PI / 6));
    context.moveTo(endX, endY);
    context.lineTo(endX - headLength * Math.cos(angle + Math.PI / 6), endY - headLength * Math.sin(angle + Math.PI / 6));
    context.stroke();
}

