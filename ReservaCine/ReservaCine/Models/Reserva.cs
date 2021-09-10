using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReservaCine.Models
{
    public class Reserva
    {
        public Guid Id { get; set; }
      
        [Display(Name = "Cliente")]
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }


        [Required(ErrorMessage = "Por favor ingrese la funcion")]
        [Display(Name = "Función")]
        public int FuncionId { get; set; }
        public Funcion Funcion { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        // [DisplayName("Fecha de Alta")]
        public DateTime FechaAlta { get; set; }

        public int CantidadButacas { get; set; }

    }

}