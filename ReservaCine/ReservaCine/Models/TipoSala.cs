using System;
using System.Collections.Generic;
<<<<<<< HEAD
=======
using System.ComponentModel;
>>>>>>> 19b22ae9c3abdfd0bd861f76d4f9c5649f935f93
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReservaCine.Models
{
    public class TipoSala
    {
<<<<<<< HEAD
        


        [Required(ErrorMessage = "El campo Nombre es requerido")]
        [MaxLength(20, ErrorMessage = "La longitud máxima de 20 caracteres")]
        [MinLength(2, ErrorMessage = "La longitud mínima es de 2 caracteres")]
        [Display(Name = "Nombre")]
        public String Nombre { get; set; }


        [Required(ErrorMessage = "El campo Precio de Entrada es requerido")]
        [Range(0, (double)decimal.MaxValue, ErrorMessage = "El precio de la entrada no puede ser negativo")]
        [Display(Name = "Precio de entrada")]
        public decimal PrecioEntrada { get; set; }
    }
}
=======
      
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Ingrese un nombre por favor")]
        [StringLength(20, MinimumLength = 3)]
        [DisplayName("Nombre de la sala")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Ingrese un precio por favor")]
        public double Precio { get; set; }

        
    }
}
>>>>>>> 19b22ae9c3abdfd0bd861f76d4f9c5649f935f93
