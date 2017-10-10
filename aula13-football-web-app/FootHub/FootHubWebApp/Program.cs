using FootHubDb;
using Nancy;
using Nancy.Hosting.Self;
using System;

namespace FootHubWebApp
{
    class Program
    {

        static void StartWebApp()
        {
            var uri = new Uri("http://localhost:3000");
            HostConfiguration hostConfigs = new HostConfiguration();
            hostConfigs.UrlReservations.CreateAutomatically = true;

            using (var host = new NancyHost(uri, new DefaultNancyBootstrapper(), hostConfigs))
            {
                host.Start();

                Console.WriteLine("Your application is running on " + uri);
                Console.WriteLine("Press any [Enter] to close the host.");
                Console.ReadLine();
            }
        }

        static void Main(string[] args)
        {
            StartWebApp();
        }
    }
}
