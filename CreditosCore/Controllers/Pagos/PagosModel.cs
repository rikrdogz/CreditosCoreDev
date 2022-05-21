using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CreditosCore.Controllers.Pagos
{
    [Table("pagos")]
    public class PagosModel
    {
        [Key]
        public int PagoId { get; set; }
        public int CreditoId { get; set; }
        public decimal Monto { get; set; }
        public decimal descuento { get; set; }
        public decimal faltaDePago { get; set; }
        public DateTime fechaCreacion { get; set; }
        public DateTime fechaPago { get; set; }
        public int EstatusId { get; set; }
        public int idUsuario { get; set; }
        [MaxLength(80)]
        public string observacion { get; set; }

    }
}
