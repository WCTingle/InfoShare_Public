using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Collections.Concurrent;
using DotNetOpenAuth.AspNet.Clients;
using Tingle_WebForms.Logic;
using Tingle_WebForms.Models;

[assembly: OwinStartup(typeof(Tingle_WebForms.Startup))]

namespace Tingle_WebForms
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }

    }
}
