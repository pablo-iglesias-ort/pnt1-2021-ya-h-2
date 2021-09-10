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
    public class Sala
    {
<<<<<<< HEAD
      
  

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
=======
        
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Ingrese un numero por favor")]
        public int Numero { get; set; }

        [Required(ErrorMessage = "Ingrese el tipo de sala por favor")]
        [DisplayName("Tipo de Sala")]
        public TipoSala tipoSala { get; set; }

        [Required(ErrorMessage = "Ingresela capacidad de butacas por favor")]
        [Range(40, 200, ErrorMessage = "La cantidad ingresada es incorrecta")]
        [DisplayName("Capacidad de butacas")]
        public int CapacidadButacas { get; set; }

        
        public List<Funcion> funciones { get; set; }

    }
}
>>>>>>> 19b22ae9c3abdfd0bd861f76d4f9c5649f935f93
