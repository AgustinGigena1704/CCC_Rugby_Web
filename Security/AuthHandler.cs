using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;
using static CCC_Rugby_Web.Services.Constants;

namespace CCC_Rugby_Web.Security
{
    public class AuthHandler : AuthenticationHandler<CustomOptions>
    {
        private readonly AuthStateComponent authStateComponent;
        public AuthHandler(IOptionsMonitor<CustomOptions> options, ILoggerFactory logger, UrlEncoder encoder, AuthStateComponent authStateComponent)
            : base(options, logger, encoder)
        {
            
            this.authStateComponent = authStateComponent;
        }
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            string? token = Context.Request.Cookies[TokenCookieName];
            if (string.IsNullOrEmpty(token))
                return AuthenticateResult.Fail("Authentication Failed");

            var userClaims = authStateComponent.VerifyUser(token);
            if (userClaims != null)
            {
                var principal = new ClaimsPrincipal(new ClaimsIdentity(userClaims, "JWT"));
                var ticket = new AuthenticationTicket(principal, Scheme.Name);
                return AuthenticateResult.Success(ticket);
            }

            return AuthenticateResult.Fail("Authentication Failed");
        }

        protected override Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            var returnUrl = Context.Request.Path + Context.Request.QueryString;
            Context.Response.Redirect($"/Auth/Login?returnUrl={UrlEncoder.Default.Encode(returnUrl)}");
            return Task.CompletedTask;
        }

        protected override Task HandleForbiddenAsync(AuthenticationProperties properties)
        {
            Context.Response.Redirect("/Auth/NotAuth");
            return Task.CompletedTask;
        }
    }

    public class CustomOptions : AuthenticationSchemeOptions
    {

    }
}
