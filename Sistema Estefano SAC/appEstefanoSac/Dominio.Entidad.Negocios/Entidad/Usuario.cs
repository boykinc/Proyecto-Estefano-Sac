using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidad.Negocios.Entidad
{
    public class Usuario
    {

        public int IdUsuario { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "Correo requerido")]
        //[EmailAddress(ErrorMessage = "Formato incorrecto")]
        [RegularExpression("[a-zA-Z0-9_]+([.][a-zA-Z0-9_]+)*@[a-zA-Z0-9_]+([.][a-zA-Z0-9_]+)*[.][a-zA-Z]{2,5}", ErrorMessage = "Formato incorrecto")]
        public string Correo { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "Clave requerida")]
        [RegularExpression(" /^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[$@$!%*?&#.$($)$-$_])[A-Za-z\\d$@$!%*?&#.$($)$-$_]{8,15}$/", ErrorMessage = "Formato incorrecto")]
        public string Clave { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Clave requerida")]
        [RegularExpression(" /^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[$@$!%*?&#.$($)$-$_])[A-Za-z\\d$@$!%*?&#.$($)$-$_]{8,15}$/", ErrorMessage = "Formato incorrecto")]
        public string ComfirmarClave { get; set; }
    }
}
