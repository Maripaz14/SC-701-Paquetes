using Autorizacion.Abstracciones.DA;
using Autorizacion.Abstracciones.Modelos.Roles;
using Autorizacion.Abstracciones.Modelos.Usuario;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Autorizacion.Abstracciones.Modelos.UsuarioRoles;
using Helpers;
using Autorizacion.Abstracciones.Entidades.Roles;

namespace Autorizacion.DA
{
    public class SeguridadDA : ISeguridadDA
    {
        IRepositorioDapper _repositorioDapper;
        private SqlConnection _sqlConnection;

        public SeguridadDA(IRepositorioDapper repositorioDapper, SqlConnection sqlConnection)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = repositorioDapper.ObtenerRepositorio();
        }

        public async Task<IEnumerable<Abstracciones.Modelos.Roles.RolesResponse>> ObtenerRolesxUsuario(UsuarioResponse usuarioResponse)
        {
            string sql = @"[ObtenerRolesxUsuario]";
            var consulta = await _sqlConnection.QueryAsync<Abstracciones.Entidades.Roles.RolesResponse>(sql, new
            {
                IdUsuario = usuarioResponse.IdUsuario

            });
            return Convertidor.ConvertirLista<Abstracciones.Entidades.Roles.RolesResponse, Abstracciones.Modelos.Roles.RolesResponse>(consulta);
        }

        public async Task<UsuarioResponse> ObtenerUsuario(UsuarioResponse usuarioResponse)
        {
            string sql = @"[ObtenerUsuario]";
            var consulta = await _sqlConnection.QueryAsync<Abstracciones.Entidades.Usuario.UsuarioResponse>(sql, new
            {
                IdUsuario = usuarioResponse.IdUsuario
            });
            return Convertidor.Convertir<Abstracciones.Entidades.Usuario.UsuarioResponse, Abstracciones.Modelos.Usuario.UsuarioResponse>(consulta.FirstOrDefault());
        }
    }
}
