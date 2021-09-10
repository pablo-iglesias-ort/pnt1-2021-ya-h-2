using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace ReservaCine.Models
{
    public class Funcion
    {
      

        [Required(ErrorMessage = "Por favor ingrese una fecha")]
        [Display(Name = "Fecha")]
        public DateTime Fecha { get; set; }


        [Required(ErrorMessage = "Por favor ingrese una hora")]
        [Display(Name = "Hora")]
        public DateTime Hora { get; set; }


        [Required(ErrorMessage = "Por favor ingrese una descripcion")]
        [MaxLength(20, ErrorMessage = "Por favor ingrese un maximo de 20 caracteres")]
        [MinLength(2, ErrorMessage = "Por favor ingrese un minimo de 2 caracteres")]
        [Display(Name = "Descripcion")]
        public string Descripcion { get; set; }


        [Display(Name = "Butacas Disponibles")]
        public int CantButacasDisponibles { get; set; }

        [Display(Name = "Confirmar")]
        public Boolean Confirmar { get; set; }

      
        [Required(ErrorMessage = "Por favor ingresar pelicula")]
        [Display(Name = "Película")]
        public int PeliculaId { get; set; }
        public Pelicula Pelicula { get; set; }

       
        [Required(ErrorMessage = "Por favor ingresar sala")]
        [Display(Name = "Sala")]
        public int SalaId { get; set; }
        public Sala Sala { get; set; }


        [Display(Name = "Reservas")]
        public List<Reserva> Reservas { get; set; }



      


    }
}