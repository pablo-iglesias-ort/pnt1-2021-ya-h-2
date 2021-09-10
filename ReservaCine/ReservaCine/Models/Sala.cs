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
