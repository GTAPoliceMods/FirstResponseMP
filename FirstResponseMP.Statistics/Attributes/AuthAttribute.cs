using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace FirstResponseMP.Statistics.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class AuthAttribute : Attribute, IAuthorizationFilter
    {
        public Task<AuthorizationPolicy> GetDefaultPolicyAsync() => Task.FromResult(new AuthorizationPolicyBuilder(CookieAuthenticationDefaults.AuthenticationScheme).RequireAuthenticatedUser().Build());

        public AuthAttribute()
        {

            
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var ctxReq = context.HttpContext.Request;
            var ctxRes = context.HttpContext.Response;

            if (string.IsNullOrWhiteSpace(ctxReq.Headers.Authorization))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (ctxReq.Headers.Authorization != "McLeakingItHasGoneOofBecauseHeMadAtMeSadface")
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (ctxReq.Headers.UserAgent != "FirstResponseMP:InternalStatsServ")
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (!context.HttpContext.WebSockets.IsWebSocketRequest)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            return;
        }
    }
}
