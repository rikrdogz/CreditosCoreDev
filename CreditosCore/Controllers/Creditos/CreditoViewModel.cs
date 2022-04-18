using CreditosCore.Controllers.Clientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditosCore.Controllers.Creditos
{
    public class CreditoViewModel
    {
        public ClientesModel cliente { get; set; }
        public CreditosModel credito { get; set; }
    }
}
