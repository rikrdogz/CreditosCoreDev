using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CreditosCore.Controllers.Usuarios
{
    [Table("usuarios")]
    public class UsuariosModel
    {
        [Key]
        public int usuarioID { get; set; }
        
        [MaxLength(12)]
        public string nickname { get; set; }
        
        [MaxLength(25)]
        public string nombre { get; set; }
        
        [MaxLength(40)]
        public string apellidoPaterno { get; set; }
        public string contra { get; set; }
        public DateTime ultimoInicioSesion { get; set; }

    }
}
