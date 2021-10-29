using System;
using System.Collections.Generic;

using System.ComponentModel;

using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReservaCine.Models
{
    public class Sala
    { 
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Ingrese un número")]
        [Range(0, 99999999999, ErrorMessage = "Ingrese un número válido")]
        public int Numero { get; set; }

        [DisplayName("Tipo de Sala")]
        public TipoSala tipoSala { get; set; }

        [Required(ErrorMessage = "Ingrese la capacidad de butacas")]
        [Range(40, 200, ErrorMessage = "La cantidad ingresada es incorrecta")]
        [DisplayName("Capacidad de butacas")]
        public int CapacidadButacas { get; set; }

        
        public IEnumerable<Funcion> Funciones { get; set; }

    }
}

