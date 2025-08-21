using Microsoft.AspNetCore.Http;
using System.Security.Claims;

public class ApiKeyClaimsInjectionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;

    public ApiKeyClaimsInjectionMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
    }

    /// <summary>
    /// Invoke method to check for API key and inject claims into the user principal.
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task Invoke(HttpContext context)
    {
        // Check for API key and decide on the roles/claims to add
        if (context.Request.Headers.TryGetValue("ApiKey", out var apiKey))
        {
            // Validate the API key here
            if (apiKey == _configuration["Api:Key"])
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Role, "API_USER"),
                };

                var identity = new ClaimsIdentity(claims, "ApiKey");
                var claimsPrincipal = new ClaimsPrincipal(identity);
                context.User = claimsPrincipal;
            }
        }

        await _next(context);
    }
}