using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace IpPermission.Web.Infrastructure
{
    public class WhiteIpPermissionMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly IpList _ipList;

        public WhiteIpPermissionMiddleWare(RequestDelegate next, IOptions<IpList> ipList)
        {
            _next = next;
            _ipList = ipList.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            var currentIp = context.Connection.RemoteIpAddress.ToString();
            var inWhiteList = _ipList.WhiteList.Any(x => x == currentIp);

            if (!inWhiteList)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return;
            }

            await _next(context);
        }
    }
}
