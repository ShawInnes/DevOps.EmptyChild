using System.Configuration.Abstractions;
using Autofac;
using DevOps.EmptyChild.Services;
using Hangfire;
using Hangfire.MemoryStorage;
using Owin;
using Serilog;
using Topshelf;
using Topshelf.Autofac;
using TopShelf.Owin;

namespace DevOps.EmptyChild
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.AppSettings()
                .MinimumLevel.Debug()
                .WriteTo.LiterateConsole()
                .CreateLogger();

            var config = new ConfigurationManager();
            var domain = config.AppSettings.AppSetting<string>("emptychild:domain", () => "localhost", () => "localhost");
            var port = config.AppSettings.AppSetting<int>("emptychild:port", () => 19090, () => 19090);

            var builder = new ContainerBuilder();
            builder.RegisterType<EmptyChildService>();
            builder.RegisterType<MemoryStorage>()
                .OnActivated(c => JobStorage.Current = c.Instance)
                .SingleInstance();

            var container = builder.Build();

            GlobalConfiguration.Configuration.UseSerilogLogProvider();
            GlobalConfiguration.Configuration.UseAutofacActivator(container);

            HostFactory.Run(x =>
            {
                x.UseSerilog();
                x.UseAutofacContainer(container);
                x.Service<EmptyChildService>(s =>
                {
                    s.ConstructUsingAutofacContainer();
                    s.WhenStarted(service => service.Start());
                    s.WhenStopped(service => service.Stop());
                    s.OwinEndpoint(app =>
                    {
                        app.Domain = domain;
                        app.Port = port;
                        app.ConfigureAppBuilder(appBuilder =>
                        {
                            appBuilder.UseHangfireDashboard("/hangfire", new DashboardOptions(), container.Resolve<MemoryStorage>());
                            appBuilder.UseHangfireServer(new BackgroundJobServerOptions(),
                                container.Resolve<MemoryStorage>());
                            appBuilder.UseNancy();
                        });
                    });
                });
                x.StartAutomatically();
                x.SetServiceName("EmptyChildService");
                x.RunAsNetworkService();
            });
        }
    }
}
