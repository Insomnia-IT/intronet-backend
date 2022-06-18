using Microsoft.AspNetCore.Authentication;
using Insomnia.Portal.BI.Options;
using Insomnia.Portal.Data.Generic;
using System;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using System.Text.Encodings.Web;

namespace Insomnia.Portal.API.Authentication
{
    public class InsomniaAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly AuthConfig _config;

        public InsomniaAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, AuthConfig config
            )
                : base(options, logger, encoder, clock)
        {
            _config = config;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (Request.Cookies.ContainsKey(ResourcesNaming.HeaderToken))
            {
                if(Request.Cookies[ResourcesNaming.HeaderToken] == _config.AdminToken)
                    return AuthenticateAdmin();
                else if(Request.Cookies[ResourcesNaming.HeaderToken] == _config.PoteryashkiToken)
                    return AuthenticatePoteryashki();
            }
            else if (Request.Headers.ContainsKey(ResourcesNaming.HeaderToken))
            {
                if (Request.Headers[ResourcesNaming.HeaderToken] == _config.AdminToken)
                    return AuthenticateAdmin();
                else if (Request.Headers[ResourcesNaming.HeaderToken] == _config.PoteryashkiToken)
                    return AuthenticatePoteryashki();
            }

            return AuthenticateResult.Fail("Authentication scheme is not supported.");
        }

        private AuthenticateResult AuthenticateAdmin()
        {
            return AuthenticateResult.Success(GetTicket("admin"));
        }

        private AuthenticateResult AuthenticatePoteryashki()
        {
            return AuthenticateResult.Success(GetTicket("poteryashki"));
        }

        private AuthenticationTicket GetTicket(List<Claim> claims) =>
            new AuthenticationTicket(new ClaimsPrincipal(new ClaimsIdentity(claims, Scheme.Name)), Scheme.Name);

        private AuthenticationTicket GetTicket(string user) => GetTicket(new List<Claim>() { new Claim("user", user) });

        private AuthenticationTicket GetTicket() => GetTicket(new List<Claim>());
    }
}