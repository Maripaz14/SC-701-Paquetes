using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autorizacion.Abstracciones.Entidades.Usuario {
    public class UsuarioTablaBase
    {

        [Required]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required]
        [Display(Name = "Primer Apellido")]
        public string PrimerApellido { get; set; }

        [Required]
        [Display(Name = "Segundo Apellido")]
        public string SegundoApellido { get; set; }

        [Required]
        [Display(Name = "Correo Electrónico")]
        [EmailAddress]
        public string Correo { get; set; }

        [Required]
        [Display(Name = "Teléfono")]
        [Phone]
        public string Telefono { get; set; }

        [Required]
        [Display(Name = "Contraseña")]
        public string Contrasena { get; set; }

    }
    public class UsuarioResponse : UsuarioTablaBase {

        [Required]
        public Guid IdUsuario { get; set; }

        [Required]
        public Guid IdEstado { get; set; }

        public Guid IdRol { get; set; }

        public string Estado { get; set; }
        public string Rol { get; set; }

    }
}
