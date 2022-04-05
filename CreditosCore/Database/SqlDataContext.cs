using CreditosCore.Controllers.Clientes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditosCore.Database
{
    public class SqlDataContext: DbContext
    {
        public DbSet<ClientesModel> clientes { get; set; }

    }
}
