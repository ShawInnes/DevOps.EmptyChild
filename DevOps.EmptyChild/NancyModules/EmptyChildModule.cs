using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;

namespace DevOps.EmptyChild.NancyModules
{
    public class EmptyChildModule : NancyModule
    {
        public EmptyChildModule()
        {
            this.Get["/"] = p => View["index"];

            this.Get["/healthcheck"] = p => Response.AsJson(new
            {
                Name=GetSystemName(),
                Host=GetHostName(),
                Version=GetVersionName()
            });
        }

        private string GetVersionName()
        {
            return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        private string GetHostName()
        {
            return System.Net.Dns.GetHostName();
        }

        private string GetSystemName()
        {
            return System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        }
    }
}
