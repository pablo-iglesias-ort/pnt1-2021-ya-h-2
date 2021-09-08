using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservaCine.Models
{
    public class Persona
    {

        public String Nombre { get; set; }

        public String Apellido { get; set;}

        public long DNI { get; set; }

        public long Telefono { get; set; }

        public String Direccion { get; set; }

        public DateTime FechaALta { get; set; }

        public String Email { get; set; }

    }
}
