using Autorizacion.Abstracciones.Modelos.Roles;
using Autorizacion.Abstracciones.Modelos.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autorizacion.Abstracciones.BW
{
    public interface IAutorizacionBW
    {
        Task<UsuarioResponse> ObtenerUsuario(UsuarioResponse usuarioResponse);
        Task<IEnumerable<RolesResponse>> ObtenerRolesxUsuario(UsuarioResponse usuarioResponse);
    }
}
