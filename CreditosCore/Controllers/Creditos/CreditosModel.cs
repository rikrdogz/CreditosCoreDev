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
        public int CreditId { get; set; }
        [Required]
        public int ClientId { get; set; }
        [Required]
        public string Folio { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public DateTime UpdatedtionDate { get; set; }

        #region Amount

        /// <summary>
        /// Prestamo
        /// </summary>
        [Required]
        public decimal IoanAmount { get; set; }

        [Required]
        public decimal InterestAmount { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        [Required]
        public decimal PaymentAmount { get; set; }

        #endregion
        /// <summary>
        /// Plazos
        /// </summary>
        [Required]
        public int IoanTerm { get; set; }

        [Required]
        public int IdUser { get; set; }

        [Required]
        public decimal AmountDiscountPaymentFinal { get; set; }

        [Required]
        public int TypePayment { get; set; }

        [Required]
        public decimal NonPaymentCommission { get; set; }
    }
}
