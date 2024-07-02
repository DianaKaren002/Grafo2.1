using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassGrafoEntidades
{
    public class Libro
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }

        public Libro() { }
        public Libro(string titulo, string autor, int IdLibro)
        {
            Titulo = titulo;
            Autor = autor;
            Id = IdLibro;
        }

        public override string ToString()
        {
            return $"{Titulo} por {Autor} ({Id})";
        }
    }
}
