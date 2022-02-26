using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IpPermission.Web.Infrastructure
{
    public class IpList
    {
        public List<string> WhiteList { get; set; }
        public List<string> BlackList { get; set; }
    }
}
