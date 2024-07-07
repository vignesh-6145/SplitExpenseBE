using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SplitExpense.Core.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace SplitExpense.Core.Authentication.Basic.Handlers
{
    public class BasicAuthenticationHandler: AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IUserService _userService;
        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock,IUserService userService) : base(options, logger, encoder, clock)
        {
            this._userService = userService;
        }
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            //checking if headers have any token or not
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return Task.FromResult(AuthenticateResult.Fail("Missing Authoriztion Header"));
            }

            var authorizationHeader = Request.Headers["Authorization"].ToString();

            if (!authorizationHeader.StartsWith("Basic", StringComparison.OrdinalIgnoreCase))
            {
                return Task.FromResult(AuthenticateResult.Fail("Authorization Header Corrupted"));
            }

            var authBase64Decoded = Encoding.UTF8.GetString(Convert.FromBase64String(authorizationHeader.Replace("Basic ","",StringComparison.OrdinalIgnoreCase)));
            var authSplit = authBase64Decoded.Split(':',2);

            if(authSplit.Length != 2) {
                return Task.FromResult(AuthenticateResult.Fail("Invalid Auth Header Format"));
            }

            var email = authSplit[0];
            var password = authSplit[1];
            var user = _userService.AuthenticateUser(email, password);
            if (user == null)
            {
                return Task.FromResult(AuthenticateResult.Fail($"Please check your credentials {email}"));
            }

            var client = new BasicAuthenticationClient
            {
                AuthenticationType = BasicAuthenticationDefaults.AuthenticationScheme,
                IsAuthenticated = true,
                Name = user.UserName
            };

            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(client, new[]
            {
               new Claim(ClaimTypes.Name,user.UserName)
            }));

            return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name)));
        }
    }
}
