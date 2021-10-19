using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReservaCine.Models
{
    public class Empleado : Usuario
    {
        [Required(ErrorMessage ="Ingrese el legajo")]
        [Range(0, 99999999999, ErrorMessage = "Ingrese un número válido")]
        public int Legajo { get; set; }

       /* public Empleado(String nombreU, String apellidoU, long dni, String email, String domicilio, long telefono, String nombreUsuario, String password, int legajo)
           : base(nombreU, apellidoU, dni, email, domicilio, telefono, nombreUsuario, password)
        {
            Legajo = legajo;

            // Reserva = reserva;
        }*/

        public Empleado ()
        {

        }
    }
}
