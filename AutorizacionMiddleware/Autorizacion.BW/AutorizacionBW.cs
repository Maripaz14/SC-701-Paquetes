using Autorizacion.Abstracciones.BW;
using Autorizacion.Abstracciones.DA;
using Autorizacion.Abstracciones.Entidades.Roles;
using Autorizacion.Abstracciones.Modelos.Roles;
using Autorizacion.Abstracciones.Modelos.Usuario;

namespace Autorizacion.BW
{
    public class AutorizacionBW : IAutorizacionBW
    {
        private ISeguridadDA _seguridadDA;

        public AutorizacionBW(ISeguridadDA seguridadDA)
        {
            _seguridadDA = seguridadDA;
        }

        public async Task<IEnumerable<Abstracciones.Modelos.Roles.RolesResponse>> ObtenerRolesxUsuario(UsuarioResponse usuarioResponse)
        {
            return await _seguridadDA.ObtenerRolesxUsuario(usuarioResponse);
        }

        public  async Task<UsuarioResponse> ObtenerUsuario(UsuarioResponse usuarioResponse)
        {
            return await _seguridadDA.ObtenerUsuario(usuarioResponse);
        }
    }
}
