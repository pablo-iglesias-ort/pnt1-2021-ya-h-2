using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReservaCine.Models
{
    public class Reserva
    {
        public Guid Id { get; set; }

        [DisplayName("Función")]
        public Funcion Funcion { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Fecha de Reserva")]
        public DateTime FechaAlta { get; set; }

        [DisplayName("Cliente")]
        public Cliente Cliente { get; set; }

        [Required (ErrorMessage = "Ingrese cantidad válida de butacas")]
        [Range(1, 12, ErrorMessage = "Cantidad fuera de rango disponible")]
        [DisplayName("Cantidad de butacas")]
        public int CantidadButacas { get; set; }


     }

}