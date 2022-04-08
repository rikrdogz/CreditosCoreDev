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
        public int ClienteId { get; set; }
        [MaxLength(50)]

        
        public string Nombre { get; set; }
        [MaxLength(60)]
        public string ApellidoPaterno { get; set; }
        [MaxLength(60)]
        public string ApellidoMaterno { get; set; }
        [MaxLength(250)]
        public string Correo { get; set; }
    }
}
