using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ReservaCine.Models
{
    public class Pelicula
    {
        public Guid Id { get; set; }

        [DisplayName("Fecha de estreno")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaLanzamiento { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2) ]
        [DisplayName("Nombre de la pelicula")]
        public String Titulo { get; set; }

        [Required]
        [StringLength(6000, MinimumLength = 10)]
        [DisplayName("Descripción")]
        public String Descripcion { get; set; }

        [Required]
        [Range(60,200, ErrorMessage = "La duración deber ser entre 60 y 200 minutos")]
        [DisplayName("Duración")]
        public int Duracion { get; set; }

        [Required]
        public Genero Genero;

        public Funcion Funcion;

       public Pelicula ()
        {

        }        
    }
}
