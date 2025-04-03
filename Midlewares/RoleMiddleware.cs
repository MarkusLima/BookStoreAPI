using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Midlewares
{

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class RoleMiddleware : Attribute, IAsyncActionFilter
    {
        private readonly string[] _roles;

        public RoleMiddleware(params string[] roles)
        {
            _roles = roles;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var user = context.HttpContext.User;

            // Verifique se o usuário está autenticado
            if (user?.Identity?.IsAuthenticated == true)
            {
                // Verifique se o usuário tem uma das roles especificadas
                if (_roles.Any(role => user.IsInRole(role)))
                {
                    // Continue para a próxima ação
                    await next();
                    return;
                }
            }

            // Se o usuário não estiver autenticado ou não tiver a role necessária, retorne 403 Forbidden
            context.Result = new ForbidResult();
        }
    }
}
