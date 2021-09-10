using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReservaCine.Models
{
    public class TipoSala
    {
      
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Ingrese un nombre por favor")]
        [StringLength(20, MinimumLength = 3)]
        [DisplayName("Nombre de la sala")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Ingrese un precio por favor")]
        public double Precio { get; set; }

        
    }
}
