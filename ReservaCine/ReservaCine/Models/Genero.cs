using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ReservaCine.Models
{
    public class Genero
    {
        private string v;

        public Genero(string v)
        {
            this.v = v;
        }

     
        [Required(ErrorMessage = "Por favor ingrese un nombre")]
        [MaxLength(20, ErrorMessage = "Por favor ingrese un maximo de 20 caracteres")]
        [MinLength(2, ErrorMessage = "Por favor ingrese un minimo de 2 caracteres")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }


        [Display(Name = "Películas")]
        public List<PeliculaGenero> Peliculas { get; set; }


    }
}
