using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using BookStoreAPI.Interface;
using BookStoreAPI.Data;
using Microsoft.Extensions.DependencyInjection;

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

            // Resolva o serviço IUserService usando IServiceProvider
            var userService = context.HttpContext.RequestServices.GetService<IUserService>();
            var userContextService = context.HttpContext.RequestServices.GetService<UserContextService>();

            // Verifique se o usuário está realmente autenticado
            if (user?.Identity?.IsAuthenticated == true)
            {

                var email = user?.Identity?.Name;
                var roleNameAndUserId = await userService.GetRoleUserByEmailAsync(email);

                // Verifique se o usuário tem uma das roles especificadas
                if (_roles.Any(role => role == roleNameAndUserId.roleName))
                {

                    userContextService.roleName = roleNameAndUserId.roleName;
                    userContextService.userId = roleNameAndUserId.userId;

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
