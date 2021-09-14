using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservaCine.Models
{
    public class Genero
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public List<Pelicula> Peliculas { get; set; }

        public Genero()
        {
        }

        public Genero(string nombre)
        {
            Nombre = nombre;
        }
    }
}