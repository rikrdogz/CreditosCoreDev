using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreditosCore.Controllers.Clientes
{
    [Table("Clientes")]
    public class ClientesModel
    {
        [Key]
        public int ClientId { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(60)]
        public string FirstName { get; set; }
        [MaxLength(60)]
        public string LastName { get; set; }
        [MaxLength(250)]
        public string Email { get; set; }
    }
}
