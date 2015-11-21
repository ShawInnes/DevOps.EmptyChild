using System.Net;
using System.Reflection;
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
            return Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        private string GetHostName()
        {
            return Dns.GetHostName();
        }

        private string GetSystemName()
        {
            return Assembly.GetExecutingAssembly().GetName().Name;
        }
    }
}
