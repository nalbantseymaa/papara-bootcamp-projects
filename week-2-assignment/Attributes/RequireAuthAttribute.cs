using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;

public class RequireAuthAttribute : Attribute, IAuthorizationFilter
{
    private const string ValidApiKey = "my-static-api-key"; // Valid API Key

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // Get the Authorization header
        if (!context.HttpContext.Request.Headers.TryGetValue("Authorization", out StringValues authHeader))
        {
            context.Result = new UnauthorizedObjectResult(new { Message = "Access denied. Authorization header is missing!" });
            return;
        }

        // Check for Bearer token
        var token = authHeader.ToString().Replace("Bearer ", "").Trim();
        if (token != ValidApiKey)
        {
            context.Result = new UnauthorizedObjectResult(new { Message = "Access denied. Invalid API Key!" });
            return;
        }
    }
}
