using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditosCore.Controllers.Clientes
{
    public class ClientesViewModel
    {
        public int ClienteId { get; set; }

    
        public string Nombre { get; set; }
      
        public string ApellidoPaterno { get; set; }
     
        public string ApellidoMaterno { get; set; }
        
        public string Correo { get; set; }

        public int numeroCreditos { get; set; }
        public decimal montoPendiente { get; set; }
    }
}
