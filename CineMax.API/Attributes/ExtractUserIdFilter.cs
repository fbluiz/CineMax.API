using CineMax.Application.Commands.CreateUser;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CineMax.API.Attributes
{
    public class ExtractUserIdFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                var userId = context.HttpContext.User.FindFirst("sub")?.Value;

                if (!string.IsNullOrEmpty(userId))
                {
                    if (Guid.TryParse(userId, out Guid userIdGuid))
                    {
                        foreach (var argument in context.ActionArguments.Values)
                        {
                            var userIdProperty = argument.GetType().GetProperty("UserId");
                            if (userIdProperty != null && userIdProperty.CanWrite && userIdProperty.PropertyType == typeof(Guid))
                            {
                                userIdProperty.SetValue(argument, userIdGuid);
                            }
                        }
                    }
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
