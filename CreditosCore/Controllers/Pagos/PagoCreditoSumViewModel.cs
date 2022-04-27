using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditosCore.Controllers.Pagos
{
    public class PagoCreditoSumViewModel
    {
        public int creditoId { get; set; }
        public decimal sumaMontos { get; set; }
        public decimal montoPagoRecurrente { get; set; }
    }
}
