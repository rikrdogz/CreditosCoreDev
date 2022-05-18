using CreditosCore.Controllers.Clientes;
using CreditosCore.Controllers.Pagos;
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
        public int idPagoUltimo { get; set; }
        public string fechaUltimoPago { get; set; }
        public decimal montoTotal { get; set; }
        public decimal montoPagado { get; set; }
        public string cliente { get; set; }

    }
}
