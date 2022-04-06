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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=tcp:creditos.database.windows.net,1433;Initial Catalog=creditos_dev;Persist Security Info=False;User ID=ricardo;Password=Rikrdogz7;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

    }
}
