function mostrarGrafo(verticesJson) {
    var canvas = document.getElementById("MiCanvas");
    var context = canvas.getContext('2d');
    context.clearRect(0, 0, canvas.width, canvas.height);

    // obtener las dimensiones completas del área de dibujo
    let tx = canvas.width;
    let ty = canvas.height;
    let xc = tx / 2;
    let yc = ty / 2;

    // el centro de la forma circular
    let circentro = new Circulo(xc, yc, 5, 'red');
    circentro.draw(context);

    // se generan los puntos en la circunferencia
    let grados = 0;
    let inc_grados = 360 / verticesJson.length;
    let hipote = 280;
    let radiocir = 50;
    let trigo2 = new Trigonometria(grados, hipote);
    let uncirculo = new Circulo();
    let radianes = 0;

    // para obtener los valores hacia la forma circular
    let xxc = 0;
    let yyc = 0;
    let circulos = [];

    // Dibujar los vértices y sus líneas al centro
    for (let i = 0; i < verticesJson.length; i++) {
        grados = i * inc_grados;
        radianes = (2 * Math.PI * grados) / 360; // conversión de los radianes
        trigo2.angulo = radianes;

        xxc = xc + trigo2.obtenerAdyacente();
        yyc = yc - trigo2.obtenerOpuesto();
        uncirculo.posiX = xxc;
        uncirculo.posiY = yyc;
        uncirculo.radio = radiocir;
        uncirculo.color = '#2E69D2';
        uncirculo.draw(context);

        circulos.push({
            x: xxc,
            y: yyc,
            radio: radiocir,
            Id: verticesJson[i].Id,
            Titulo: verticesJson[i].Titulo,
            aristas: verticesJson[i].aristas
        });
        context.textAlign = "center";
        context.textBaseline = "middle";
        context.font = "18px Candara";
        context.fillText(verticesJson[i].Titulo, xxc, yyc);
    }

    for (let i = 0; i < circulos.length; i++) {
        let circActual = circulos[i];
        for (let j = 0; j < circActual.aristas.length; j++) {
            let arista = circActual.aristas[j];
            let circDestino = circulos.find(c => c.numeroDato == arista.numeroDato);
            if (circDestino) {
                drawArrow(context, circActual.x, circActual.y, circDestino.x, circDestino.y, circActual.radio, circDestino.radio, arista.costo);
            }
        }
    }
}
//para crear la flecha 
function drawArrow(context, fromX, fromY, toX, toY, fromRadius, toRadius) {
    let headLength = 10; 
    let angle = Math.atan2(toY - fromY, toX - fromX);

    context.beginPath();
    context.moveTo(endX, endY);
    context.lineTo(endX - headLength * Math.cos(angle - Math.PI / 6), endY - headLength * Math.sin(angle - Math.PI / 6));
    context.moveTo(endX, endY);
    context.lineTo(endX - headLength * Math.cos(angle + Math.PI / 6), endY - headLength * Math.sin(angle + Math.PI / 6));
    context.stroke();

    let startX = fromX + Math.cos(angle) * fromRadius;
    let startY = fromY + Math.sin(angle) * fromRadius;
    let endX = toX - Math.cos(angle) * toRadius;
    let endY = toY - Math.sin(angle) * toRadius;

    // crear la linea 
    context.strokeStyle = '#1c646d';
    context.beginPath();
    context.moveTo(startX, startY);
    context.lineTo(endX, endY);
    context.stroke();

   
    
}