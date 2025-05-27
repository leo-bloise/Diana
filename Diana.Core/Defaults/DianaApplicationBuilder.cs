using Diana.Core.Host;
using Diana.Core.Host.Settings;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diana.Core.Defaults
{
    public class DianaApplicationBuilder : IDianaHostBuilder
    {
        public IHostEnvironment Environment => _hostEnvironment;

        public IServiceCollection Services => _services;

        public IConfigurationManager Configuration => _configurationManager;

        private IHostEnvironment _hostEnvironment;

        private IConfigurationManager _configurationManager = new ConfigurationManager();

        private IServiceCollection _services = new ServiceCollection();

        private IEnumerable<Type> _commands;

        private string[] _args;

        public DianaApplicationBuilder(string[] args)
        {
            _args = args;
            Services.AddSingleton<DiscordSocketClient>();
        }
        /// <summary>
        /// Creates a new instance of DianaApplicationBuilder with the provided arguments and the default configuration.
        /// </summary>
        public static DianaApplicationBuilder Create(string[] args)
        {
            DianaApplicationBuilder builder = new DianaApplicationBuilder(args);
            builder.ConfigureDiscordSettings();
            builder.ConfigureLogger();
            builder.RegisterClassLoader();
            builder.RegisterSlashCommandListener();
            builder.LoadCommands();
            return builder;
        }
        public IHostEnvironment CreateHostEnvironment()
        {
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            var config = configurationBuilder.
                    AddEnvironmentVariables()
                    .AddCommandLine(_args)
                    .Build();
            return new HostingEnvironment()
            {
                EnvironmentName = config["DOTNET_ENVIRONMENT"] ?? "Production",
            };
        }
        public IHostEnvironment CreateHostEnvironment(Func<IConfigurationBuilder, string[], IHostEnvironment> configureDelegate)
        {
            return configureDelegate.Invoke(new ConfigurationBuilder(), _args);
        }
        public void ConfigureDiscordSettings()
        {
            _hostEnvironment = CreateHostEnvironment();
            Configuration
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{Environment.EnvironmentName}.json", true, true);
            Services.Configure<DiscordSettings>(Configuration.GetSection(nameof(DiscordSettings)));
        }
        public void ConfigureDiscordSettings(Action<IDianaHostBuilder> configureAction)
        {
            configureAction.Invoke(this);
        }
        public void ConfigureLogger()
        {
            Services.AddLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
            });
        }
        public void ConfigureLogger(Action<ILoggingBuilder> configureAction)
        {
            Services.AddLogging(configureAction);
        }

        public void RegisterClassLoader()
        {
            Services.AddSingleton<IClassLoader, DefaultClassLoader>();
        }

        public void RegisterSlashCommandListener()
        {
            Services.AddSingleton<ISlashCommandListener, DefaultSlashCommandListener>();
        }
        public void LoadCommands()
        {
            ServiceProvider serviceProvider = Services.BuildServiceProvider();
            IClassLoader classLoader = serviceProvider.GetRequiredService<IClassLoader>();
            IEnumerable<Type> commands = classLoader.LoadCommands();
            foreach (Type command in commands)
            {
                Services.AddScoped(command);
            }
            _commands = commands;
        }
        public IHost Build()
        {
            return new DianaApplication(Services.BuildServiceProvider(), Configuration.Build(), _commands);
        }
    }
}
