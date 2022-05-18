using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditosCore.Controllers.Pagos
{
    public class PagoViewModel
    {
        public int idPago { get; set; }
        public string nombre { get; set; }
        public decimal monto { get; set; }
        public decimal descuento { get; set; }
        public decimal faltaPago { get; set; }
        public string fechaPago { get; set; }
        public string fechaCreacionPago { get; set; }
        public string numeroPago { get; set; }
    }
}
