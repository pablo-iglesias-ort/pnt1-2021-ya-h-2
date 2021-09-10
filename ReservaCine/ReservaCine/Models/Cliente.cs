using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservaCine.Models
{
    public class Cliente : Usuario
    {
        public List<Reserva> Reservas { get; set; }
    }
}
