using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditosCore.Shared
{
    public class CreditoSistemaExcepcion : Exception
    {
        public CreditoSistemaExcepcion()
        {

        }

        public CreditoSistemaExcepcion(string Mensaje): base(Mensaje)
        {

        }

        public CreditoSistemaExcepcion(string Mensaje, Exception Excepcion):base(Mensaje, Excepcion)
        {

        }
    }
}
