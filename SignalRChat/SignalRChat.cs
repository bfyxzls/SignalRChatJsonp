using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;

[assembly: OwinStartup(typeof(SignalR.Core.Startup))]
namespace SignalR.Core
{

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            app.Map("/signalr", map =>
            {
                map.UseCors(CorsOptions.AllowAll);
                var hubConfiguration = new HubConfiguration
                {
                    EnableJSONP = true
                };
                map.RunSignalR(hubConfiguration);
            });
            app.MapSignalR("/UrlHubZzl", new HubConfiguration() { EnableJSONP = true });
        }

    }
}