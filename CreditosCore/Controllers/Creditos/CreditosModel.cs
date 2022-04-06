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
        public int ClientId { get; set; }
        public string Folio { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedtionDate { get; set; }

        #region Amount

        /// <summary>
        /// Prestamo
        /// </summary>
        public decimal IoanAmount { get; set; }
        public decimal InterestAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PaymentAmount { get; set; }

        #endregion
        /// <summary>
        /// Plazos
        /// </summary>
        public int IoanTerm { get; set; }
        public int IdUser { get; set; }
        public decimal AmountDiscountPaymentFinal { get; set; }
        public int TypePayment { get; set; }
        public decimal NonPaymentCommission { get; set; }
    }
}
