using Autorizacion.Abstracciones.BW;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Autorizacion.Abstracciones.Modelos.Usuario;
using Autorizacion.Abstracciones.Modelos.Roles;

namespace Autorizacion.Middleware
{
    public class ClaimsRoles
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private IAutorizacionBW _autorizacionBW;

        public ClaimsRoles(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext httpContext, IAutorizacionBW autorizacionBW)
        {
            _autorizacionBW = autorizacionBW;
            ClaimsIdentity appIdentity = await verificarAutorizacion(httpContext);
            httpContext.User.AddIdentity(appIdentity);
            await _next(httpContext);
        }

        private async Task<ClaimsIdentity> verificarAutorizacion(HttpContext httpContext)
        {
            var claims = new List<Claim>();
            if (httpContext.User != null && httpContext.User.Identity.IsAuthenticated)
            {
                await ObtenerRoles(httpContext, claims);
                await ObtenerUsuario(httpContext, claims);
            }
            var appIdentity = new ClaimsIdentity(claims);
            return appIdentity;
        }

        private async Task ObtenerRoles(HttpContext httpContext, List<Claim> claims)
        {
            var roles = await obtenerInformacionRoles(httpContext);
            if (roles != null && roles.Any())
            {
                foreach (var rol in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, rol.IdRol.ToString()));
                }
            }
        }

        private async Task ObtenerUsuario(HttpContext httpContext, List<Claim> claims)
        {
            var usuario = await obtenerInformacionUsuario(httpContext);
            if (usuario is not null && string.IsNullOrEmpty(usuario.IdUsuario.ToString()) && string.IsNullOrEmpty(usuario.Correo.ToString()))
            {
                claims.Add(new Claim(ClaimTypes.Email, usuario.Correo));
                claims.Add(new Claim("IdUsuario", usuario.IdUsuario.ToString()));
            }
        }

        private async Task<UsuarioResponse> obtenerInformacionUsuario(HttpContext httpContext)
        {
            return await _autorizacionBW.ObtenerUsuario(new Abstracciones.Modelos.Usuario.UsuarioResponse 
            { 
                Correo = httpContext.User.Claims.Where(c => c.Type == "Correo").FirstOrDefault().Value 
            });
        }

        private async Task<IEnumerable<RolesResponse>> obtenerInformacionRoles(HttpContext httpContext)
        {
            return await _autorizacionBW.ObtenerRolesxUsuario(new Abstracciones.Modelos.Usuario.UsuarioResponse 
            { 
                Correo = httpContext.User.Claims.Where(c => c.Type == "Correo").FirstOrDefault().Value 
            });
        }

    }
    public static class ClaimsUsuarioMiddlewareExtensions
    {
        public static IApplicationBuilder AutorizacionClaims(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ClaimsRoles>();
        }
    }
}
