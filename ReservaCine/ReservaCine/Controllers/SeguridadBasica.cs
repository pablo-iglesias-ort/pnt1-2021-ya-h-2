using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservaCine.Controllers
{
    public class SeguridadBasica : ISeguridad
    {
        public byte[] EncriptarPass(string pass)
        {
            throw new NotImplementedException();
        }

        public bool ValidarPass(string pass)
        {
            throw new NotImplementedException();
        }
    }
}
