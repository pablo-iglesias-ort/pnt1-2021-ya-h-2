using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservaCine.Models
{
    public class Genero
    {
        private string v;

        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public List<Pelicula> Peliculas { get; set; }


        public Genero(string v, object a)
        {
            this.v = v;

        }

        public Genero()
        {
        }

        public Genero(string v)
        {
            this.v = v;
        }
    }
}