using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autorizacion.Abstracciones.Modelos.Roles {
    public class RolesTablaBase {
        public string Nombre { get; set; }
    }   
    public class RolesResponse : RolesTablaBase {
        public Guid IdRol { get; set; }
    }
}
