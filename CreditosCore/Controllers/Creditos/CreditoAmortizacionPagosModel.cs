using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditosCore.Controllers.Creditos
{
    public class CreditoAmortizacionPagosModel
    {
        public int CreditoAmortizacionId { get; set; }
        public int NumeroPago { get; set; }
        public int CreditoId { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public int EstatusId { get; set; }


    }
}
