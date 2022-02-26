using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace IpPermission.Web.Infrastructure
{
    public class BlackIpPermissionFilter : ActionFilterAttribute
    {
        private readonly IpList _ipList;

        public BlackIpPermissionFilter(IOptions<IpList> ipList)
        {
            _ipList = ipList.Value;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var currentIp = context.HttpContext.Connection.RemoteIpAddress.ToString();
            var inBlackList = _ipList.BlackList.Any(x => x == currentIp);

            if (inBlackList)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return;
            }

            base.OnActionExecuted(context);
        }
    }
}
