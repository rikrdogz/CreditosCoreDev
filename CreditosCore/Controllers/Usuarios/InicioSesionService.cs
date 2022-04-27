using CreditosCore.Database;
using CreditosCore.Shared;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
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

        public UsuarioSesionViewModel ValidarInicio(UsuarioPorIniciarViewModel usuarioData)
        {
            try
            {
                var usuario = db.usuarios.Where(u => u.nickname == usuarioData.nickName && u.contra == usuarioData.contra).FirstOrDefault();

                if (usuario == null)
                {
                    throw new CreditoSistemaExcepcion("usuario o contraseña incorrectos");
                }
                return new UsuarioSesionViewModel() { nickName = usuario.nickname, token="" };
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string GenerarToken(UsuarioPorIniciarViewModel usuario)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(DataShared.KeyJWT));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(DataShared.ConfigIssuer,
             DataShared.ConfigIssuer,
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
