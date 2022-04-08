using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CreditosCore.Controllers.Creditos
{
    [Table("creditosamortiguacion")]
    public class CreditoAmortizacionPagosModel
    {
        [Key]
        public int CreditoAmortizacionId { get; set; }

        [Required]
        public int NumeroPago { get; set; }

        [Required]
        [ForeignKey("CreditoId")]
        public int CreditoId { get; set; }
        public CreditosModel credito { get; set; }

        [Required]
        public decimal Monto { get; set; }

        [Required]
        public DateTime FechaVencimiento { get; set; }

        [Required]
        public int EstatusId { get; set; }


    }
}
