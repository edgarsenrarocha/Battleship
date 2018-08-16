using FlareHR.Battleship.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FlareHR.Battleship.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // create service collection
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            // create service provider
            var serviceProvider = serviceCollection.BuildServiceProvider();

            // entry to run app
            serviceProvider.GetService<App>().Run();
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            // add logging
            serviceCollection.AddSingleton(new LoggerFactory()
              .AddConsole()
              .AddDebug());
            serviceCollection.AddLogging();

            // add services
            serviceCollection.AddTransient<IPlayerService, PlayerService>();
            serviceCollection.AddTransient<IGameService, GameService>();

            // add app
            serviceCollection.AddTransient<App>();
        }
    }
}

