using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReservaCine.Models
{
    public class Sala
    {
      
  

        [Required(ErrorMessage = "Por favor ingrese un nombre")]
        [MaxLength(20, ErrorMessage = "Por favor ingrese un maximo de 20 caracteres")]
        [MinLength(2, ErrorMessage = "Por favor ingrese un minimo de 2 caracteres")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }


        [Display(Name = "Tipo de Sala")]
        public int TipoId { get; set; }
        public TipoSala Tipo { get; set; }


        [Required(ErrorMessage = "Por favor ingresar capacidad")]
        [Display(Name = "Capacidad Total")]
        [Range(15, 200, ErrorMessage = "La capacidad de la sala debe ser entre 15 y 200")]
        public int CapacidadTotal { get; set; }


        [Display(Name = "Funciones")]
        public List<Funcion> Funciones { get; set; }


    }
}