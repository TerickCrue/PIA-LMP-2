using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace PIA_LMP_API.Data.Helpers
{
    public static class Utileria
    {
        public static int ObtenerIdUsuarioSesion(ControllerBase controller)
        {
            if (controller.User.FindFirst(ClaimTypes.NameIdentifier) == null)
            {
                throw new UnauthorizedException("Acceso no autorizado");
            }

            return int.Parse(controller.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }

        public static int TryObtenerIdUsuarioSesion(ControllerBase controller)
        {
            if (controller.User.FindFirst(ClaimTypes.NameIdentifier) == null)
            {
                return 0;
            }

            return int.Parse(controller.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }

        public static string ObtenerToken(ControllerBase controller)
        {
            string jwt = controller.HttpContext.Request.Headers["Authorization"];
            string stream = jwt;

            if (stream != null)
            {
                stream = stream.Replace("Bearer ", "");
                return stream;
            }

            return null;
        }
    }
}
