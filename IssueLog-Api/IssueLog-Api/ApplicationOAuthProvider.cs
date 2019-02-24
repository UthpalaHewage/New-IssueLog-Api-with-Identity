using IssueLog_Api.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace IssueLog_Api
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var userStore = new UserStore<ApplicationUser>(new IssueLogDbContext());
            var manager = new UserManager<ApplicationUser>(userStore);
            var user = await manager.FindAsync(context.UserName, context.Password);
            if (user != null)
            {
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);

                identity.AddClaim(new Claim("Email", user.Email));
                identity.AddClaim(new Claim("FName", user.FName));
                identity.AddClaim(new Claim("LName", user.LName));
                context.Validated(identity);
            }
            else
                return;
        }
    }
}