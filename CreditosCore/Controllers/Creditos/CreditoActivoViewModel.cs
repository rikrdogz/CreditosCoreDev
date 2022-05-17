using CreditosCore.Controllers.Clientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditosCore.Controllers.Creditos
{
    public class CreditoActivoViewModel
    {
        public int idCredito { get; set; }
        public decimal pendientePago { get; set; }
        public int numeroPago { get; set; }
        public decimal montoPrestado { get; set; }
        public decimal montoPagado { get; set; }
        public string cliente { get; set; }
    }
}
