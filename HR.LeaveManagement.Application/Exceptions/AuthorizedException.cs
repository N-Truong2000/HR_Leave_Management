using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Text.Encodings.Web;

namespace HR.LeaveManagement.Application.Exceptions
{
    public class CustomAuthenticationOptions : AuthenticationSchemeOptions
    {
        // You can add custom properties here if needed
    }

    public class AuthorizedException : AuthenticationHandler<CustomAuthenticationOptions>
    {
        public AuthorizedException(
            IOptionsMonitor<CustomAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // Your authentication logic here
            // For demo purposes, assume authentication fails
            return AuthenticateResult.Fail("Access denied. User is not authorized 123.");
        }

        protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            // This method is called when an unauthenticated request tries to access a resource that requires authentication
            Response.StatusCode = 401;
            var errorResponse = new
            {
                type = "AuthorizedException",
                title = "Access denied. User is not authorized.",
                status = 401,
                errors = new { }
            };

            var errorResponseJson = JsonSerializer.Serialize(errorResponse);
            await Response.Body.WriteAsync(System.Text.Encoding.UTF8.GetBytes(errorResponseJson));
        }
    }
}
