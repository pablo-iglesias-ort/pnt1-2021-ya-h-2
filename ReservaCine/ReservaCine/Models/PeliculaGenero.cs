using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ReservaCine.Models
{
    public class PeliculaGenero
    {
      

        [Display(Name = "Pelicula")]
        public int PeliculaId { get; set; }
        public Pelicula Pelicula { get; set; }


        [Display(Name = "Genero")]
        public int GeneroId { get; set; }
        public Genero Genero { get; set; }


    }
}