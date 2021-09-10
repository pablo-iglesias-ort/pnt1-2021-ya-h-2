using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservaCine.Models
{
    public class Reserva
    {
        public Guid Id { get; set; }

        public Funcion Funcion { get; set; }

        public DateTime FechaAlta { get; set; }

        public Cliente Cliente { get; set; }

        public int CantidadButacas { get; set; }

    }
}
