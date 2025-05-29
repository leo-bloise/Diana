using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Diana.Core.Host
{
    public interface IDianaHostBuilder
    {
        public IHostEnvironment Environment { get; }
        public IServiceCollection Services { get; }
        public IConfigurationManager Configuration { get; }
        /// <summary>
        ///Creates a new host environment based on the provided arguments. 
        ///It applies the command line arguments to the configuration builder. 
        ///It uses DOTNET_ENVIRONMENT to set the environment name
        ///It's recommended to use this method to create a host environment if you don't have something specific in mind.
        /// </summary>
        public IHostEnvironment CreateHostEnvironment();
        /// <summary>
        ///Creates a new host environment based on the provided arguments. 
        ///It delegates all the configuration to the provided delegate and all the CLI arguments to the delegate as well.
        /// </summary>
        public IHostEnvironment CreateHostEnvironment(Func<IConfigurationBuilder, string[], IHostEnvironment> configureDelegate);
        /// <summary>
        ///Creates and configures Discord settings for the host.
        ///It uses the DiscordSettings class to hold the settings from appsettings.json or appsettings.{Environment}.json.
        /// </summary>
        public void ConfigureDiscordSettings();
        /// <summary>
        ///Creates and configures Discord settings for the host.
        ///It canni
        /// </summary>
        public void ConfigureDiscordSettings(Action<IDianaHostBuilder> configureAction);
        /// <summary>
        /// Configures the logger for the host.
        /// </summary>
        public void ConfigureLogger();
        /// <summary>
        /// Configures the logger for the host.
        /// </summary>
        public void ConfigureLogger(Action<ILoggingBuilder> configureAction);
        /// <summary>
        /// Registers the class loader service in the DI container.
        /// It'll be used to load commands and other types dynamically.
        /// </summary>
        public void RegisterClassLoader();
        /// <summary>
        /// Registers the slash command listener service in the DI container.
        /// It'll be used to listen for slash commands and handle them accordingly.
        /// </summary>
        public void RegisterSlashCommandListener();
        //<summary>
        /// Loads all commands from the specified assembly and registers them in the DI container.
        /// </summary>
        public void LoadCommands();
    }
}
