using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReservaCine.Models
{
    public class Reserva
    {
      
        [Display(Name = "Cliente")]
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }


        [Required(ErrorMessage = "Por favor ingrese la funcion")]
        [Display(Name = "Función")]
        public int FuncionId { get; set; }
        public Funcion Funcion { get; set; }

 // Verificar ya que decia que no tenian limite, yo le puse un numero grande

        [Required(ErrorMessage = "Por favor ingresar numero de butacas")]
        [Range(1, 999999, ErrorMessage = "La cantidad de butacas debe ser entre 1 a 9999999")]
        [Display(Name = "Cantidad de butacas")]
        public int CantButacas { get; set; }


        [Display(Name = "Costo total")]
        public decimal CostoTotal { get; set; }


        [Display(Name = "Fecha de alta")]
        public DateTime FechaDeAlta { get; set; }



    }

}