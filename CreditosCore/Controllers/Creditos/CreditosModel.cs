using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CreditosCore.Controllers.Creditos
{
    [Table("creditos")]
    public class CreditosModel
    {
        [Key]
        public int CreditoId { get; set; }
        [Required]
        public int ClienteId { get; set; }
        [Required]
        public string Folio { get; set; }
        [Required]
        public DateTime FechaCreacion { get; set; }
        [Required]
        public DateTime FechaModificacion { get; set; }

        [Required]
        [Column(TypeName ="Date")]
        public DateTime FechaCredito { get; set; }

        #region Amount

        /// <summary>
        /// Prestamo
        /// </summary>
        [Required]
        public decimal MontoPrestamo { get; set; }

        [Required]
        public decimal MontoInteres { get; set; }

        [Required]
        public decimal MontoTotal { get; set; }

        [Required]
        public decimal MontoPago { get; set; }

        #endregion
        /// <summary>
        /// Plazos
        /// </summary>
        [Required]
        public int Plazos { get; set; }

        [Required]
        public int idUsuario { get; set; }

        [Required]
        public decimal DescuentoPagoFinal { get; set; }

        [Required]
        public int TipoPago { get; set; }

        [Required]
        public decimal ComisionFaltaPago { get; set; }
    }
}
