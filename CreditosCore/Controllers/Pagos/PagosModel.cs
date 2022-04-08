using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditosCore.Controllers.Pagos
{
    public class PagosModel
    {
        public int PagoId { get; set; }
        public int CreditoId { get; set; }
        public decimal Monto { get; set; }
        public DateTime fechaCreacion { get; set; }
        public DateTime fechaPago { get; set; }
        public int EstatusId { get; set; }
        public int idUsuario { get; set; }
    }
}
