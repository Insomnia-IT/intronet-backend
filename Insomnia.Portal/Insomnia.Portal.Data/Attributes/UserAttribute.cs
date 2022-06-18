using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Insomnia.Portal.Data.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Insomnia.Portal.Data.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class UserAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly string userName;

        public UserAttribute(string userName)
        {
            this.userName = userName;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (!user.Identity.IsAuthenticated)
                return;

            if (!IsAllowed(user))
            {
                context.Result = new StatusCodeResult((int)HttpStatusCode.Forbidden);
                return;
            }
        }

        public bool IsAllowed(ClaimsPrincipal user) =>
            UserName(user).Value == userName;

        private static Claim UserName(ClaimsPrincipal user) =>
            user.Claims.FirstOrDefault(x => x.Type == "user");
    }
}
