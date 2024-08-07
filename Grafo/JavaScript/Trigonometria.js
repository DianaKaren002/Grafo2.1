class Trigonometria {
    constructor(angulo, hipo) {
        this.angulo = angulo;
        this.hipotenusa = hipo;
    }
    obtenerAdyacente() {
        let adyacente = Math.cos(this.angulo) * this.hipotenusa;
        return adyacente;
    }

    obtenerOpuesto() {
        let opuesto = Math.sin(this.angulo) * this.hipotenusa;
        return opuesto;
    }
}