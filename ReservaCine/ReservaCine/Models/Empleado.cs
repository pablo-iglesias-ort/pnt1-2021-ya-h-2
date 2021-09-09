using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReservaCine.Models
{
    public class Empleado
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellido { get; set; }
        [Required]
        public int dni { get; set; }
        [Required]
        public int Telefono { get; set; }
        [Required]
        public string Direccion { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaAlta { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public int Legajo { get; set; }
    }
}
