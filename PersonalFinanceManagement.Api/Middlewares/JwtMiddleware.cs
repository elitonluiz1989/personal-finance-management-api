using System.Security.Claims;

namespace PersonalFinanceManagement.Api.Middlewares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            AttachUserToContext(httpContext);

            return _next(httpContext);
        }

        private static void AttachUserToContext(HttpContext context)
        {
            if (context.User.Claims.Any() is false)
                return;

            var success = int.TryParse(context.User.Claims.First(c => c.Type == ClaimTypes.Sid).Value, out int userId);

            if (success is false)
                return;

            context.Items.Add("UserId", userId);
        }
    }
}
