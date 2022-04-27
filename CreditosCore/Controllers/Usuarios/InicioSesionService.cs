using CreditosCore.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditosCore.Controllers.Usuarios
{
    public class InicioSesionService
    {
        SqlDataContext db;
        public InicioSesionService()
        {
            db = new SqlDataContext();
        }

        public int CantidadUsuarios()
        {
            return db.usuarios.Count();
            
        }


    }
}
